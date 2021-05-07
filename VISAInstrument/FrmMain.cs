using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using VISAInstrument.Utility;
using VISAInstrument.Port;
using VISAInstrument.Extension;
using VISAInstrument.Properties;
using Ivi.Visa;
using NationalInstruments.Visa;
using System.Threading;


namespace VISAInstrument
{
    public partial class FrmMain : Form
    {
        public bool CancelDisplayForm { private set; get; }
        public FrmMain()
        {
            InitializeComponent();
        }

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
        private string[] _asciiCommands = { "*IDN?","*TST?", "*RST", "*CLS", "*ESE", "*ESE?", "*ESR?", "*OPC", "*OPC?", "*PSC", "*PSC?", "*SRE", "*SRE?", "*STB?", "*SAV", "*RCL","*TRG" };
        private string[] _hexCommands;
        private bool _isAsciiCommand = true;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            ShowTime();
            关于ToolStripMenuItem.Text = $"{Application.ProductName}({Application.ProductVersion})";
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
            //Ascii
            _asciiCommands = _asciiCommands.OrderBy(n => n).ToArray();
            cboCommand.DataSource = _asciiCommands;
            //Hex
            _hexCommands = _asciiCommands.ToHexString();
            cboCommand.SelectedIndex = 4;
            EnableControl(true);
            if (CancelDisplayForm) Close();
        }

        CancellationTokenSource cts;
        private void ShowTime()
        {
            Task.Factory.StartNew(() => 
            {
                string now;
                while(!cts.IsCancellationRequested)
                {
                    now = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    InvokeToForm(() => 时间ToolStripMenuItem.Text = now);
                    Thread.Sleep(500);
                }
            });
        }

        private PortOperatorBase _portOperatorBase;
        private bool _isWritingError = false;
        private void btnWrite_Click(object sender, EventArgs e)
        {
            _isWritingError = false;
            if (string.IsNullOrEmpty(cboCommand.Text))
            {
                MessageBox.Show(Resources.CommandNotEmpty);
                return;
            }
            string content;
            if(_isAsciiCommand)
            {
                content = cboCommand.Text;
            }
            else
            {
                try
                {
                    content = cboCommand.Text.ToAsciiString();
                }
                catch(Exception ex)
                {
                    _isWritingError = true;
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                _portOperatorBase.WriteLine(content);
                cboCommand.AddItem(cboCommand.Text);
            }
            catch
            {
                content =  $"写入命令\"{cboCommand.Text}\"失败！";
            }
            DisplayToTextBox($"[Time:{stopwatch.ElapsedMilliseconds}ms] Write: {cboCommand.Text}");
        }


        private void btnRead_Click(object sender, EventArgs e)
        {
            ClearIfTextBoxOverFlow();
            string result;
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                result = _isAsciiCommand ? _portOperatorBase.ReadLine() : _portOperatorBase.ReadLine().ToHexString();
            }
            catch(IOTimeoutException)
            {
                result = Resources.ReadTimeout;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            DisplayToTextBox($"[Time:{stopwatch.ElapsedMilliseconds}ms] Read:  {result}");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnWrite.PerformClick();
            if(!_isWritingError) btnRead.PerformClick();
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
            if(!hasException) _portOperatorBase.Timeout = (int)nudTimeout.Value;
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
                    CancelDisplayForm = true;
                    InvokeToForm(() => { tableLayoutPanel.Enabled = false; this.Text = Resources.RuntimeError; });
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
                        EnableControl(false);
                    }
                    catch { }
                }
            }
            else
            {
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
            lblCommand.Enabled = !enable;
            cboCommand.Enabled = !enable;
            btnWrite.Enabled = !enable;
            btnRead.Enabled = !enable;
            btnQuery.Enabled = !enable;
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
            if(txtDisplay.Text.Length != 0 )
            {
                EnableContextMenuStrip(true);
            }
            else
            {
                EnableContextMenuStrip(false);
            }
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
            if(t != null && !t.IsCompleted)
            {
                MessageBox.Show(Resources.LoadingInstrumentResource);
                e.Cancel = true;
                return;
            }
            cts.Cancel();
            try
            {
                _portOperatorBase?.Close();
            }
            catch { }
        }
        public const string IpRegex = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";
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

        private void EnableAsciiOrHexMenuItem(bool isAsciiChecked)
        {
            aSCIIToolStripMenuItem.Checked = isAsciiChecked;
            hexToolStripMenuItem.Checked = !isAsciiChecked;
            _isAsciiCommand = isAsciiChecked;
        }

        //ASCII
        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableAsciiOrHexMenuItem(true);
            cboCommand.DataSource = _asciiCommands;
            cboCommand.SelectedIndex = 4;
            
        }
        //Hex
        private void hexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableAsciiOrHexMenuItem(false);
            cboCommand.DataSource = _hexCommands;
            cboCommand.SelectedIndex = 4;
        }

        private void 全选ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cboCommand.SelectAll();
        }

        private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(cboCommand.SelectedText);
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cboCommand.Text = Clipboard.GetText();
            Clipboard.Clear();
        }

        private void cmsCommand_Opening(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(cboCommand.Text.Trim()))
            {
                全选ToolStripMenuItem1.Enabled = false;
                复制ToolStripMenuItem1.Enabled = false;
            }
            else
            {
                全选ToolStripMenuItem1.Enabled = true;
                if(string.IsNullOrEmpty(cboCommand.SelectedText))
                {
                    复制ToolStripMenuItem1.Enabled = false;
                }
                else
                {
                    复制ToolStripMenuItem1.Enabled = true;
                }
            }
            if(string.IsNullOrEmpty(Clipboard.GetText()))
            {
                粘贴ToolStripMenuItem.Enabled = false;
            }
            else
            {
                粘贴ToolStripMenuItem.Enabled = true;
            }
        }
    }
}