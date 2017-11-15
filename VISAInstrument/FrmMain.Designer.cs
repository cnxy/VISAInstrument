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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbtALL = new System.Windows.Forms.RadioButton();
            this.cboAll = new System.Windows.Forms.ComboBox();
            this.rbtRS232 = new System.Windows.Forms.RadioButton();
            this.cboRS232 = new System.Windows.Forms.ComboBox();
            this.rbtUSB = new System.Windows.Forms.RadioButton();
            this.cboUSB = new System.Windows.Forms.ComboBox();
            this.rbtGPIB = new System.Windows.Forms.RadioButton();
            this.cboGPIB = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCommand = new System.Windows.Forms.Label();
            this.cboCommand = new System.Windows.Forms.ComboBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.rbtLAN = new System.Windows.Forms.RadioButton();
            this.cboLAN = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBoxDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.groupBoxDisplay, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel2, 0, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(598, 538);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rbtALL);
            this.flowLayoutPanel1.Controls.Add(this.cboAll);
            this.flowLayoutPanel1.Controls.Add(this.rbtRS232);
            this.flowLayoutPanel1.Controls.Add(this.cboRS232);
            this.flowLayoutPanel1.Controls.Add(this.rbtUSB);
            this.flowLayoutPanel1.Controls.Add(this.cboUSB);
            this.flowLayoutPanel1.Controls.Add(this.rbtGPIB);
            this.flowLayoutPanel1.Controls.Add(this.cboGPIB);
            this.flowLayoutPanel1.Controls.Add(this.rbtLAN);
            this.flowLayoutPanel1.Controls.Add(this.cboLAN);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(592, 53);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // rbtALL
            // 
            this.rbtALL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtALL.AutoSize = true;
            this.rbtALL.Location = new System.Drawing.Point(3, 5);
            this.rbtALL.Name = "rbtALL";
            this.rbtALL.Size = new System.Drawing.Size(47, 16);
            this.rbtALL.TabIndex = 0;
            this.rbtALL.Text = "所有";
            this.rbtALL.UseVisualStyleBackColor = true;
            this.rbtALL.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // cboAll
            // 
            this.cboAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboAll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAll.FormattingEnabled = true;
            this.cboAll.Location = new System.Drawing.Point(56, 3);
            this.cboAll.Name = "cboAll";
            this.cboAll.Size = new System.Drawing.Size(136, 20);
            this.cboAll.TabIndex = 1;
            // 
            // rbtRS232
            // 
            this.rbtRS232.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtRS232.AutoSize = true;
            this.rbtRS232.Location = new System.Drawing.Point(198, 5);
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
            this.cboRS232.Location = new System.Drawing.Point(257, 3);
            this.cboRS232.Name = "cboRS232";
            this.cboRS232.Size = new System.Drawing.Size(136, 20);
            this.cboRS232.TabIndex = 3;
            // 
            // rbtUSB
            // 
            this.rbtUSB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtUSB.AutoSize = true;
            this.rbtUSB.Location = new System.Drawing.Point(399, 5);
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
            this.cboUSB.Location = new System.Drawing.Point(446, 3);
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
            this.rbtGPIB.Size = new System.Drawing.Size(47, 16);
            this.rbtGPIB.TabIndex = 6;
            this.rbtGPIB.Text = "GPIB";
            this.rbtGPIB.UseVisualStyleBackColor = true;
            this.rbtGPIB.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // cboGPIB
            // 
            this.cboGPIB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboGPIB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGPIB.FormattingEnabled = true;
            this.cboGPIB.Location = new System.Drawing.Point(56, 29);
            this.cboGPIB.Name = "cboGPIB";
            this.cboGPIB.Size = new System.Drawing.Size(136, 20);
            this.cboGPIB.TabIndex = 7;
            this.cboGPIB.SelectedIndexChanged += new System.EventHandler(this.cboGPIB_SelectedIndexChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lblCommand);
            this.flowLayoutPanel2.Controls.Add(this.cboCommand);
            this.flowLayoutPanel2.Controls.Add(this.btnWrite);
            this.flowLayoutPanel2.Controls.Add(this.btnRead);
            this.flowLayoutPanel2.Controls.Add(this.btnQuery);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 87);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(592, 31);
            this.flowLayoutPanel2.TabIndex = 1;
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
            "*RST"});
            this.cboCommand.Location = new System.Drawing.Point(38, 4);
            this.cboCommand.Name = "cboCommand";
            this.cboCommand.Size = new System.Drawing.Size(306, 20);
            this.cboCommand.TabIndex = 1;
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnWrite.Location = new System.Drawing.Point(350, 3);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "写入";
            this.btnWrite.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRead.Location = new System.Drawing.Point(431, 3);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnQuery.Location = new System.Drawing.Point(512, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "询问";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // groupBoxDisplay
            // 
            this.groupBoxDisplay.Controls.Add(this.txtDisplay);
            this.groupBoxDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDisplay.Location = new System.Drawing.Point(3, 135);
            this.groupBoxDisplay.Name = "groupBoxDisplay";
            this.groupBoxDisplay.Size = new System.Drawing.Size(592, 400);
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
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size(586, 380);
            this.txtDisplay.TabIndex = 0;
            // 
            // rbtLAN
            // 
            this.rbtLAN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtLAN.AutoSize = true;
            this.rbtLAN.Location = new System.Drawing.Point(198, 31);
            this.rbtLAN.Name = "rbtLAN";
            this.rbtLAN.Size = new System.Drawing.Size(53, 16);
            this.rbtLAN.TabIndex = 8;
            this.rbtLAN.Text = "LAN  ";
            this.rbtLAN.UseVisualStyleBackColor = true;
            this.rbtLAN.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // cboLAN
            // 
            this.cboLAN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboLAN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLAN.FormattingEnabled = true;
            this.cboLAN.Location = new System.Drawing.Point(257, 29);
            this.cboLAN.Name = "cboLAN";
            this.cboLAN.Size = new System.Drawing.Size(136, 20);
            this.cboLAN.TabIndex = 9;
            this.cboLAN.SelectedIndexChanged += new System.EventHandler(this.cboGPIB_SelectedIndexChanged);
            // 
            // FrmMain
            // 
            this.AcceptButton = this.btnWrite;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 538);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VISA仪器控制";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBoxDisplay.ResumeLayout(false);
            this.groupBoxDisplay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbtALL;
        private System.Windows.Forms.ComboBox cboAll;
        private System.Windows.Forms.RadioButton rbtRS232;
        private System.Windows.Forms.ComboBox cboRS232;
        private System.Windows.Forms.RadioButton rbtUSB;
        private System.Windows.Forms.ComboBox cboUSB;
        private System.Windows.Forms.RadioButton rbtGPIB;
        private System.Windows.Forms.ComboBox cboGPIB;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.ComboBox cboCommand;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBoxDisplay;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.RadioButton rbtLAN;
        private System.Windows.Forms.ComboBox cboLAN;
    }
}

