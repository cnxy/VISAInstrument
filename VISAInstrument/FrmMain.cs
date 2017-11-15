using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VISAInstrument.Port;
using VISAInstrument.Extension;
using VISAInstrument.Properties;
using System.IO.Ports;

namespace VISAInstrument
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtRS232 != sender as RadioButton)
            {
                this.tableLayoutPanel.RowStyles[2].Height = 0;
                return;
            }
            this.tableLayoutPanel.RowStyles[2].Height = 63F;
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
            cboCommand.SelectedIndex = 0;
        }
        PortOperatorBase portOperatorBase;
        private void btnWrite_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cboCommand.Text))
            {
                MessageBox.Show("命令不能为空！");
                return;
            }
            portOperatorBase.WriteLine(cboCommand.Text);
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
            DisplayToTextBox(result);
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
                    portOperatorBase = new RS232PortOperator(cboRS232.SelectedItem.ToString(),
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
            txtDisplay.Text += $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {content}\r\n";
            txtDisplay.SelectionStart = txtDisplay.Text.Length - 1;
            txtDisplay.ScrollToCaret();
        }

        private void ClearIfTextBoxOverFlow()
        {
            if (txtDisplay.Text.Length > 20480) txtDisplay.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboRS232.ShowAndDisplay(PortUltility.FindAddresses(PortType.RS232));
            cboUSB.ShowAndDisplay(PortUltility.FindAddresses(PortType.USB));
            cboGPIB.ShowAndDisplay(PortUltility.FindAddresses(PortType.GPIB));
            cboLAN.ShowAndDisplay(PortUltility.FindAddresses(PortType.LAN));
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(btnOpen.Text == Resources.OpenString)
            {
                if (NewPortInstance())
                {
                    btnOpen.Text = Resources.CloseString;
                    EnableControl(false);
                    portOperatorBase.Open();
                }
            }
            else
            {
                btnOpen.Text = Resources.OpenString;
                EnableControl(true);
                try
                {
                    portOperatorBase.Close();
                }
                catch { }
            }
        }

        private void EnableControl(bool enable)
        {
            flowLayoutPanel1.Enabled = enable;
            flowLayoutPanel2.Enabled = enable;
            btnRefresh.Enabled = enable;
            flowLayoutPanel3.Enabled = !enable;
            groupBoxDisplay.Enabled = !enable;
        }
    }
}
