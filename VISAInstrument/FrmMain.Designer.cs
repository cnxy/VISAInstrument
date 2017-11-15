namespace VISAInstrument
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbtRS232 = new System.Windows.Forms.RadioButton();
            this.cboRS232 = new System.Windows.Forms.ComboBox();
            this.rbtUSB = new System.Windows.Forms.RadioButton();
            this.cboUSB = new System.Windows.Forms.ComboBox();
            this.rbtGPIB = new System.Windows.Forms.RadioButton();
            this.cboGPIB = new System.Windows.Forms.ComboBox();
            this.rbtLAN = new System.Windows.Forms.RadioButton();
            this.cboLAN = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCommand = new System.Windows.Forms.Label();
            this.cboCommand = new System.Windows.Forms.ComboBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.lblParity = new System.Windows.Forms.Label();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.lblFlowControl = new System.Windows.Forms.Label();
            this.cboFlowControl = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBoxDisplay.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 397F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.groupBoxDisplay, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel3, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel4, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(607, 463);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // groupBoxDisplay
            // 
            this.tableLayoutPanel.SetColumnSpan(this.groupBoxDisplay, 4);
            this.groupBoxDisplay.Controls.Add(this.txtDisplay);
            this.groupBoxDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDisplay.Enabled = false;
            this.groupBoxDisplay.Location = new System.Drawing.Point(3, 177);
            this.groupBoxDisplay.Name = "groupBoxDisplay";
            this.groupBoxDisplay.Size = new System.Drawing.Size(601, 283);
            this.groupBoxDisplay.TabIndex = 2;
            this.groupBoxDisplay.TabStop = false;
            this.groupBoxDisplay.Text = "显示";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDisplay.Location = new System.Drawing.Point(3, 17);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDisplay.Size = new System.Drawing.Size(595, 263);
            this.txtDisplay.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rbtRS232);
            this.flowLayoutPanel1.Controls.Add(this.cboRS232);
            this.flowLayoutPanel1.Controls.Add(this.rbtUSB);
            this.flowLayoutPanel1.Controls.Add(this.cboUSB);
            this.flowLayoutPanel1.Controls.Add(this.rbtGPIB);
            this.flowLayoutPanel1.Controls.Add(this.cboGPIB);
            this.flowLayoutPanel1.Controls.Add(this.rbtLAN);
            this.flowLayoutPanel1.Controls.Add(this.cboLAN);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(391, 53);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // rbtRS232
            // 
            this.rbtRS232.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtRS232.AutoSize = true;
            this.rbtRS232.Location = new System.Drawing.Point(3, 5);
            this.rbtRS232.Name = "rbtRS232";
            this.rbtRS232.Size = new System.Drawing.Size(53, 16);
            this.rbtRS232.TabIndex = 2;
            this.rbtRS232.Text = "RS232";
            this.rbtRS232.UseVisualStyleBackColor = true;
            this.rbtRS232.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // cboRS232
            // 
            this.cboRS232.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboRS232.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRS232.FormattingEnabled = true;
            this.cboRS232.Location = new System.Drawing.Point(62, 3);
            this.cboRS232.Name = "cboRS232";
            this.cboRS232.Size = new System.Drawing.Size(136, 20);
            this.cboRS232.TabIndex = 3;
            // 
            // rbtUSB
            // 
            this.rbtUSB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtUSB.AutoSize = true;
            this.rbtUSB.Location = new System.Drawing.Point(204, 5);
            this.rbtUSB.Name = "rbtUSB";
            this.rbtUSB.Size = new System.Drawing.Size(41, 16);
            this.rbtUSB.TabIndex = 4;
            this.rbtUSB.Text = "USB";
            this.rbtUSB.UseVisualStyleBackColor = true;
            this.rbtUSB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // cboUSB
            // 
            this.cboUSB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboUSB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUSB.FormattingEnabled = true;
            this.cboUSB.Location = new System.Drawing.Point(251, 3);
            this.cboUSB.Name = "cboUSB";
            this.cboUSB.Size = new System.Drawing.Size(136, 20);
            this.cboUSB.TabIndex = 5;
            // 
            // rbtGPIB
            // 
            this.rbtGPIB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtGPIB.AutoSize = true;
            this.rbtGPIB.Location = new System.Drawing.Point(3, 31);
            this.rbtGPIB.Name = "rbtGPIB";
            this.rbtGPIB.Size = new System.Drawing.Size(53, 16);
            this.rbtGPIB.TabIndex = 6;
            this.rbtGPIB.Text = "GPIB ";
            this.rbtGPIB.UseVisualStyleBackColor = true;
            this.rbtGPIB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // cboGPIB
            // 
            this.cboGPIB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboGPIB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGPIB.FormattingEnabled = true;
            this.cboGPIB.Location = new System.Drawing.Point(62, 29);
            this.cboGPIB.Name = "cboGPIB";
            this.cboGPIB.Size = new System.Drawing.Size(136, 20);
            this.cboGPIB.TabIndex = 7;
            // 
            // rbtLAN
            // 
            this.rbtLAN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtLAN.AutoSize = true;
            this.rbtLAN.Location = new System.Drawing.Point(204, 31);
            this.rbtLAN.Name = "rbtLAN";
            this.rbtLAN.Size = new System.Drawing.Size(41, 16);
            this.rbtLAN.TabIndex = 8;
            this.rbtLAN.Text = "LAN";
            this.rbtLAN.UseVisualStyleBackColor = true;
            this.rbtLAN.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // cboLAN
            // 
            this.cboLAN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboLAN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLAN.FormattingEnabled = true;
            this.cboLAN.Location = new System.Drawing.Point(251, 29);
            this.cboLAN.Name = "cboLAN";
            this.cboLAN.Size = new System.Drawing.Size(136, 20);
            this.cboLAN.TabIndex = 9;
            // 
            // flowLayoutPanel3
            // 
            this.tableLayoutPanel.SetColumnSpan(this.flowLayoutPanel3, 3);
            this.flowLayoutPanel3.Controls.Add(this.lblCommand);
            this.flowLayoutPanel3.Controls.Add(this.cboCommand);
            this.flowLayoutPanel3.Controls.Add(this.btnWrite);
            this.flowLayoutPanel3.Controls.Add(this.btnRead);
            this.flowLayoutPanel3.Controls.Add(this.btnQuery);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Enabled = false;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 140);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(496, 31);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // lblCommand
            // 
            this.lblCommand.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(3, 8);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(29, 12);
            this.lblCommand.TabIndex = 0;
            this.lblCommand.Text = "命令";
            // 
            // cboCommand
            // 
            this.cboCommand.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboCommand.FormattingEnabled = true;
            this.cboCommand.Items.AddRange(new object[] {
            "*IDN?",
            "*RST",
            "*CLS",
            "*ESE",
            "*ESE?",
            "*ESR?",
            "*OPC",
            "*OPC?",
            "*PSC",
            "*PSC?",
            "*SRE",
            "*SRE?",
            "*STB?",
            "*SAV",
            "*RCL"});
            this.cboCommand.Location = new System.Drawing.Point(38, 4);
            this.cboCommand.Name = "cboCommand";
            this.cboCommand.Size = new System.Drawing.Size(277, 20);
            this.cboCommand.TabIndex = 1;
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnWrite.Location = new System.Drawing.Point(321, 3);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(53, 23);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "写入";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRead.Location = new System.Drawing.Point(380, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(53, 23);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnQuery.Location = new System.Drawing.Point(439, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(53, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "询问";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel.SetColumnSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Controls.Add(this.lblBaudRate);
            this.flowLayoutPanel2.Controls.Add(this.cboBaudRate);
            this.flowLayoutPanel2.Controls.Add(this.lblParity);
            this.flowLayoutPanel2.Controls.Add(this.cboParity);
            this.flowLayoutPanel2.Controls.Add(this.lblStopBits);
            this.flowLayoutPanel2.Controls.Add(this.cboStopBits);
            this.flowLayoutPanel2.Controls.Add(this.lblDataBits);
            this.flowLayoutPanel2.Controls.Add(this.cboDataBits);
            this.flowLayoutPanel2.Controls.Add(this.lblFlowControl);
            this.flowLayoutPanel2.Controls.Add(this.cboFlowControl);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 77);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(428, 57);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(3, 7);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(41, 12);
            this.lblBaudRate.TabIndex = 0;
            this.lblBaudRate.Text = "波特率";
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Location = new System.Drawing.Point(50, 3);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(81, 20);
            this.cboBaudRate.TabIndex = 1;
            // 
            // lblParity
            // 
            this.lblParity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new System.Drawing.Point(137, 7);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(65, 12);
            this.lblParity.TabIndex = 0;
            this.lblParity.Text = "奇偶校验位";
            // 
            // cboParity
            // 
            this.cboParity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(208, 3);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(81, 20);
            this.cboParity.TabIndex = 2;
            // 
            // lblStopBits
            // 
            this.lblStopBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new System.Drawing.Point(295, 7);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(41, 12);
            this.lblStopBits.TabIndex = 0;
            this.lblStopBits.Text = "停止位";
            // 
            // cboStopBits
            // 
            this.cboStopBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(342, 3);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(81, 20);
            this.cboStopBits.TabIndex = 3;
            // 
            // lblDataBits
            // 
            this.lblDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new System.Drawing.Point(3, 33);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(41, 12);
            this.lblDataBits.TabIndex = 0;
            this.lblDataBits.Text = "数据位";
            // 
            // cboDataBits
            // 
            this.cboDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(50, 29);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(81, 20);
            this.cboDataBits.TabIndex = 4;
            // 
            // lblFlowControl
            // 
            this.lblFlowControl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFlowControl.AutoSize = true;
            this.lblFlowControl.Location = new System.Drawing.Point(137, 33);
            this.lblFlowControl.Name = "lblFlowControl";
            this.lblFlowControl.Size = new System.Drawing.Size(65, 12);
            this.lblFlowControl.TabIndex = 0;
            this.lblFlowControl.Text = "控制流    ";
            // 
            // cboFlowControl
            // 
            this.cboFlowControl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboFlowControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFlowControl.FormattingEnabled = true;
            this.cboFlowControl.Location = new System.Drawing.Point(208, 29);
            this.cboFlowControl.Name = "cboFlowControl";
            this.cboFlowControl.Size = new System.Drawing.Size(81, 20);
            this.cboFlowControl.TabIndex = 5;
            // 
            // flowLayoutPanel4
            // 
            this.tableLayoutPanel.SetColumnSpan(this.flowLayoutPanel4, 2);
            this.flowLayoutPanel4.Controls.Add(this.btnRefresh);
            this.flowLayoutPanel4.Controls.Add(this.btnOpen);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(400, 18);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(99, 53);
            this.flowLayoutPanel4.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(2, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(93, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(1, 28);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(1);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(94, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // FrmMain
            // 
            this.AcceptButton = this.btnWrite;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 463);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VISA仪器控制";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.groupBoxDisplay.ResumeLayout(false);
            this.groupBoxDisplay.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbtRS232;
        private System.Windows.Forms.ComboBox cboRS232;
        private System.Windows.Forms.RadioButton rbtUSB;
        private System.Windows.Forms.ComboBox cboUSB;
        private System.Windows.Forms.RadioButton rbtGPIB;
        private System.Windows.Forms.ComboBox cboGPIB;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.ComboBox cboCommand;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBoxDisplay;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.RadioButton rbtLAN;
        private System.Windows.Forms.ComboBox cboLAN;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.Label lblFlowControl;
        private System.Windows.Forms.ComboBox cboFlowControl;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.ComboBox cboBaudRate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button btnRefresh;
    }
}

