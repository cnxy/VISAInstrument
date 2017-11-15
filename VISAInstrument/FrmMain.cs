using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VISAInstrument.Port;

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
            RadioButton radioButton = (RadioButton)sender;
            List<string> address = new List<string>();
            if(radioButton == rbtALL)
            {
                cboAll.Items.Clear();
                cboAll.Items.AddRange(PortUltility.FindAddresses());
            }
            else if(radioButton == rbtRS232)
            {
                cboRS232.Items.Clear();
                cboRS232.Items.AddRange(PortUltility.FindAddresses(PortType.RS232));
            }
            else if (radioButton == rbtUSB)
            {
                cboUSB.Items.Clear();
                cboUSB.Items.AddRange(PortUltility.FindAddresses(PortType.USB));
            }
            else if (radioButton == rbtGPIB)
            {
                cboGPIB.Items.Clear();
                cboGPIB.Items.AddRange(PortUltility.FindAddresses(PortType.GPIB));
            }
            else
            {
                cboLAN.Items.Clear();
                cboLAN.Items.AddRange(PortUltility.FindAddresses(PortType.LAN));
            }
        }

        private void cboGPIB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            rbtALL.Checked = true;
        }
    }
}
