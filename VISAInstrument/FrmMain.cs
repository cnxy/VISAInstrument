using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using VISAInstrument.Port;
using VISAInstrument.Extension;
using VISAInstrument.Properties;

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

        int[] baudRate = { 256000, 128000, 115200, 57600, 56000, 43000, 38400, 28800, 19200, 9600, 4800, 2400, 1200, 600, 300, 110 };
        int[] dataBits = { 8, 7, 6 };
        string[] commmands = { "*IDN?","*TST?", "*RST", "*CLS", "*ESE", "*ESE?", "*ESR?", "*OPC", "*OPC?", "*PSC", "*PSC?", "*SRE", "*SRE?", "*STB?", "*SAV", "*RCL","*TRG" };
        private void FrmMain_Load(object sender, EventArgs e)
        {
            rbtRS232.Checked = true;
            btnRefresh.PerformClick();
            btnOpen.Text = Resources.OpenString;
            cboBaudRate.DataSource = baudRate;
            cboBaudRate.SelectedIndex = 9;
            cboParity.DataSource = Enum.GetValues(typeof(Parity));
            cboStopBits.DataSource = Enum.GetValues(typeof(StopBits));
            cboStopBits.SelectedIndex = 1;
            cboDataBits.DataSource = dataBits;
            cboFlowControl.DataSource = Enum.GetValues(typeof(FlowControl));
            cboCommand.DataSource = commmands.OrderBy(n => n).ToArray();
            cboCommand.SelectedIndex = 4;
            if (CancelDisplayForm) Close();
        }
        PortOperatorBase portOperatorBase;
        private void btnWrite_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cboCommand.Text))
            {
                MessageBox.Show("命令不能为空！");
                return;
            }
            try
            {
                DisplayToTextBox($"Write: {cboCommand.Text}");
                portOperatorBase.WriteLine(cboCommand.Text);
                cboCommand.AddItem(cboCommand.Text);
            }
            catch
            {
                MessageBox.Show($"写入命令\"{cboCommand.Text}\"失败！");
            }
        }


        private void btnRead_Click(object sender, EventArgs e)
        {
            ClearIfTextBoxOverFlow();
            string result;
            try
            {
                result = portOperatorBase.ReadLine();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            DisplayToTextBox($"Read:  {result}");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnWrite.PerformClick();
            btnRead.PerformClick();
        }

        private bool NewPortInstance()
        {
            bool hasAddress = false;
            DoSomethingForRadioButton(
                () =>
                {
                    if (cboRS232.SelectedIndex == -1) return;
                    portOperatorBase = new RS232PortOperator(((Pair<string,string>)cboRS232.SelectedItem).Value.ToString(),
                        (int)cboBaudRate.SelectedItem, (Parity)cboParity.SelectedItem,
                        (StopBits)cboStopBits.SelectedItem, (int)cboDataBits.SelectedItem);
                    hasAddress = true;
                },
                () =>
                {
                    if (cboUSB.SelectedIndex == -1) return;
                    portOperatorBase = new USBPortOperator(cboUSB.SelectedItem.ToString());
                    hasAddress = true;
                },
                () =>
                {
                    if (cboGPIB.SelectedIndex == -1) return;
                    portOperatorBase = new GPIBPortOperator(cboGPIB.SelectedItem.ToString());
                    hasAddress = true;
                },
                () =>
                {
                    if (cboLAN.SelectedIndex == -1) return;
                    portOperatorBase = new LANPortOperator(cboLAN.SelectedItem.ToString());
                    hasAddress = true;
                });
            return hasAddress;
        }

        private void DisplayToTextBox(string content)
        {
            txtDisplay.Text += $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}   {content}\r\n";
            txtDisplay.SelectionStart = txtDisplay.Text.Length - 1;
            txtDisplay.ScrollToCaret();
        }

        private void ClearIfTextBoxOverFlow()
        {
            if (txtDisplay.Text.Length > 20480) txtDisplay.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string[] rs232Address = PortUltility.FindAddresses(PortType.RS232);
                cboRS232.ShowAndDisplay(rs232Address, PortUltility.FindRS232Type(rs232Address));
                cboUSB.ShowAndDisplay(PortUltility.FindAddresses(PortType.USB));
                cboGPIB.ShowAndDisplay(PortUltility.FindAddresses(PortType.GPIB));
                cboLAN.ShowAndDisplay(PortUltility.FindAddresses(PortType.LAN));
            }
            catch(Exception ex)
            {
                if(ex is ResultException || ex is DllNotFoundException)

                {
                    DialogResult result = MessageBox.Show("加载VISA32错误，请保证已经安装VISA32运行时！\r\n\r\n点击“是”从弹出的网址进行下载并安装。", "运行时错误", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start("https://github.com/cnxy/VISAInstrument/releases/download/1.0.0.0/visa441runtime.zip");
                    }
                    CancelDisplayForm = true;
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(btnOpen.Text == Resources.OpenString)
            {
                if (NewPortInstance())
                {
                    try
                    {
                        portOperatorBase.Open();
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
                    portOperatorBase.Close();
                    btnOpen.Text = Resources.OpenString;
                    EnableControl(true);
                }
                catch { }
            }
        }

        private void EnableControl(bool enable)
        {
            flowLayoutPanel1.Enabled = enable;
            flowLayoutPanel2.Enabled = enable;
            btnRefresh.Enabled = enable;
            flowLayoutPanel5.Enabled = enable;
            flowLayoutPanel3.Enabled = !enable;
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

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/cnxy");
        }

        private void blogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.cnc6.cn");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                portOperatorBase?.Close();
            }
            catch { }
        }
        public const string IPRegex = @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";
        private void btnCheckIP_Click(object sender, EventArgs e)
        {
            if (!txtIPAddress.Text.IsMatch(IPRegex))
            {
                MessageBox.Show("不是正确的IP地址，请重新输入！");
                txtIPAddress.SetSelect();
                return;
            }
            foreach (var item in cboLAN.Items)
            {
                if (((string)item).Contains(txtIPAddress.Text))
                {
                    MessageBox.Show("LAN地址已经包含该IP，请重新输入！");
                    txtIPAddress.SetSelect();
                    return;
                }
            }
            if (!PortUltility.OpenIPAddress(txtIPAddress.Text, out string fullAddress))
            {
                MessageBox.Show("没有检测到有效的仪器IP地址，请重新输入！");
                txtIPAddress.SetSelect();
                return;
            }
            cboLAN.Items.Add(fullAddress);
            cboLAN.Text = cboLAN.Items[cboLAN.Items.Count-1].ToString();
            MessageBox.Show("检测成功！");
        }
    }
}
