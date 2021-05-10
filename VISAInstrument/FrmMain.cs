using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ivi.Visa;
using VISAInstrument.Extension;
using VISAInstrument.Port;
using VISAInstrument.Properties;
using VISAInstrument.Utility;


namespace VISAInstrument
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private bool _cancelDisplayForm;
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtRS232 == sender as RadioButton)
            {
                this.tableLayoutPanel.RowStyles[2].Height = 35F;
                this.tableLayoutPanel.RowStyles[3].Height = 0F;
                return;
            }
            if (rbtLAN == sender as RadioButton)
            {
                this.tableLayoutPanel.RowStyles[2].Height = 0F;
                this.tableLayoutPanel.RowStyles[3].Height = 35F;
                return;
            }
            this.tableLayoutPanel.RowStyles[2].Height = 0F;
            this.tableLayoutPanel.RowStyles[3].Height = 0F;
        }

        private void DoSomethingForRadioButton(params Action[] actionOfRbt)
        {
            if (actionOfRbt.Length != 4) throw new ArgumentException();
            if (rbtRS232.Checked) actionOfRbt[0]();
            if (rbtUSB.Checked) actionOfRbt[1]();
            if (rbtGPIB.Checked) actionOfRbt[2]();
            if (rbtLAN.Checked) actionOfRbt[3]();
        }

        private readonly int[] _baudRate = { 256000, 128000, 115200, 57600, 56000, 43000, 38400, 28800, 19200, 9600, 4800, 2400, 1200, 600, 300, 110 };
        private readonly int[] _dataBits = { 8, 7, 6 };
        private bool _isAsciiCommand = true;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            ShowTime();
            关于ToolStripMenuItem.Text = $@"{Application.ProductName}({Application.ProductVersion})";
            rbtRS232.Checked = true;
            btnRefresh.PerformClick();
            btnOpen.Text = Resources.OpenString;
            cboBaudRate.DataSource = _baudRate;
            cboBaudRate.SelectedIndex = 9;
            cboParity.DataSource = Enum.GetValues(typeof(SerialParity));
            cboStopBits.DataSource = Enum.GetValues(typeof(SerialStopBitsMode));
            cboStopBits.SelectedIndex = 0;
            cboDataBits.DataSource = _dataBits;
            cboFlowControl.DataSource = Enum.GetValues(typeof(SerialFlowControlModes));
            EnableControl(true);
            if (_cancelDisplayForm) Close();
        }

        CancellationTokenSource _cts;
        private void ShowTime()
        {
            Task.Factory.StartNew(() => 
            {
                string now;
                while(!_cts.IsCancellationRequested)
                {
                    now = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    InvokeToForm(() => 时间ToolStripMenuItem.Text = now);
                    Thread.Sleep(500);
                }
            });
        }

        private PortOperatorBase _portOperatorBase;
        private bool _isWritingError = false;

        private bool Write()
        {
            _isWritingError = false;
            if (string.IsNullOrEmpty(txtCommand.Text))
            {
                Invoke(new Action(() => MessageBox.Show(Resources.CommandNotEmpty)));
                return false;
            }
            string asciiString = string.Empty;
            byte[] byteArray = null;
            if (_isAsciiCommand)
            {
                asciiString = txtCommand.Text;
            }
            else
            {
                if (StringEx.TryParseByteStringToByte(txtCommand.Text, out byte[] bytes))
                {
                    byteArray = bytes;
                }
                else
                {
                    _isWritingError = true;
                    Invoke(new Action(() => MessageBox.Show(@"转换字节失败，请按照“XX XX XX”格式输入内容")));
                    return false;
                }
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                if (_isAsciiCommand)
                {
                    if (chkAppendNewLine.Checked)
                    {
                        _portOperatorBase.WriteLine(asciiString);
                    }
                    else
                    {
                        _portOperatorBase.Write(asciiString);
                    }
                }
                else
                {
                    if (chkAppendNewLine.Checked)
                    {
                        _portOperatorBase.WriteLine(byteArray);
                    }
                    else
                    {
                        _portOperatorBase.Write(byteArray);
                    }
                }

            }
            catch
            {
                Invoke(new Action(() => MessageBox.Show($@"写入命令“{txtCommand.Text}”失败！")));
                return false;
            }
            Invoke(new Action(() => DisplayToTextBox($"[Time:{stopwatch.ElapsedMilliseconds}ms] Write: {txtCommand.Text}")));
            return true;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            Write();
        }

        private void Read()
        {
            Read(rdoUntilNewLine.Checked, (int)nudSpecifiedCount.Value);
        }

        private void Read(bool isUntilNewLine,int specifiedCount)
        {
            ClearIfTextBoxOverFlow();
            string result;
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                if (_isAsciiCommand)
                {
                    result = isUntilNewLine ? _portOperatorBase.ReadLine() : _portOperatorBase.Read(specifiedCount);
                }
                else
                {
                    byte[] bytes = isUntilNewLine ? _portOperatorBase.ReadToBytes() : _portOperatorBase.ReadToBytes(specifiedCount);
                    if (ByteEx.TryParseByteToByteString(bytes, out string byteString))
                    {
                        result = byteString;
                    }
                    else
                    {
                        throw new InvalidCastException("无法转换从接收缓冲区接收回来的数据");
                    }
                }
            }
            catch (IOTimeoutException)
            {
                result = Resources.ReadTimeout;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Invoke(new Action(() => DisplayToTextBox($"[Time:{stopwatch.ElapsedMilliseconds}ms] Read:  {result}")));
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Read();
        }

        private bool Query()
        {
            bool isSuccessful = Write();
            if (isSuccessful && !_isWritingError) Read();
            return isSuccessful;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        private bool NewPortInstance()
        {
            bool hasAddress = false;
            bool hasException = false;
            DoSomethingForRadioButton(
                () =>
                {
                    if (cboRS232.SelectedIndex == -1) return;
                    try
                    {
                        _portOperatorBase = new RS232PortOperator(((Pair<string, string>)cboRS232.SelectedItem).Value.ToString(),
                                               (int)cboBaudRate.SelectedItem, (SerialParity)cboParity.SelectedItem,
                                               (SerialStopBitsMode)cboStopBits.SelectedItem, (int)cboDataBits.SelectedItem);
                        hasAddress = true;
                    }
                    catch
                    {
                        hasException = true;
                    }
                },
                () =>
                {
                    if (cboUSB.SelectedIndex == -1) return;
                    try
                    {
                        _portOperatorBase = new USBPortOperator(cboUSB.SelectedItem.ToString());
                        hasAddress = true;
                    }
                    catch
                    {
                        hasException = true;
                    }
                },
                () =>
                {
                    if (cboGPIB.SelectedIndex == -1) return;
                    try
                    {
                        _portOperatorBase = new GPIBPortOperator(cboGPIB.SelectedItem.ToString());
                        hasAddress = true;
                    }
                    catch
                    {
                        hasException = true;
                    }
                },
                () =>
                {
                    if (cboLAN.SelectedIndex == -1) return;
                    try
                    {
                        _portOperatorBase = new LANPortOperator(cboLAN.SelectedItem.ToString());
                        hasAddress = true;
                    }
                    catch
                    {
                        hasException = true;
                    }
                });
            if(!hasException && hasAddress) _portOperatorBase.Timeout = (int)nudTimeout.Value;
            return hasAddress;
        }

        private void DisplayToTextBox(string content)
        {
            txtDisplay.Text += $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {content}\r\n";
            txtDisplay.SelectionStart = txtDisplay.Text.Length - 1;
            txtDisplay.ScrollToCaret();
        }

        private void ClearIfTextBoxOverFlow()
        {
            if (txtDisplay.Text.Length > 20480) txtDisplay.Clear();
        }

        Task t = null;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string title = Text;
            t = Task.Factory.StartNew(() =>
            {
                InvokeToForm(() => { btnRefresh.Enabled = false; btnOpen.Enabled = false; Text = title + Resources.LoadingRS232; });
                string[] content1 = PortUltility.FindAddresses(PortType.RS232);
                string[] content2 = PortUltility.FindRS232Type(content1);
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();
                for (int i = 0; i < content2.Length; i++)
                {
                    if (content2[i].Contains("LPT")) continue;
                    list1.Add(content1[i]);
                    list2.Add(content2[i]);
                }
                content1 = list1.ToArray();
                content2 = list2.ToArray();
                InvokeToForm(() => cboRS232.ShowAndDisplay(content1, content2));
                InvokeToForm(() => { Text = title + Resources.LoadingUSB; });
                content1 = PortUltility.FindAddresses(PortType.USB);
                InvokeToForm(() => cboUSB.ShowAndDisplay(content1));
                InvokeToForm(() => { Text = title + Resources.LoadingGPIB; });
                content1 = PortUltility.FindAddresses(PortType.GPIB);
                InvokeToForm(() => cboGPIB.ShowAndDisplay(content1));
                InvokeToForm(() => { Text = title + Resources.LoadingLAN; });
                content1 = PortUltility.FindAddresses(PortType.LAN);
                InvokeToForm(() => cboLAN.ShowAndDisplay(content1));
                InvokeToForm(() => { btnRefresh.Enabled = true; btnOpen.Enabled = true; Text = title; });
            }).ContinueWith(x=> 
            {
                if(x.IsFaulted)
                {
                    _cancelDisplayForm = true;
                    InvokeToForm(() => { tableLayoutPanel.Enabled = false; this.Text = Resources.RuntimeError; });
                    MessageBox.Show(x.Exception.InnerException.Message);
                }
            });
        }


        private void InvokeToForm(Action action)
        {
            try
            {
                this.Invoke(action);
            }
            catch { }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(btnOpen.Text == Resources.OpenString)
            {
                if (NewPortInstance())
                {
                    try
                    {
                        _portOperatorBase.Open();
                        btnOpen.Text = Resources.CloseString;
                        if(_portOperatorBase is RS232PortOperator)
                        {
                            chkRealTimeReceive.Enabled = true;
                            BindOrRemoveDataReceivedEvent();
                        }
                        else chkRealTimeReceive.Enabled = false;
                        EnableControl(false);
                        chkStartCycle_CheckedChanged(null, null);
                    }
                    catch { }
                }
                
            }
            else
            {
                if(CheckCycleEnable("关闭"))
                {
                    return;
                }

                try
                {
                    _portOperatorBase.Close();
                }
                catch { }
                btnOpen.Text = Resources.OpenString;
                EnableControl(true);
            }
        }

        private void EnableControl(bool enable)
        {
            flowLayoutPanel1.Enabled = enable;
            flowLayoutPanel2.Enabled = enable;
            btnRefresh.Enabled = enable;
            flowLayoutPanel5.Enabled = enable;
            flowLayoutPanel7.Enabled = !enable;
            txtCommand.Enabled = !enable;
            if(enable)
            {
                btnWrite.Enabled = false;
                btnRead.Enabled = false;
                btnQuery.Enabled = false;
                btnCycle.Enabled = false;
            }
            
            lblOverTime.Enabled = enable;
            lblTimeout.Enabled = enable;
            nudTimeout.Enabled = enable;
        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtDisplay.Clear();
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            EnableContextMenuStrip(txtDisplay.Text.Length != 0);
        }

        private void EnableContextMenuStrip(bool enable)
        {
            contextMenuStrip.Enabled = enable;
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtDisplay.Focus();
            txtDisplay.SelectAll();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtDisplay.SelectedText);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckCycleEnable("关闭"))
            {
                e.Cancel = true;
                return;
            }

            if (t != null && !t.IsCompleted)
            {
                MessageBox.Show(Resources.LoadingInstrumentResource);
                e.Cancel = true;
                return;
            }
            _cts.Cancel();
            try
            {
                _portOperatorBase?.Close();
            }
            catch { }
        }

        private const string IpRegex = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";
        private void btnCheckIP_Click(object sender, EventArgs e)
        {
            if (!txtIPAddress.Text.IsMatch(IpRegex))
            {
                MessageBox.Show(Resources.NotCorrectIP);
                txtIPAddress.SetSelect();
                return;
            }
            if (cboLAN.Items.Cast<object>().Any(item => ((string)item).Contains(txtIPAddress.Text)))
            {
                MessageBox.Show(Resources.LANContainIP);
                txtIPAddress.SetSelect();
                return;
            }
            if (!PortUltility.OpenIPAddress(txtIPAddress.Text, out string fullAddress))
            {
                MessageBox.Show(Resources.NotDetectIP);
                txtIPAddress.SetSelect();
                return;
            }
            cboLAN.Items.Add(fullAddress);
            cboLAN.Text = cboLAN.Items[cboLAN.Items.Count-1].ToString();
            MessageBox.Show(Resources.DetectOK);
        }

        private void githubToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.GithubURL);
        }

        private void blogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.BlogURL);
        }

        private void byCNXYToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:cnc46@qq.com");
        }

        private void 时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string system32 = Environment.GetFolderPath(Environment.SpecialFolder.System);
                Process.Start($@"{system32}\rundll32.exe", "shell32.dll,Control_RunDLL timedate.cpl,,0");
            }
            catch { }
        }

        private void 全选ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtCommand.SelectAll();
        }

        private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCommand.SelectedText);
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCommand.Text = Clipboard.GetText();
            Clipboard.Clear();
        }

        private void cmsCommand_Opening(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtCommand.Text.Trim()))
            {
                全选ToolStripMenuItem1.Enabled = false;
                复制ToolStripMenuItem1.Enabled = false;
            }
            else
            {
                全选ToolStripMenuItem1.Enabled = true;
                复制ToolStripMenuItem1.Enabled = !string.IsNullOrEmpty(txtCommand.SelectedText);
            }

            粘贴ToolStripMenuItem.Enabled = !string.IsNullOrEmpty(Clipboard.GetText());
        }

        private void rdoAsciiByte_CheckedChanged(object sender, EventArgs e)
        {
            _isAsciiCommand = rdoAscii.Checked;
            if (sender == rdoAscii && !rdoAscii.Checked) return;
            if(sender == rdoByte && !rdoByte.Checked) return;
            if (string.IsNullOrEmpty(txtCommand.Text)) return;
            bool isSuccessful = false;
            if (rdoAscii.Checked)
            {
                if (StringEx.TryParseByteStringToAsciiString(txtCommand.Text, out string asciiString))
                {
                    txtCommand.Text = asciiString;
                    isSuccessful = true;
                }
            }
            else
            {

                if (StringEx.TryParseAsciiStringToByteString(txtCommand.Text, out string byteString))
                {
                    txtCommand.Text = byteString;
                    isSuccessful = true;
                }
            }
            if(!isSuccessful) txtCommand.Clear();
        }

        private void rdoUntilNewLineSpecifiedCount_CheckedChanged(object sender, EventArgs e)
        {
            nudSpecifiedCount.Enabled = !rdoUntilNewLine.Checked;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string message = Common.VisaSharedComponent.Concat(Common.NiVisaRuntime).Aggregate((x, y) => $"{x}\r\n{y}").TrimEnd('\r', '\n');
            MessageBox.Show(message);
        }

        private void chkStartCycle_CheckedChanged(object sender, EventArgs e)
        {
            if(!chkRealTimeReceive.Checked)
            {
                flowLayoutPanel10.Enabled = chkStartCycle.Checked;
                btnWrite.Enabled = !chkStartCycle.Checked;
                btnRead.Enabled = !chkStartCycle.Checked;
                btnQuery.Enabled = !chkStartCycle.Checked;
                btnCycle.Enabled = chkStartCycle.Checked;
                rdoUntilNewLine.Enabled = true;
                rdoSpecifiedCount.Enabled = true;
                rdoSendRead.Enabled = true;
            }
            else
            {
                btnRead.Enabled = false;
                btnQuery.Enabled = false;

                rdoUntilNewLine.Enabled = false;
                rdoSendRead.Enabled = false;
                rdoSpecifiedCount.Enabled = false;

                if (chkStartCycle.Checked)
                {
                    rdoSend.Checked = true;
                    rdoUntilNewLine.Checked = true;
                    
                    flowLayoutPanel10.Enabled = true;
                    btnWrite.Enabled = false;
                    btnCycle.Enabled = true;
                }
                else
                {
                    flowLayoutPanel10.Enabled = false;
                    btnWrite.Enabled = true;
                    btnCycle.Enabled = false;
                }
            }
        }

        private void rdoSendRead_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSendRead.Checked)  btnCycle.Text = "循环发送读取";
        }

        private void rdoSend_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSend.Checked) btnCycle.Text = "循环发送";
        }

        private void EnableCycle(bool enabled)
        {
            Invoke(new Action(() => flowLayoutPanel9.Enabled = !enabled));
            cycleEnabled = enabled;
        }

        bool cycleEnabled;
        string originalCycleText;
        const string StopCycleText = "停止"; 
        private void btnCycle_Click(object sender, EventArgs e)
        {
            if(btnCycle.Text == StopCycleText)
            {
                EnableCycle(false);
                btnCycle.Enabled = false;
                Task.Factory.StartNew(() => 
                {
                    while(btnCycle.Text == StopCycleText)
                    {
                        Thread.Sleep(100);
                    }
                    Invoke(new Action(() => btnCycle.Enabled = true));
                });
            }
            else
            {
                EnableCycle(true);
                originalCycleText = btnCycle.Text;
                btnCycle.Text = StopCycleText;
                Task.Factory.StartNew(() =>
                {
                    int count = 0;
                    int cycleCount = (int)nudCycleCount.Value;
                    int intervalTime = (int)nudInterval.Value;
                    while (cycleEnabled)
                    {
                        if (cycleCount != 0 && count >= cycleCount)
                        {
                            EnableCycle(false);
                            break;
                        }
                        bool isSuccessful;
                        if (rdoSend.Checked) isSuccessful = Write();
                        else isSuccessful = Query();
                        if (!isSuccessful)
                        {
                            EnableCycle(false);
                            break;
                        }
                        else Thread.Sleep(intervalTime);
                        if(cycleCount != 0) count++;
                    }
                    Invoke(new Action(() => btnCycle.Text = originalCycleText));
                });
            }
        }

        private bool CheckCycleEnable(string operationName)
        {
            bool cycleEnabledTemp = cycleEnabled;
            if (cycleEnabledTemp)
            {
                MessageBox.Show($"请停止循环操作后再执行{operationName}操作");
            }
            return cycleEnabledTemp;
        }

        private void BindOrRemoveDataReceivedEvent()
        {
            if (_portOperatorBase is RS232PortOperator portOperator)
            {
                if (chkRealTimeReceive.Checked)
                {
                    portOperator.DataReceived += PortOperator_DataReceived;
                }
                else
                {
                    portOperator.DataReceived -= PortOperator_DataReceived;
                }
            }
        }

        private void chkRealTimeReceive_CheckedChanged(object sender, EventArgs e)
        {
            chkStartCycle_CheckedChanged(null, null);
            BindOrRemoveDataReceivedEvent();
        }

        private void PortOperator_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(e.BytesToRead > 0) Read(false, e.BytesToRead);
        }
    }
}