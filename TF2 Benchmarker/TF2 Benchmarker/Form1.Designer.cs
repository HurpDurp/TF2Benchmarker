namespace TF2_Benchmarker
{
    partial class Benchmarker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.gb_gameoptions = new System.Windows.Forms.GroupBox();
            this.lbl_dxver = new System.Windows.Forms.Label();
            this.rb_dx98 = new System.Windows.Forms.RadioButton();
            this.rb_dx95 = new System.Windows.Forms.RadioButton();
            this.rb_dx90 = new System.Windows.Forms.RadioButton();
            this.rb_dx81 = new System.Windows.Forms.RadioButton();
            this.rb_dx8 = new System.Windows.Forms.RadioButton();
            this.lbl_tf2path = new System.Windows.Forms.Label();
            this.btn_tfpath = new System.Windows.Forms.Button();
            this.lbl_launchoptions = new System.Windows.Forms.Label();
            this.lv_commands = new System.Windows.Forms.ListView();
            this.gb_config = new System.Windows.Forms.GroupBox();
            this.txt_configaddvalue = new System.Windows.Forms.TextBox();
            this.btn_configadditem = new System.Windows.Forms.Button();
            this.lbl_configadditem = new System.Windows.Forms.Label();
            this.txt_configaddname = new System.Windows.Forms.TextBox();
            this.rb_customconfig = new System.Windows.Forms.RadioButton();
            this.rb_defaultconfig = new System.Windows.Forms.RadioButton();
            this.lbl_configstatus = new System.Windows.Forms.Label();
            this.btn_loadconfig = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tp_benchmarkcvars = new System.Windows.Forms.TabPage();
            this.lv_benchmarkcvars = new System.Windows.Forms.ListView();
            this.tp_fpsconfig = new System.Windows.Forms.TabPage();
            this.gb_gameoptions.SuspendLayout();
            this.gb_config.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tp_benchmarkcvars.SuspendLayout();
            this.tp_fpsconfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 117);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(416, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "-novid -w 1920 -h 1080 -fullscreen";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(12, 12);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "&Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // gb_gameoptions
            // 
            this.gb_gameoptions.Controls.Add(this.lbl_dxver);
            this.gb_gameoptions.Controls.Add(this.rb_dx98);
            this.gb_gameoptions.Controls.Add(this.rb_dx95);
            this.gb_gameoptions.Controls.Add(this.rb_dx90);
            this.gb_gameoptions.Controls.Add(this.rb_dx81);
            this.gb_gameoptions.Controls.Add(this.rb_dx8);
            this.gb_gameoptions.Controls.Add(this.lbl_tf2path);
            this.gb_gameoptions.Controls.Add(this.btn_tfpath);
            this.gb_gameoptions.Controls.Add(this.lbl_launchoptions);
            this.gb_gameoptions.Controls.Add(this.textBox1);
            this.gb_gameoptions.Location = new System.Drawing.Point(12, 41);
            this.gb_gameoptions.Name = "gb_gameoptions";
            this.gb_gameoptions.Size = new System.Drawing.Size(431, 147);
            this.gb_gameoptions.TabIndex = 3;
            this.gb_gameoptions.TabStop = false;
            this.gb_gameoptions.Text = "Game Options";
            // 
            // lbl_dxver
            // 
            this.lbl_dxver.AutoSize = true;
            this.lbl_dxver.Location = new System.Drawing.Point(6, 56);
            this.lbl_dxver.Name = "lbl_dxver";
            this.lbl_dxver.Size = new System.Drawing.Size(80, 13);
            this.lbl_dxver.TabIndex = 11;
            this.lbl_dxver.Text = "DirectX Version";
            // 
            // rb_dx98
            // 
            this.rb_dx98.AutoSize = true;
            this.rb_dx98.Location = new System.Drawing.Point(247, 72);
            this.rb_dx98.Name = "rb_dx98";
            this.rb_dx98.Size = new System.Drawing.Size(58, 17);
            this.rb_dx98.TabIndex = 6;
            this.rb_dx98.TabStop = true;
            this.rb_dx98.Text = "DX 9.8";
            this.rb_dx98.UseVisualStyleBackColor = true;
            // 
            // rb_dx95
            // 
            this.rb_dx95.AutoSize = true;
            this.rb_dx95.Location = new System.Drawing.Point(183, 72);
            this.rb_dx95.Name = "rb_dx95";
            this.rb_dx95.Size = new System.Drawing.Size(58, 17);
            this.rb_dx95.TabIndex = 5;
            this.rb_dx95.TabStop = true;
            this.rb_dx95.Text = "DX 9.5";
            this.rb_dx95.UseVisualStyleBackColor = true;
            // 
            // rb_dx90
            // 
            this.rb_dx90.AutoSize = true;
            this.rb_dx90.Location = new System.Drawing.Point(128, 72);
            this.rb_dx90.Name = "rb_dx90";
            this.rb_dx90.Size = new System.Drawing.Size(49, 17);
            this.rb_dx90.TabIndex = 4;
            this.rb_dx90.TabStop = true;
            this.rb_dx90.Text = "DX 9";
            this.rb_dx90.UseVisualStyleBackColor = true;
            // 
            // rb_dx81
            // 
            this.rb_dx81.AutoSize = true;
            this.rb_dx81.Location = new System.Drawing.Point(64, 72);
            this.rb_dx81.Name = "rb_dx81";
            this.rb_dx81.Size = new System.Drawing.Size(58, 17);
            this.rb_dx81.TabIndex = 3;
            this.rb_dx81.TabStop = true;
            this.rb_dx81.Text = "DX 8.1";
            this.rb_dx81.UseVisualStyleBackColor = true;
            // 
            // rb_dx8
            // 
            this.rb_dx8.AutoSize = true;
            this.rb_dx8.Location = new System.Drawing.Point(9, 72);
            this.rb_dx8.Name = "rb_dx8";
            this.rb_dx8.Size = new System.Drawing.Size(49, 17);
            this.rb_dx8.TabIndex = 2;
            this.rb_dx8.TabStop = true;
            this.rb_dx8.Text = "DX 8";
            this.rb_dx8.UseVisualStyleBackColor = true;
            // 
            // lbl_tf2path
            // 
            this.lbl_tf2path.AutoSize = true;
            this.lbl_tf2path.Location = new System.Drawing.Point(90, 24);
            this.lbl_tf2path.MaximumSize = new System.Drawing.Size(330, 0);
            this.lbl_tf2path.Name = "lbl_tf2path";
            this.lbl_tf2path.Size = new System.Drawing.Size(63, 13);
            this.lbl_tf2path.TabIndex = 5;
            this.lbl_tf2path.Text = "Not Loaded";
            // 
            // btn_tfpath
            // 
            this.btn_tfpath.Location = new System.Drawing.Point(9, 19);
            this.btn_tfpath.Name = "btn_tfpath";
            this.btn_tfpath.Size = new System.Drawing.Size(75, 23);
            this.btn_tfpath.TabIndex = 1;
            this.btn_tfpath.Text = "TF2 Path";
            this.btn_tfpath.UseVisualStyleBackColor = true;
            this.btn_tfpath.Click += new System.EventHandler(this.btn_tfpath_Click);
            // 
            // lbl_launchoptions
            // 
            this.lbl_launchoptions.AutoSize = true;
            this.lbl_launchoptions.Location = new System.Drawing.Point(6, 101);
            this.lbl_launchoptions.Name = "lbl_launchoptions";
            this.lbl_launchoptions.Size = new System.Drawing.Size(82, 13);
            this.lbl_launchoptions.TabIndex = 2;
            this.lbl_launchoptions.Text = "Launch Options";
            // 
            // lv_commands
            // 
            this.lv_commands.Location = new System.Drawing.Point(-1, -1);
            this.lv_commands.Name = "lv_commands";
            this.lv_commands.Size = new System.Drawing.Size(431, 465);
            this.lv_commands.TabIndex = 5;
            this.lv_commands.UseCompatibleStateImageBehavior = false;
            // 
            // gb_config
            // 
            this.gb_config.Controls.Add(this.txt_configaddvalue);
            this.gb_config.Controls.Add(this.btn_configadditem);
            this.gb_config.Controls.Add(this.lbl_configadditem);
            this.gb_config.Controls.Add(this.txt_configaddname);
            this.gb_config.Controls.Add(this.rb_customconfig);
            this.gb_config.Controls.Add(this.rb_defaultconfig);
            this.gb_config.Controls.Add(this.lbl_configstatus);
            this.gb_config.Controls.Add(this.btn_loadconfig);
            this.gb_config.Location = new System.Drawing.Point(12, 194);
            this.gb_config.Name = "gb_config";
            this.gb_config.Size = new System.Drawing.Size(431, 115);
            this.gb_config.TabIndex = 4;
            this.gb_config.TabStop = false;
            this.gb_config.Text = "Config";
            // 
            // txt_configaddvalue
            // 
            this.txt_configaddvalue.Location = new System.Drawing.Point(278, 87);
            this.txt_configaddvalue.Name = "txt_configaddvalue";
            this.txt_configaddvalue.Size = new System.Drawing.Size(65, 20);
            this.txt_configaddvalue.TabIndex = 12;
            this.txt_configaddvalue.Text = "Value";
            this.txt_configaddvalue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_configaddvalue_KeyDown);
            // 
            // btn_configadditem
            // 
            this.btn_configadditem.Location = new System.Drawing.Point(349, 85);
            this.btn_configadditem.Name = "btn_configadditem";
            this.btn_configadditem.Size = new System.Drawing.Size(75, 23);
            this.btn_configadditem.TabIndex = 13;
            this.btn_configadditem.Text = "Add";
            this.btn_configadditem.UseVisualStyleBackColor = true;
            this.btn_configadditem.Click += new System.EventHandler(this.btn_configadditem_Click);
            // 
            // lbl_configadditem
            // 
            this.lbl_configadditem.AutoSize = true;
            this.lbl_configadditem.Location = new System.Drawing.Point(6, 71);
            this.lbl_configadditem.Name = "lbl_configadditem";
            this.lbl_configadditem.Size = new System.Drawing.Size(76, 13);
            this.lbl_configadditem.TabIndex = 17;
            this.lbl_configadditem.Text = "Add Command";
            // 
            // txt_configaddname
            // 
            this.txt_configaddname.Location = new System.Drawing.Point(9, 87);
            this.txt_configaddname.Name = "txt_configaddname";
            this.txt_configaddname.Size = new System.Drawing.Size(263, 20);
            this.txt_configaddname.TabIndex = 11;
            this.txt_configaddname.Text = "Name";
            this.txt_configaddname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_configaddname_KeyDown);
            // 
            // rb_customconfig
            // 
            this.rb_customconfig.AutoSize = true;
            this.rb_customconfig.Location = new System.Drawing.Point(9, 42);
            this.rb_customconfig.Name = "rb_customconfig";
            this.rb_customconfig.Size = new System.Drawing.Size(115, 17);
            this.rb_customconfig.TabIndex = 9;
            this.rb_customconfig.TabStop = true;
            this.rb_customconfig.Text = "Use Custom Config";
            this.rb_customconfig.UseVisualStyleBackColor = true;
            this.rb_customconfig.CheckedChanged += new System.EventHandler(this.rb_customconfig_CheckedChanged);
            // 
            // rb_defaultconfig
            // 
            this.rb_defaultconfig.AutoSize = true;
            this.rb_defaultconfig.Location = new System.Drawing.Point(9, 19);
            this.rb_defaultconfig.Name = "rb_defaultconfig";
            this.rb_defaultconfig.Size = new System.Drawing.Size(114, 17);
            this.rb_defaultconfig.TabIndex = 8;
            this.rb_defaultconfig.TabStop = true;
            this.rb_defaultconfig.Text = "Use Default Config";
            this.rb_defaultconfig.UseVisualStyleBackColor = true;
            this.rb_defaultconfig.CheckedChanged += new System.EventHandler(this.rb_defaultconfig_CheckedChanged);
            // 
            // lbl_configstatus
            // 
            this.lbl_configstatus.AutoSize = true;
            this.lbl_configstatus.Location = new System.Drawing.Point(215, 44);
            this.lbl_configstatus.MaximumSize = new System.Drawing.Size(330, 0);
            this.lbl_configstatus.Name = "lbl_configstatus";
            this.lbl_configstatus.Size = new System.Drawing.Size(59, 13);
            this.lbl_configstatus.TabIndex = 13;
            this.lbl_configstatus.Text = "Not loaded";
            // 
            // btn_loadconfig
            // 
            this.btn_loadconfig.Location = new System.Drawing.Point(130, 39);
            this.btn_loadconfig.Name = "btn_loadconfig";
            this.btn_loadconfig.Size = new System.Drawing.Size(79, 23);
            this.btn_loadconfig.TabIndex = 10;
            this.btn_loadconfig.Text = "Load Config";
            this.btn_loadconfig.UseVisualStyleBackColor = true;
            this.btn_loadconfig.Click += new System.EventHandler(this.btn_loadconfig_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tp_benchmarkcvars);
            this.tabControl.Controls.Add(this.tp_fpsconfig);
            this.tabControl.Location = new System.Drawing.Point(449, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(437, 486);
            this.tabControl.TabIndex = 15;
            // 
            // tp_benchmarkcvars
            // 
            this.tp_benchmarkcvars.Controls.Add(this.lv_benchmarkcvars);
            this.tp_benchmarkcvars.Location = new System.Drawing.Point(4, 22);
            this.tp_benchmarkcvars.Name = "tp_benchmarkcvars";
            this.tp_benchmarkcvars.Padding = new System.Windows.Forms.Padding(3);
            this.tp_benchmarkcvars.Size = new System.Drawing.Size(429, 460);
            this.tp_benchmarkcvars.TabIndex = 1;
            this.tp_benchmarkcvars.Text = "Benchmark Convars";
            this.tp_benchmarkcvars.UseVisualStyleBackColor = true;
            // 
            // lv_benchmarkcvars
            // 
            this.lv_benchmarkcvars.Location = new System.Drawing.Point(-2, -1);
            this.lv_benchmarkcvars.Name = "lv_benchmarkcvars";
            this.lv_benchmarkcvars.Size = new System.Drawing.Size(435, 465);
            this.lv_benchmarkcvars.TabIndex = 8;
            this.lv_benchmarkcvars.UseCompatibleStateImageBehavior = false;
            // 
            // tp_fpsconfig
            // 
            this.tp_fpsconfig.Controls.Add(this.lv_commands);
            this.tp_fpsconfig.Location = new System.Drawing.Point(4, 22);
            this.tp_fpsconfig.Name = "tp_fpsconfig";
            this.tp_fpsconfig.Padding = new System.Windows.Forms.Padding(3);
            this.tp_fpsconfig.Size = new System.Drawing.Size(429, 460);
            this.tp_fpsconfig.TabIndex = 0;
            this.tp_fpsconfig.Text = "FPS Config";
            this.tp_fpsconfig.UseVisualStyleBackColor = true;
            // 
            // Benchmarker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 510);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.gb_config);
            this.Controls.Add(this.gb_gameoptions);
            this.Controls.Add(this.btn_start);
            this.Name = "Benchmarker";
            this.Text = "TF2 Benchmarker";
            this.gb_gameoptions.ResumeLayout(false);
            this.gb_gameoptions.PerformLayout();
            this.gb_config.ResumeLayout(false);
            this.gb_config.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tp_benchmarkcvars.ResumeLayout(false);
            this.tp_fpsconfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.GroupBox gb_gameoptions;
        private System.Windows.Forms.Label lbl_launchoptions;
        private System.Windows.Forms.RadioButton rb_dx98;
        private System.Windows.Forms.RadioButton rb_dx95;
        private System.Windows.Forms.RadioButton rb_dx90;
        private System.Windows.Forms.RadioButton rb_dx81;
        private System.Windows.Forms.RadioButton rb_dx8;
        private System.Windows.Forms.Label lbl_tf2path;
        private System.Windows.Forms.Button btn_tfpath;
        private System.Windows.Forms.Label lbl_dxver;
        private System.Windows.Forms.GroupBox gb_config;
        private System.Windows.Forms.Label lbl_configstatus;
        private System.Windows.Forms.Button btn_loadconfig;
        private System.Windows.Forms.RadioButton rb_customconfig;
        private System.Windows.Forms.RadioButton rb_defaultconfig;
        private System.Windows.Forms.ListView lv_commands;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tp_fpsconfig;
        private System.Windows.Forms.TabPage tp_benchmarkcvars;
        private System.Windows.Forms.ListView lv_benchmarkcvars;
        private System.Windows.Forms.TextBox txt_configaddvalue;
        private System.Windows.Forms.Button btn_configadditem;
        private System.Windows.Forms.Label lbl_configadditem;
        private System.Windows.Forms.TextBox txt_configaddname;
    }
}

