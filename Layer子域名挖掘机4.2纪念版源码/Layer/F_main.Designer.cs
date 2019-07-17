namespace Layer
{
    partial class F_main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_main));
            this.lbl_state = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开网站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制域名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仅导域名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_ports = new System.Windows.Forms.TextBox();
            this.cmb_dns = new System.Windows.Forms.ComboBox();
            this.txt_domain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_control = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.chb_Enumerate = new System.Windows.Forms.CheckBox();
            this.chb_Serverapi = new System.Windows.Forms.CheckBox();
            this.chb_scanports = new System.Windows.Forms.CheckBox();
            this.save_path = new System.Windows.Forms.SaveFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_thread = new System.Windows.Forms.ComboBox();
            this.chb_webstate = new System.Windows.Forms.CheckBox();
            this.复制所选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LV_result = new Layer.ListViewNF();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_state
            // 
            this.lbl_state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_state.AutoSize = true;
            this.lbl_state.Location = new System.Drawing.Point(11, 674);
            this.lbl_state.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_state.Name = "lbl_state";
            this.lbl_state.Size = new System.Drawing.Size(438, 15);
            this.lbl_state.TabIndex = 4;
            this.lbl_state.Text = "状态：等待启动，如果无法获取结果请安装.NET4.0才能正常使用";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开网站ToolStripMenuItem,
            this.复制域名ToolStripMenuItem,
            this.复制IPToolStripMenuItem,
            this.复制所选ToolStripMenuItem,
            this.仅导域名ToolStripMenuItem,
            this.导出列表ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 160);
            // 
            // 打开网站ToolStripMenuItem
            // 
            this.打开网站ToolStripMenuItem.Name = "打开网站ToolStripMenuItem";
            this.打开网站ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.打开网站ToolStripMenuItem.Text = "打开网站";
            this.打开网站ToolStripMenuItem.Click += new System.EventHandler(this.打开网站ToolStripMenuItem_Click);
            // 
            // 复制域名ToolStripMenuItem
            // 
            this.复制域名ToolStripMenuItem.Name = "复制域名ToolStripMenuItem";
            this.复制域名ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.复制域名ToolStripMenuItem.Text = "复制域名";
            this.复制域名ToolStripMenuItem.Click += new System.EventHandler(this.复制域名ToolStripMenuItem_Click);
            // 
            // 复制IPToolStripMenuItem
            // 
            this.复制IPToolStripMenuItem.Name = "复制IPToolStripMenuItem";
            this.复制IPToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.复制IPToolStripMenuItem.Text = "复制 IP";
            this.复制IPToolStripMenuItem.Click += new System.EventHandler(this.复制IPToolStripMenuItem_Click);
            // 
            // 仅导域名ToolStripMenuItem
            // 
            this.仅导域名ToolStripMenuItem.Name = "仅导域名ToolStripMenuItem";
            this.仅导域名ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.仅导域名ToolStripMenuItem.Text = "导出域名";
            this.仅导域名ToolStripMenuItem.Click += new System.EventHandler(this.仅导域名ToolStripMenuItem_Click);
            // 
            // 导出列表ToolStripMenuItem
            // 
            this.导出列表ToolStripMenuItem.Name = "导出列表ToolStripMenuItem";
            this.导出列表ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.导出列表ToolStripMenuItem.Text = "导出全部";
            this.导出列表ToolStripMenuItem.Click += new System.EventHandler(this.导出列表ToolStripMenuItem_Click);
            // 
            // txt_ports
            // 
            this.txt_ports.Location = new System.Drawing.Point(302, 11);
            this.txt_ports.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ports.Name = "txt_ports";
            this.txt_ports.Size = new System.Drawing.Size(123, 25);
            this.txt_ports.TabIndex = 14;
            this.txt_ports.Text = "80,443";
            // 
            // cmb_dns
            // 
            this.cmb_dns.FormattingEnabled = true;
            this.cmb_dns.Items.AddRange(new object[] {
            "114DNS-114.114.114.114",
            "阿里DNS-223.5.5.5",
            "DNSPOD-119.29.29.29",
            "直接输入DNS服务器地址"});
            this.cmb_dns.Location = new System.Drawing.Point(670, 10);
            this.cmb_dns.Name = "cmb_dns";
            this.cmb_dns.Size = new System.Drawing.Size(188, 23);
            this.cmb_dns.TabIndex = 13;
            // 
            // txt_domain
            // 
            this.txt_domain.Location = new System.Drawing.Point(65, 11);
            this.txt_domain.Margin = new System.Windows.Forms.Padding(2);
            this.txt_domain.Name = "txt_domain";
            this.txt_domain.Size = new System.Drawing.Size(184, 25);
            this.txt_domain.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(259, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "端口：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(632, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "DNS：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "域名：";
            // 
            // bt_control
            // 
            this.bt_control.Location = new System.Drawing.Point(1121, 8);
            this.bt_control.Margin = new System.Windows.Forms.Padding(2);
            this.bt_control.Name = "bt_control";
            this.bt_control.Size = new System.Drawing.Size(84, 25);
            this.bt_control.TabIndex = 8;
            this.bt_control.Text = "启 动";
            this.bt_control.UseVisualStyleBackColor = true;
            this.bt_control.Click += new System.EventHandler(this.bt_control_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(1125, 674);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(99, 15);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "访问Seay博客";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // chb_Enumerate
            // 
            this.chb_Enumerate.AutoSize = true;
            this.chb_Enumerate.Checked = true;
            this.chb_Enumerate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Enumerate.Location = new System.Drawing.Point(986, 12);
            this.chb_Enumerate.Name = "chb_Enumerate";
            this.chb_Enumerate.Size = new System.Drawing.Size(59, 19);
            this.chb_Enumerate.TabIndex = 16;
            this.chb_Enumerate.Text = "枚举";
            this.chb_Enumerate.UseVisualStyleBackColor = true;
            // 
            // chb_Serverapi
            // 
            this.chb_Serverapi.AutoSize = true;
            this.chb_Serverapi.Checked = true;
            this.chb_Serverapi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Serverapi.Location = new System.Drawing.Point(1051, 12);
            this.chb_Serverapi.Name = "chb_Serverapi";
            this.chb_Serverapi.Size = new System.Drawing.Size(59, 19);
            this.chb_Serverapi.TabIndex = 16;
            this.chb_Serverapi.Text = "接口";
            this.chb_Serverapi.UseVisualStyleBackColor = true;
            // 
            // chb_scanports
            // 
            this.chb_scanports.AutoSize = true;
            this.chb_scanports.Checked = true;
            this.chb_scanports.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_scanports.Location = new System.Drawing.Point(430, 12);
            this.chb_scanports.Name = "chb_scanports";
            this.chb_scanports.Size = new System.Drawing.Size(89, 19);
            this.chb_scanports.TabIndex = 16;
            this.chb_scanports.Text = "扫描端口";
            this.chb_scanports.UseVisualStyleBackColor = true;
            // 
            // save_path
            // 
            this.save_path.DefaultExt = "txt";
            this.save_path.FileName = "子域名列表-Layer";
            this.save_path.Filter = "*.txt|TXT|*.*|所有";
            this.save_path.InitialDirectory = "Desktop";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(863, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "线程：";
            // 
            // cmb_thread
            // 
            this.cmb_thread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_thread.FormattingEnabled = true;
            this.cmb_thread.Items.AddRange(new object[] {
            "全速",
            "50",
            "100",
            "150",
            "200",
            "300"});
            this.cmb_thread.Location = new System.Drawing.Point(907, 10);
            this.cmb_thread.Name = "cmb_thread";
            this.cmb_thread.Size = new System.Drawing.Size(61, 23);
            this.cmb_thread.TabIndex = 19;
            // 
            // chb_webstate
            // 
            this.chb_webstate.AutoSize = true;
            this.chb_webstate.Checked = true;
            this.chb_webstate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_webstate.Location = new System.Drawing.Point(525, 12);
            this.chb_webstate.Name = "chb_webstate";
            this.chb_webstate.Size = new System.Drawing.Size(104, 19);
            this.chb_webstate.TabIndex = 16;
            this.chb_webstate.Text = "服务器信息";
            this.chb_webstate.UseVisualStyleBackColor = true;
            // 
            // 复制所选ToolStripMenuItem
            // 
            this.复制所选ToolStripMenuItem.Name = "复制所选ToolStripMenuItem";
            this.复制所选ToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
            this.复制所选ToolStripMenuItem.Text = "复制所选项";
            this.复制所选ToolStripMenuItem.Click += new System.EventHandler(this.复制所选ToolStripMenuItem_Click);
            // 
            // LV_result
            // 
            this.LV_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LV_result.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader5});
            this.LV_result.ContextMenuStrip = this.contextMenuStrip1;
            this.LV_result.FullRowSelect = true;
            this.LV_result.GridLines = true;
            this.LV_result.Location = new System.Drawing.Point(11, 46);
            this.LV_result.Margin = new System.Windows.Forms.Padding(2);
            this.LV_result.Name = "LV_result";
            this.LV_result.Size = new System.Drawing.Size(1216, 621);
            this.LV_result.TabIndex = 5;
            this.LV_result.UseCompatibleStateImageBehavior = false;
            this.LV_result.View = System.Windows.Forms.View.Details;
            this.LV_result.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LV_result_ColumnClick);
            this.LV_result.DoubleClick += new System.EventHandler(this.LV_result_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "域名";
            this.columnHeader2.Width = 220;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "解析IP";
            this.columnHeader3.Width = 280;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "开放端口";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "WEB服务器";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "网站状态";
            this.columnHeader6.Width = 180;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "来源";
            this.columnHeader5.Width = 120;
            // 
            // F_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1236, 697);
            this.Controls.Add(this.cmb_thread);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chb_webstate);
            this.Controls.Add(this.chb_scanports);
            this.Controls.Add(this.chb_Serverapi);
            this.Controls.Add(this.chb_Enumerate);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.txt_ports);
            this.Controls.Add(this.cmb_dns);
            this.Controls.Add(this.txt_domain);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_control);
            this.Controls.Add(this.LV_result);
            this.Controls.Add(this.lbl_state);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "F_main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Layer子域名挖掘机4.2 纪念版";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_main_FormClosing);
            this.Load += new System.EventHandler(this.F_main_Load);
            this.SizeChanged += new System.EventHandler(this.F_main_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lbl_state;
        public ListViewNF LV_result;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TextBox txt_ports;
        private System.Windows.Forms.ComboBox cmb_dns;
        private System.Windows.Forms.TextBox txt_domain;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_control;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chb_Enumerate;
        private System.Windows.Forms.CheckBox chb_Serverapi;
        private System.Windows.Forms.CheckBox chb_scanports;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开网站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制域名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制IPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仅导域名ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog save_path;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_thread;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.CheckBox chb_webstate;
        private System.Windows.Forms.ToolStripMenuItem 复制所选ToolStripMenuItem;
    }
}

