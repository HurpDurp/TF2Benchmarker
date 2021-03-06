﻿namespace TF2_Benchmarker
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
            this.components = new System.ComponentModel.Container();
            this.txt_launchoptions = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.gb_gameoptions = new System.Windows.Forms.GroupBox();
            this.rb_dxnone = new System.Windows.Forms.RadioButton();
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
            this.btn_clearfps = new System.Windows.Forms.Button();
            this.txt_configaddvalue = new System.Windows.Forms.TextBox();
            this.btn_configadditem = new System.Windows.Forms.Button();
            this.lbl_configadditem = new System.Windows.Forms.Label();
            this.txt_configaddname = new System.Windows.Forms.TextBox();
            this.rb_customconfig = new System.Windows.Forms.RadioButton();
            this.rb_defaultconfig = new System.Windows.Forms.RadioButton();
            this.btn_loadconfig = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tp_log = new System.Windows.Forms.TabPage();
            this.lb_log = new System.Windows.Forms.ListBox();
            this.tp_benchmarkcvars = new System.Windows.Forms.TabPage();
            this.lv_benchmarkcvars = new System.Windows.Forms.ListView();
            this.tp_fpsconfig = new System.Windows.Forms.TabPage();
            this.tp_results = new System.Windows.Forms.TabPage();
            this.btn_clearresults = new System.Windows.Forms.Button();
            this.btn_exportresults = new System.Windows.Forms.Button();
            this.lv_results = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_runtwice = new System.Windows.Forms.CheckBox();
            this.btn_clearbench = new System.Windows.Forms.Button();
            this.lbl_demoname = new System.Windows.Forms.Label();
            this.txt_demoname = new System.Windows.Forms.TextBox();
            this.txt_benchmarkval = new System.Windows.Forms.TextBox();
            this.btn_benchadditem = new System.Windows.Forms.Button();
            this.btn_loadbenchconfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_benchcommand = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.WorkerThread = new System.ComponentModel.BackgroundWorker();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_runbaseline = new System.Windows.Forms.Button();
            this.gb_gameoptions.SuspendLayout();
            this.gb_config.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tp_log.SuspendLayout();
            this.tp_benchmarkcvars.SuspendLayout();
            this.tp_fpsconfig.SuspendLayout();
            this.tp_results.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_launchoptions
            // 
            this.txt_launchoptions.Location = new System.Drawing.Point(9, 117);
            this.txt_launchoptions.Name = "txt_launchoptions";
            this.txt_launchoptions.Size = new System.Drawing.Size(416, 20);
            this.txt_launchoptions.TabIndex = 10;
            this.txt_launchoptions.TextChanged += new System.EventHandler(this.txt_launchoptions_TextChanged);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(12, 12);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "&Start";
            this.toolTip.SetToolTip(this.btn_start, "Start the benchmark and run a baseline if needed.");
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // gb_gameoptions
            // 
            this.gb_gameoptions.Controls.Add(this.rb_dxnone);
            this.gb_gameoptions.Controls.Add(this.lbl_dxver);
            this.gb_gameoptions.Controls.Add(this.rb_dx98);
            this.gb_gameoptions.Controls.Add(this.rb_dx95);
            this.gb_gameoptions.Controls.Add(this.rb_dx90);
            this.gb_gameoptions.Controls.Add(this.rb_dx81);
            this.gb_gameoptions.Controls.Add(this.rb_dx8);
            this.gb_gameoptions.Controls.Add(this.lbl_tf2path);
            this.gb_gameoptions.Controls.Add(this.btn_tfpath);
            this.gb_gameoptions.Controls.Add(this.lbl_launchoptions);
            this.gb_gameoptions.Controls.Add(this.txt_launchoptions);
            this.gb_gameoptions.Location = new System.Drawing.Point(12, 41);
            this.gb_gameoptions.Name = "gb_gameoptions";
            this.gb_gameoptions.Size = new System.Drawing.Size(431, 147);
            this.gb_gameoptions.TabIndex = 3;
            this.gb_gameoptions.TabStop = false;
            this.gb_gameoptions.Text = "Game Options";
            // 
            // rb_dxnone
            // 
            this.rb_dxnone.AutoSize = true;
            this.rb_dxnone.Location = new System.Drawing.Point(329, 72);
            this.rb_dxnone.Name = "rb_dxnone";
            this.rb_dxnone.Size = new System.Drawing.Size(81, 17);
            this.rb_dxnone.TabIndex = 4;
            this.rb_dxnone.TabStop = true;
            this.rb_dxnone.Text = "Unchanged";
            this.rb_dxnone.UseVisualStyleBackColor = true;
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
            this.rb_dx98.Location = new System.Drawing.Point(265, 72);
            this.rb_dx98.Name = "rb_dx98";
            this.rb_dx98.Size = new System.Drawing.Size(58, 17);
            this.rb_dx98.TabIndex = 4;
            this.rb_dx98.TabStop = true;
            this.rb_dx98.Text = "DX 9.8";
            this.rb_dx98.UseVisualStyleBackColor = true;
            this.rb_dx98.CheckedChanged += new System.EventHandler(this.rb_dx98_CheckedChanged);
            // 
            // rb_dx95
            // 
            this.rb_dx95.AutoSize = true;
            this.rb_dx95.Location = new System.Drawing.Point(201, 72);
            this.rb_dx95.Name = "rb_dx95";
            this.rb_dx95.Size = new System.Drawing.Size(58, 17);
            this.rb_dx95.TabIndex = 4;
            this.rb_dx95.TabStop = true;
            this.rb_dx95.Text = "DX 9.5";
            this.rb_dx95.UseVisualStyleBackColor = true;
            this.rb_dx95.CheckedChanged += new System.EventHandler(this.rb_dx95_CheckedChanged);
            // 
            // rb_dx90
            // 
            this.rb_dx90.AutoSize = true;
            this.rb_dx90.Location = new System.Drawing.Point(137, 72);
            this.rb_dx90.Name = "rb_dx90";
            this.rb_dx90.Size = new System.Drawing.Size(58, 17);
            this.rb_dx90.TabIndex = 4;
            this.rb_dx90.TabStop = true;
            this.rb_dx90.Text = "DX 9.0";
            this.rb_dx90.UseVisualStyleBackColor = true;
            this.rb_dx90.CheckedChanged += new System.EventHandler(this.rb_dx90_CheckedChanged);
            // 
            // rb_dx81
            // 
            this.rb_dx81.AutoSize = true;
            this.rb_dx81.Location = new System.Drawing.Point(73, 72);
            this.rb_dx81.Name = "rb_dx81";
            this.rb_dx81.Size = new System.Drawing.Size(58, 17);
            this.rb_dx81.TabIndex = 4;
            this.rb_dx81.TabStop = true;
            this.rb_dx81.Text = "DX 8.1";
            this.rb_dx81.UseVisualStyleBackColor = true;
            this.rb_dx81.CheckedChanged += new System.EventHandler(this.rb_dx81_CheckedChanged);
            // 
            // rb_dx8
            // 
            this.rb_dx8.AutoSize = true;
            this.rb_dx8.Location = new System.Drawing.Point(9, 72);
            this.rb_dx8.Name = "rb_dx8";
            this.rb_dx8.Size = new System.Drawing.Size(58, 17);
            this.rb_dx8.TabIndex = 4;
            this.rb_dx8.TabStop = true;
            this.rb_dx8.Text = "DX 8.0";
            this.rb_dx8.UseVisualStyleBackColor = true;
            this.rb_dx8.CheckedChanged += new System.EventHandler(this.rb_dx8_CheckedChanged);
            // 
            // lbl_tf2path
            // 
            this.lbl_tf2path.AutoSize = true;
            this.lbl_tf2path.Location = new System.Drawing.Point(90, 24);
            this.lbl_tf2path.MaximumSize = new System.Drawing.Size(330, 0);
            this.lbl_tf2path.Name = "lbl_tf2path";
            this.lbl_tf2path.Size = new System.Drawing.Size(43, 13);
            this.lbl_tf2path.TabIndex = 5;
            this.lbl_tf2path.Text = "Not Set";
            // 
            // btn_tfpath
            // 
            this.btn_tfpath.Location = new System.Drawing.Point(9, 19);
            this.btn_tfpath.Name = "btn_tfpath";
            this.btn_tfpath.Size = new System.Drawing.Size(75, 23);
            this.btn_tfpath.TabIndex = 3;
            this.btn_tfpath.Text = "TF2 &Path";
            this.toolTip.SetToolTip(this.btn_tfpath, "Set the path to the Team Fortress 2 folder.");
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
            this.lv_commands.Size = new System.Drawing.Size(434, 428);
            this.lv_commands.TabIndex = 27;
            this.lv_commands.UseCompatibleStateImageBehavior = false;
            this.lv_commands.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lv_commands_ItemCheck);
            // 
            // gb_config
            // 
            this.gb_config.Controls.Add(this.btn_clearfps);
            this.gb_config.Controls.Add(this.txt_configaddvalue);
            this.gb_config.Controls.Add(this.btn_configadditem);
            this.gb_config.Controls.Add(this.lbl_configadditem);
            this.gb_config.Controls.Add(this.txt_configaddname);
            this.gb_config.Controls.Add(this.rb_customconfig);
            this.gb_config.Controls.Add(this.rb_defaultconfig);
            this.gb_config.Controls.Add(this.btn_loadconfig);
            this.gb_config.Location = new System.Drawing.Point(12, 194);
            this.gb_config.Name = "gb_config";
            this.gb_config.Size = new System.Drawing.Size(431, 115);
            this.gb_config.TabIndex = 4;
            this.gb_config.TabStop = false;
            this.gb_config.Text = "Base FPS Config";
            // 
            // btn_clearfps
            // 
            this.btn_clearfps.Location = new System.Drawing.Point(193, 39);
            this.btn_clearfps.Name = "btn_clearfps";
            this.btn_clearfps.Size = new System.Drawing.Size(75, 23);
            this.btn_clearfps.TabIndex = 14;
            this.btn_clearfps.Text = "Clear";
            this.toolTip.SetToolTip(this.btn_clearfps, "Clear out the current FPS config.");
            this.btn_clearfps.UseVisualStyleBackColor = true;
            this.btn_clearfps.Click += new System.EventHandler(this.btn_clearfps_Click);
            // 
            // txt_configaddvalue
            // 
            this.txt_configaddvalue.Location = new System.Drawing.Point(278, 87);
            this.txt_configaddvalue.Name = "txt_configaddvalue";
            this.txt_configaddvalue.Size = new System.Drawing.Size(65, 20);
            this.txt_configaddvalue.TabIndex = 16;
            this.txt_configaddvalue.Text = "Value";
            this.txt_configaddvalue.Enter += new System.EventHandler(this.txt_configaddvalue_Enter);
            this.txt_configaddvalue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_configaddvalue_KeyDown);
            // 
            // btn_configadditem
            // 
            this.btn_configadditem.Location = new System.Drawing.Point(349, 85);
            this.btn_configadditem.Name = "btn_configadditem";
            this.btn_configadditem.Size = new System.Drawing.Size(75, 23);
            this.btn_configadditem.TabIndex = 17;
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
            this.txt_configaddname.TabIndex = 15;
            this.txt_configaddname.Text = "Name";
            this.txt_configaddname.Enter += new System.EventHandler(this.txt_configaddname_Enter);
            this.txt_configaddname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_configaddname_KeyDown);
            // 
            // rb_customconfig
            // 
            this.rb_customconfig.AutoSize = true;
            this.rb_customconfig.Location = new System.Drawing.Point(9, 42);
            this.rb_customconfig.Name = "rb_customconfig";
            this.rb_customconfig.Size = new System.Drawing.Size(93, 17);
            this.rb_customconfig.TabIndex = 11;
            this.rb_customconfig.TabStop = true;
            this.rb_customconfig.Text = "Custom Config";
            this.rb_customconfig.UseVisualStyleBackColor = true;
            this.rb_customconfig.CheckedChanged += new System.EventHandler(this.rb_customconfig_CheckedChanged);
            // 
            // rb_defaultconfig
            // 
            this.rb_defaultconfig.AutoSize = true;
            this.rb_defaultconfig.Location = new System.Drawing.Point(9, 19);
            this.rb_defaultconfig.Name = "rb_defaultconfig";
            this.rb_defaultconfig.Size = new System.Drawing.Size(59, 17);
            this.rb_defaultconfig.TabIndex = 11;
            this.rb_defaultconfig.TabStop = true;
            this.rb_defaultconfig.Text = "Default";
            this.toolTip.SetToolTip(this.rb_defaultconfig, "Use the default TF2 configuration.");
            this.rb_defaultconfig.UseVisualStyleBackColor = true;
            this.rb_defaultconfig.CheckedChanged += new System.EventHandler(this.rb_defaultconfig_CheckedChanged);
            // 
            // btn_loadconfig
            // 
            this.btn_loadconfig.Location = new System.Drawing.Point(108, 39);
            this.btn_loadconfig.Name = "btn_loadconfig";
            this.btn_loadconfig.Size = new System.Drawing.Size(79, 23);
            this.btn_loadconfig.TabIndex = 13;
            this.btn_loadconfig.Text = "Load";
            this.toolTip.SetToolTip(this.btn_loadconfig, "Load an FPS config to run with the benchmark.");
            this.btn_loadconfig.UseVisualStyleBackColor = true;
            this.btn_loadconfig.Click += new System.EventHandler(this.btn_loadconfig_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tp_log);
            this.tabControl.Controls.Add(this.tp_benchmarkcvars);
            this.tabControl.Controls.Add(this.tp_fpsconfig);
            this.tabControl.Controls.Add(this.tp_results);
            this.tabControl.Location = new System.Drawing.Point(449, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(437, 449);
            this.tabControl.TabIndex = 25;
            // 
            // tp_log
            // 
            this.tp_log.Controls.Add(this.lb_log);
            this.tp_log.Location = new System.Drawing.Point(4, 22);
            this.tp_log.Name = "tp_log";
            this.tp_log.Size = new System.Drawing.Size(429, 423);
            this.tp_log.TabIndex = 2;
            this.tp_log.Text = "Log";
            this.tp_log.UseVisualStyleBackColor = true;
            // 
            // lb_log
            // 
            this.lb_log.FormattingEnabled = true;
            this.lb_log.Location = new System.Drawing.Point(0, 2);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(427, 420);
            this.lb_log.TabIndex = 27;
            // 
            // tp_benchmarkcvars
            // 
            this.tp_benchmarkcvars.Controls.Add(this.lv_benchmarkcvars);
            this.tp_benchmarkcvars.Location = new System.Drawing.Point(4, 22);
            this.tp_benchmarkcvars.Name = "tp_benchmarkcvars";
            this.tp_benchmarkcvars.Padding = new System.Windows.Forms.Padding(3);
            this.tp_benchmarkcvars.Size = new System.Drawing.Size(429, 423);
            this.tp_benchmarkcvars.TabIndex = 1;
            this.tp_benchmarkcvars.Text = "Benchmark List";
            this.tp_benchmarkcvars.UseVisualStyleBackColor = true;
            // 
            // lv_benchmarkcvars
            // 
            this.lv_benchmarkcvars.Location = new System.Drawing.Point(-1, -1);
            this.lv_benchmarkcvars.Name = "lv_benchmarkcvars";
            this.lv_benchmarkcvars.Size = new System.Drawing.Size(434, 428);
            this.lv_benchmarkcvars.TabIndex = 27;
            this.lv_benchmarkcvars.UseCompatibleStateImageBehavior = false;
            // 
            // tp_fpsconfig
            // 
            this.tp_fpsconfig.Controls.Add(this.lv_commands);
            this.tp_fpsconfig.Location = new System.Drawing.Point(4, 22);
            this.tp_fpsconfig.Name = "tp_fpsconfig";
            this.tp_fpsconfig.Padding = new System.Windows.Forms.Padding(3);
            this.tp_fpsconfig.Size = new System.Drawing.Size(429, 423);
            this.tp_fpsconfig.TabIndex = 0;
            this.tp_fpsconfig.Text = "FPS Config";
            this.tp_fpsconfig.UseVisualStyleBackColor = true;
            // 
            // tp_results
            // 
            this.tp_results.Controls.Add(this.btn_clearresults);
            this.tp_results.Controls.Add(this.btn_exportresults);
            this.tp_results.Controls.Add(this.lv_results);
            this.tp_results.Location = new System.Drawing.Point(4, 22);
            this.tp_results.Margin = new System.Windows.Forms.Padding(2);
            this.tp_results.Name = "tp_results";
            this.tp_results.Size = new System.Drawing.Size(429, 423);
            this.tp_results.TabIndex = 3;
            this.tp_results.Text = "Results";
            this.tp_results.UseVisualStyleBackColor = true;
            // 
            // btn_clearresults
            // 
            this.btn_clearresults.Location = new System.Drawing.Point(84, 397);
            this.btn_clearresults.Name = "btn_clearresults";
            this.btn_clearresults.Size = new System.Drawing.Size(75, 23);
            this.btn_clearresults.TabIndex = 29;
            this.btn_clearresults.Text = "Clear";
            this.toolTip.SetToolTip(this.btn_clearresults, "Clear the results list.");
            this.btn_clearresults.UseVisualStyleBackColor = true;
            this.btn_clearresults.Click += new System.EventHandler(this.btn_clearresults_Click);
            // 
            // btn_exportresults
            // 
            this.btn_exportresults.Location = new System.Drawing.Point(3, 397);
            this.btn_exportresults.Name = "btn_exportresults";
            this.btn_exportresults.Size = new System.Drawing.Size(75, 23);
            this.btn_exportresults.TabIndex = 28;
            this.btn_exportresults.Text = "Export";
            this.toolTip.SetToolTip(this.btn_exportresults, "Export the current results list.");
            this.btn_exportresults.UseVisualStyleBackColor = true;
            this.btn_exportresults.Click += new System.EventHandler(this.btn_exportresults_Click);
            // 
            // lv_results
            // 
            this.lv_results.Location = new System.Drawing.Point(-1, -1);
            this.lv_results.Name = "lv_results";
            this.lv_results.Size = new System.Drawing.Size(434, 395);
            this.lv_results.TabIndex = 27;
            this.lv_results.UseCompatibleStateImageBehavior = false;
            this.lv_results.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lv_results_ColumnWidthChanging);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_runtwice);
            this.groupBox1.Controls.Add(this.btn_clearbench);
            this.groupBox1.Controls.Add(this.lbl_demoname);
            this.groupBox1.Controls.Add(this.txt_demoname);
            this.groupBox1.Controls.Add(this.txt_benchmarkval);
            this.groupBox1.Controls.Add(this.btn_benchadditem);
            this.groupBox1.Controls.Add(this.btn_loadbenchconfig);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_benchcommand);
            this.groupBox1.Location = new System.Drawing.Point(12, 315);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 146);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Benchmark";
            // 
            // cb_runtwice
            // 
            this.cb_runtwice.AutoSize = true;
            this.cb_runtwice.Checked = true;
            this.cb_runtwice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_runtwice.Location = new System.Drawing.Point(175, 23);
            this.cb_runtwice.Name = "cb_runtwice";
            this.cb_runtwice.Size = new System.Drawing.Size(78, 17);
            this.cb_runtwice.TabIndex = 20;
            this.cb_runtwice.Text = "Run Twice";
            this.toolTip.SetToolTip(this.cb_runtwice, "Run the benchmark twice per command, and discard the first result for consistency" +
        ". Recommended to leave this on.");
            this.cb_runtwice.UseVisualStyleBackColor = true;
            // 
            // btn_clearbench
            // 
            this.btn_clearbench.Location = new System.Drawing.Point(94, 19);
            this.btn_clearbench.Name = "btn_clearbench";
            this.btn_clearbench.Size = new System.Drawing.Size(75, 23);
            this.btn_clearbench.TabIndex = 19;
            this.btn_clearbench.Text = "Clear";
            this.toolTip.SetToolTip(this.btn_clearbench, "Clear the list of commands to benchmark.");
            this.btn_clearbench.UseVisualStyleBackColor = true;
            this.btn_clearbench.Click += new System.EventHandler(this.btn_clearbench_Click);
            // 
            // lbl_demoname
            // 
            this.lbl_demoname.AutoSize = true;
            this.lbl_demoname.Location = new System.Drawing.Point(6, 99);
            this.lbl_demoname.Name = "lbl_demoname";
            this.lbl_demoname.Size = new System.Drawing.Size(66, 13);
            this.lbl_demoname.TabIndex = 23;
            this.lbl_demoname.Text = "Demo Name";
            // 
            // txt_demoname
            // 
            this.txt_demoname.Location = new System.Drawing.Point(9, 115);
            this.txt_demoname.Name = "txt_demoname";
            this.txt_demoname.Size = new System.Drawing.Size(415, 20);
            this.txt_demoname.TabIndex = 24;
            this.txt_demoname.TextChanged += new System.EventHandler(this.txt_demoname_TextChanged);
            // 
            // txt_benchmarkval
            // 
            this.txt_benchmarkval.Location = new System.Drawing.Point(278, 66);
            this.txt_benchmarkval.Name = "txt_benchmarkval";
            this.txt_benchmarkval.Size = new System.Drawing.Size(65, 20);
            this.txt_benchmarkval.TabIndex = 22;
            this.txt_benchmarkval.Text = "Value";
            this.txt_benchmarkval.Enter += new System.EventHandler(this.txt_benchmarkval_Enter);
            this.txt_benchmarkval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_benchmarkval_KeyDown);
            // 
            // btn_benchadditem
            // 
            this.btn_benchadditem.Location = new System.Drawing.Point(349, 64);
            this.btn_benchadditem.Name = "btn_benchadditem";
            this.btn_benchadditem.Size = new System.Drawing.Size(75, 23);
            this.btn_benchadditem.TabIndex = 23;
            this.btn_benchadditem.Text = "Add";
            this.btn_benchadditem.UseVisualStyleBackColor = true;
            this.btn_benchadditem.Click += new System.EventHandler(this.btn_benchadditem_Click);
            // 
            // btn_loadbenchconfig
            // 
            this.btn_loadbenchconfig.Location = new System.Drawing.Point(9, 19);
            this.btn_loadbenchconfig.Name = "btn_loadbenchconfig";
            this.btn_loadbenchconfig.Size = new System.Drawing.Size(79, 23);
            this.btn_loadbenchconfig.TabIndex = 18;
            this.btn_loadbenchconfig.Text = "Load Commands";
            this.toolTip.SetToolTip(this.btn_loadbenchconfig, "Load a file containing commands to benchmark.");
            this.btn_loadbenchconfig.UseVisualStyleBackColor = true;
            this.btn_loadbenchconfig.Click += new System.EventHandler(this.btn_loadbenchconfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Add Command";
            // 
            // txt_benchcommand
            // 
            this.txt_benchcommand.Location = new System.Drawing.Point(9, 66);
            this.txt_benchcommand.Name = "txt_benchcommand";
            this.txt_benchcommand.Size = new System.Drawing.Size(263, 20);
            this.txt_benchcommand.TabIndex = 21;
            this.txt_benchcommand.Text = "Name";
            this.txt_benchcommand.Enter += new System.EventHandler(this.txt_benchcommand_Enter);
            this.txt_benchcommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_benchcommand_KeyDown);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(187, 12);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(82, 23);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Save Settings";
            this.toolTip.SetToolTip(this.btn_save, "Save the game path, launch options, and demo name to load later.");
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // WorkerThread
            // 
            this.WorkerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerThread_DoWork);
            this.WorkerThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.WorkerThread_ProgressChanged);
            this.WorkerThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerThread_RunWorkerCompleted);
            // 
            // btn_runbaseline
            // 
            this.btn_runbaseline.Location = new System.Drawing.Point(93, 12);
            this.btn_runbaseline.Name = "btn_runbaseline";
            this.btn_runbaseline.Size = new System.Drawing.Size(88, 23);
            this.btn_runbaseline.TabIndex = 1;
            this.btn_runbaseline.Text = "Run &Baseline";
            this.toolTip.SetToolTip(this.btn_runbaseline, "Only run a baseline benchmark, without any commands.");
            this.btn_runbaseline.UseVisualStyleBackColor = true;
            this.btn_runbaseline.Click += new System.EventHandler(this.btn_runbaseline_Click);
            // 
            // Benchmarker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 472);
            this.Controls.Add(this.btn_runbaseline);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.gb_config);
            this.Controls.Add(this.gb_gameoptions);
            this.Controls.Add(this.btn_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Benchmarker";
            this.Text = "TF2 Bench ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Benchmarker_FormClosing);
            this.gb_gameoptions.ResumeLayout(false);
            this.gb_gameoptions.PerformLayout();
            this.gb_config.ResumeLayout(false);
            this.gb_config.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tp_log.ResumeLayout(false);
            this.tp_benchmarkcvars.ResumeLayout(false);
            this.tp_fpsconfig.ResumeLayout(false);
            this.tp_results.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txt_launchoptions;
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
        private System.Windows.Forms.TabPage tp_log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_benchadditem;
        private System.Windows.Forms.Button btn_loadbenchconfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_benchcommand;
        private System.Windows.Forms.ListBox lb_log;
        private System.Windows.Forms.TextBox txt_benchmarkval;
        private System.Windows.Forms.TabPage tp_results;
        private System.Windows.Forms.ListView lv_results;
        private System.Windows.Forms.RadioButton rb_dxnone;
        private System.Windows.Forms.Label lbl_demoname;
        private System.Windows.Forms.TextBox txt_demoname;
        private System.Windows.Forms.Button btn_clearbench;
        private System.Windows.Forms.Button btn_clearfps;
        private System.Windows.Forms.Button btn_save;
        private System.ComponentModel.BackgroundWorker WorkerThread;
        private System.Windows.Forms.CheckBox cb_runtwice;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btn_exportresults;
        private System.Windows.Forms.Button btn_clearresults;
        private System.Windows.Forms.Button btn_runbaseline;
    }
}

