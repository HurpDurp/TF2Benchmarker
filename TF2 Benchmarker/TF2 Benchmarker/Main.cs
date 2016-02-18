﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TF2_Benchmarker
{
    struct Cvar
    {
        public string Command, Value;
        public List<Cvar> MultiLineCommand;
        // Horrible hack to avoid restructuring a bunch of stuff,
        // This struct probably needs to be made a class.

        public Cvar(string c, string v)
        {
            Command = c;
            Value = v;
            MultiLineCommand = null;
        }

        public Cvar(string c, List<Cvar> m)
        {
            Command = c;
            Value = null;
            MultiLineCommand = m;
        }
    }

    public partial class Benchmarker : Form
    {
        string TFPath;
        
        #region Forms

        public Benchmarker()
        {
            InitializeComponent();

            TFPath = "";

            // Radio buttons
            rb_dxnone.Checked = true;
            rb_defaultconfig.Checked = true;

            // Listviews
            lv_commands.View = View.Details;
            lv_benchmarkcvars.View = View.Details;
            lv_results.View = View.Details;

            lv_commands.CheckBoxes = true;
            lv_commands.Columns.Add("Command", 350);
            lv_commands.Columns.Add("Value", -2);

            lv_benchmarkcvars.CheckBoxes = true;
            lv_benchmarkcvars.Columns.Add("Command", 350);
            lv_benchmarkcvars.Columns.Add("Value", -2);

            lv_results.Columns.Add("Demo File", 150);
            lv_results.Columns.Add("FPS");
            lv_results.Columns.Add("Variability", 75);
            lv_results.Columns.Add("Comment", -2);

            // Hotkey
            RegisterHotKey(this.Handle, 0, (int)KeyModifier.Shift, Keys.F10.GetHashCode());

            // BackgroundWorker
            WorkerThread.WorkerSupportsCancellation = true;
            WorkerThread.WorkerReportsProgress = true;

            Log("Started");

            InitializeConfig();
        }

        private void Benchmarker_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Remove temporary directory
            
            if (TFPath.Length > 0)
            {
                string path = TFPath + @"\tf\custom\tfbench";
                try
                {
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                }
                catch { }
            }

            // Unregister hotkey
            UnregisterHotKey(this.Handle, 0);
        }

        #endregion

        #region Buttons

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (!WorkerThread.IsBusy && TFPath.Length > 0)
            {
                btn_start.Text = "&Stop";

                // Get FPS config commands
                var FPSConfig = new List<Cvar>();
                foreach (ListViewItem item in lv_commands.Items)
                {
                    if (item.Checked)
                        FPSConfig.Add(new Cvar(item.Text, item.SubItems[1].Text));
                }

                // Get benchmark commands
                var BenchmarkCvars = new List<Cvar>();
                foreach (ListViewItem item in lv_benchmarkcvars.Items)
                {
                    if (item.Checked)
                        if (item.Tag == null)
                            BenchmarkCvars.Add(new Cvar(item.Text, item.SubItems[1].Text));
                        else
                            BenchmarkCvars.Add(new Cvar(item.Text, item.Tag as List<Cvar>));
                }

                Object[] Args = new Object[] { FPSConfig, BenchmarkCvars };

                WorkerThread.RunWorkerAsync(Args);
            }
            else
            {
                if (WorkerThread.IsBusy)
                    Log("Stopping benchmark after this run.");
                else
                    Log("TF2 path not set, aborting.");

                btn_start.Text = "&Start";
                WorkerThread.CancelAsync();
            }
        }

        private void btn_tfpath_Click(object sender, EventArgs e)
        {
            var TFFolderDialog = new FolderBrowserDialog();
            TFFolderDialog.Description = "Select Team Fortress 2 Directory";
            
            if (TFFolderDialog.ShowDialog() == DialogResult.OK)
            {
                var hl2exe = new FileInfo(TFFolderDialog.SelectedPath + @"\hl2.exe");

                if (hl2exe.Exists)
                {
                    TFPath = TFFolderDialog.SelectedPath;
                    lbl_tf2path.Text = TFFolderDialog.SelectedPath;

                    Log("TF2 path set");
                }
                else
                    MessageBox.Show("Invalid directory, could not locate hl2.exe");
            }
        }

        private void btn_loadconfig_Click(object sender, EventArgs e)
        {
            var ConfigDialog = new OpenFileDialog();

            ConfigDialog.Title = "Open FPS Config";
            ConfigDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";

            if (ConfigDialog.ShowDialog() == DialogResult.OK)
            {
                var FPSConfig = new List<string>();

                // Read FPS config
                string noComments;

                using (StreamReader sr = new StreamReader(ConfigDialog.FileName))
                {
                    while (sr.Peek() != -1)
                    {
                        noComments = StripComments(sr.ReadLine());
                        noComments = noComments.Replace("\"", "");

                        if (noComments.Trim().Length > 0)
                            FPSConfig.Add(noComments);
                    }
                }

                // Populate command listview
                foreach (string s in FPSConfig)
                {
                    try
                    {
                        string cvar = s.Substring(0, s.IndexOf(" ")).Trim();
                        string val = s.Substring(s.IndexOf(" ") + 1).Trim();

                        var lv = new ListViewItem(cvar);
                        lv.SubItems.Add(val);
                        lv.Checked = true;

                        lv_commands.Items.Add(lv);
                    }
                    catch
                    {
                        Log("Could not load command: " + s);
                    }
                }

                Log("FPS config loaded");
            }
        }

        private void btn_configadditem_Click(object sender, EventArgs e)
        {
            if (txt_configaddname.Text != "Command" && txt_configaddvalue.Text != "Value"
                && txt_configaddname.Text.Length > 0 && txt_configaddvalue.Text.Length > 0)
            {
                ListViewItem item = new ListViewItem(txt_configaddname.Text);
                item.SubItems.Add(txt_configaddvalue.Text);
                item.Checked = true;
                
                txt_configaddname.Clear();
                txt_configaddvalue.Clear();

                Log("Command \"" + item.Text + " " + item.SubItems[1].Text + "\" added");
            }
        }

        private void btn_loadbenchconfig_Click(object sender, EventArgs e)
        {
            var ConfigDialog = new OpenFileDialog();

            ConfigDialog.Title = "Open Benchmark Config";
            ConfigDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ConfigDialog.ShowDialog() == DialogResult.OK)
            {
                var BenchConfig = new List<string>();

                // Read Bench config
                string noComments;

                using (StreamReader sr = new StreamReader(ConfigDialog.FileName))
                {
                    while (sr.Peek() != -1)
                    {
                        noComments = StripComments(sr.ReadLine());
                        noComments = noComments.Replace("\"", "");

                        if (noComments.Trim().Length > 0)
                            BenchConfig.Add(noComments);
                    }
                }

                // Parse the benchmark input
                foreach (string s in BenchConfig)
                {
                    if (!s.Contains("|"))
                    {
                        string temp = s.Trim();
                        
                        try
                        {
                            string cvar = s.Substring(0, s.IndexOf(" ")).Trim();
                            string val = s.Substring(s.IndexOf(" ") + 1).Trim();

                            var lv = new ListViewItem(cvar);
                            lv.SubItems.Add(val);
                            lv.Checked = true;

                            lv_benchmarkcvars.Items.Add(lv);
                        }
                        catch
                        {
                            Log("Could not parse command: " + s);
                        }
                    }
                    else
                    {
                        List<Cvar> MultiLineCmd = new List<Cvar>();

                        string[] cmds = s.Split('|');
                        string name = "";

                        for(int i = 0; i < cmds.Length; i++)
                        {
                            cmds[i] = cmds[i].Trim();

                            if (i == 0)
                            {
                                // First part of the multiline contains the name
                                name = cmds[i].Trim();
                                continue;
                            }

                            try
                            {
                                string command = cmds[i].Substring(0, cmds[i].IndexOf(" "));
                                string val = cmds[i].Substring(cmds[i].IndexOf(" ") + 1);

                                MultiLineCmd.Add(new Cvar(command, val));
                            }
                            catch
                            {
                                Log("Could not parse custom command: " + name);
                            }
                        }

                        if (MultiLineCmd.Count > 0)
                        {
                            ListViewItem lv = new ListViewItem();
                            lv.Text = "Custom: " + name;
                            lv.Tag = MultiLineCmd;
                            lv.Checked = true;

                            lv_benchmarkcvars.Items.Add(lv);
                        }
                    }
                }

                Log("Benchmark config added");
            }
        }

        private void btn_benchadditem_Click(object sender, EventArgs e)
        {
            if (txt_benchcommand.Text != "Command" && txt_benchmarkval.Text != "Value"
                && txt_benchcommand.Text.Length > 0 && txt_benchmarkval.Text.Length > 0)
            {
                ListViewItem item = new ListViewItem(txt_benchcommand.Text);
                item.SubItems.Add(txt_benchmarkval.Text);
                item.Checked = true;

                lv_benchmarkcvars.Items.Add(item);

                txt_benchcommand.Clear();
                txt_benchmarkval.Clear();

                Log("Command \"" + item.Text + " " + item.SubItems[1].Text + "\" added");
            }
        }

        private void btn_clearbench_Click(object sender, EventArgs e)
        {
            lv_benchmarkcvars.Items.Clear();
        }

        private void btn_clearfps_Click(object sender, EventArgs e)
        {
            lv_commands.Items.Clear();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            IniFile ConfigFile = new IniFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TFBenchmark.ini");
            
            if (TFPath.Length > 0)
                ConfigFile.IniWriteValue("General", "TFPath", TFPath);

            ConfigFile.IniWriteValue("General", "LaunchOptions", txt_launchoptions.Text);
            ConfigFile.IniWriteValue("General", "DemoName", txt_demoname.Text);

            Log("Settings saved.");
        }

        #endregion

        #region Radio Buttons

        private void rb_defaultconfig_CheckedChanged(object sender, EventArgs e)
        {
            btn_loadconfig.Enabled = false;
            lbl_configadditem.Enabled = false;
            txt_configaddname.Enabled = false;
            txt_configaddvalue.Enabled = false;
            btn_configadditem.Enabled = false;
            btn_clearfps.Enabled = false;
            
            lv_commands.Items.Clear();
        }

        private void rb_customconfig_CheckedChanged(object sender, EventArgs e)
        {
            btn_loadconfig.Enabled = true;
            lbl_configadditem.Enabled = true;
            txt_configaddname.Enabled = true;
            txt_configaddvalue.Enabled = true;
            btn_configadditem.Enabled = true;
            btn_clearfps.Enabled = true;
        }

        #endregion

        #region UI

        private void txt_configaddname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_configaddvalue.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txt_configaddvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_configadditem.Focus();
                btn_configadditem_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void txt_benchcommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_benchmarkval.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void tb_benchmarkval_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_benchadditem.Focus();
                btn_benchadditem_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region Worker Thread

        private void WorkerThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!File.Exists(TFPath + @"\tf\" + txt_demoname.Text))
            {
                WorkerThread.ReportProgress(0, "Could not find demo file, aborting.");
                return;
            }

            WorkerThread.ReportProgress(0, "Starting...");
            PrepDirectory(TFPath);

            // Generate a baseline benchmark

            WorkerThread.ReportProgress(0, "Generating baseline benchmark...");
            
            string args;
            Object[] WorkerArgs = e.Argument as Object[];
            List<Cvar> FPSConfig = WorkerArgs[0] as List<Cvar>;
            List<Cvar> BenchCvars = WorkerArgs[1] as List<Cvar>;

            // Back up old config.cfg
            var config = new FileInfo(TFPath + @"\tf\cfg\config.cfg");
            if (config.Exists)
            {
                config.IsReadOnly = false;
                config.CopyTo(TFPath + @"\tf\cfg\config.cfg.bak", true);
                config.Delete();
            }
            
            // Generate a fresh config.cfg with the first run of the benchmark, and set directX level if we need to
            if (rb_defaultconfig.Checked)
            {
                // Use default config
                args = "-steam -game tf " + txt_launchoptions.Text + GetDxLevel() + " -default +timedemoquit " + txt_demoname.Text;

                FPSConfig.Add(new Cvar("host_writeconfig", "\"config\" full"));

                WriteCfg(FPSConfig, TFPath, false);
                StartBenchmark(args, TFPath, "Baseline");

                rb_dxnone.Checked = true;
                FPSConfig.Clear();
            }
            else
            {
                // Use fps config instead of defaults
                args = "-steam -game tf " + txt_launchoptions.Text + GetDxLevel() + " +timedemoquit " + txt_demoname.Text;
                
                WriteCfg(FPSConfig, TFPath);
                StartBenchmark(args, TFPath, "Baseline");
            }

            // Set it to read only, so we don't overwrite it while benchmarking
            config.Refresh();
            if (config.Exists)
                config.IsReadOnly = true;

            WorkerThread.ReportProgress(0, "Done.");

            // Main benchmark loop

            List<Cvar> ConfigToWrite;
            args = "-steam -game tf " + txt_launchoptions.Text + " +timedemoquit " + txt_demoname.Text;

            foreach (var item in BenchCvars)
            {
                if (WorkerThread.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                WorkerThread.ReportProgress(0, "Benchmarking " + PrintCvar(item) + "...");

                ConfigToWrite = MergeConfig(FPSConfig, item);

                PrepDirectory(TFPath);
                WriteCfg(ConfigToWrite, TFPath);

                StartBenchmark(args, TFPath, PrintCvar(item));

                WorkerThread.ReportProgress(0, "Done.");
            }

            // Clean up, delete our generated config
            config.Refresh();
            if (config.Exists)
            {
                config.IsReadOnly = false;
                config.Delete();
            }

            // Replace it with the original
            config = new FileInfo(TFPath + @"\tf\cfg\config.cfg.bak");
            if (config.Exists)
            {
                config.CopyTo(TFPath + @"\tf\cfg\config.cfg");
                config.Delete();
            }

            WorkerThread.ReportProgress(0, "Benchmark Complete.");
        }

        private void WorkerThread_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Log(e.UserState as String);
        }

        private void WorkerThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btn_start.Text = "&Start";
        }

        #endregion

        #region Benchmark Functions

        private void StartBenchmark(string args, string path, string comment)
        {
            StartGame(args, path);

            // Parse csv file

            FileInfo results = new FileInfo(path + @"\tf\sourcebench.csv");
            if (!results.Exists)
            {
                WorkerThread.ReportProgress(0, "Benchmark results file not found.");
                return;
            }

            using (TextFieldParser parser = new TextFieldParser(results.FullName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = false;
                parser.TrimWhiteSpace = true;

                parser.ReadFields();
                while (parser.PeekChars(1) != null)
                {
                    ListViewItem li = new ListViewItem();

                    string[] row = parser.ReadFields();
                    for (int i = 0; i < row.Length; i++)
                    {
                        if (i == 0)
                            li.Text = row[i];
                        else if (i == 1)
                            li.SubItems.Add(row[i]);
                        else if (i == 2)
                            li.SubItems.Add(row[i]);
                    }

                    li.SubItems.Add(comment);

                    if (lv_results.InvokeRequired)
                    {
                        lv_results.Invoke(new MethodInvoker(delegate
                        {
                            lv_results.Items.Add(li);
                        }));
                    }
                    else
                        lv_results.Items.Add(li);
                }
            }
        }

        private void StartGame(string args, string path)
        {
            // run game, wait until it finishes

            ProcessStartInfo TF2Proc = new ProcessStartInfo();
            TF2Proc.CreateNoWindow = true;
            TF2Proc.Arguments = args;
            TF2Proc.FileName = path + @"\hl2.exe";

            using (Process proc = Process.Start(TF2Proc))
            {
                proc.WaitForExit();
            }
        }

        private void PrepDirectory(string path)
        {
            // Clean up csv

            FileInfo benchcsv = new FileInfo(path + @"\tf\sourcebench.csv");
            if (benchcsv.Exists)
            {
                benchcsv.CopyTo("sourcebench.csv.bak", true);
                benchcsv.Delete();
            }

            // Create a directory for our autoexec if it doesn't already exist

            string cfgpath = path + @"\tf\custom\tfbench\cfg";
            Directory.CreateDirectory(cfgpath);

            // Remove old autoexec

            FileInfo autoexec = new FileInfo(cfgpath + @"\autoexec.cfg");
            if (autoexec.Exists)
                autoexec.Delete();
        }

        private void WriteCfg(List<Cvar> config, string path, bool format = true)
        {
            using (StreamWriter file = new StreamWriter(path + @"\tf\custom\tfbench\cfg\autoexec.cfg", false))
            {
                foreach (Cvar c in config)
                    if (format)
                        file.WriteLine(String.Format("{0} \"{1}\"", c.Command, c.Value));
                    else
                        file.WriteLine(PrintCvar(c));
            }
        }

        private List<Cvar> MergeConfig(List<Cvar> dest, Cvar c)
        {
            var tempconfig = new List<Cvar>(dest);
            bool added = false;

            if (c.Value != null)
            {
                for (int i = 0; i < tempconfig.Count; i++)
                {
                    if (tempconfig[i].Command == c.Command)
                    {
                        tempconfig[i] = c;
                        added = true;
                    }
                }

                if (!added)
                    tempconfig.Add(c);
            }
            else
            {
                List<Cvar> commands = c.MultiLineCommand;

                foreach (Cvar d in commands)
                {
                    added = false;

                    for (int i = 0; i < tempconfig.Count; i++)
                    {
                        if (tempconfig[i].Command == d.Command)
                        {
                            tempconfig[i] = d;
                            added = true;
                        }
                    }

                    if (!added)
                        tempconfig.Add(d);
                }
            }

            return tempconfig;
        }

        #endregion

        #region Helper Functions

        static string StripComments(string s)
        {
            var re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            return Regex.Replace(s, re, "$1");
        }

        private void Log(string message)
        {
            lb_log.Items.Add(String.Format("[{0}] {1}", DateTime.Now, message));
            lb_log.TopIndex = lb_log.Items.Count - 1; // Scroll to bottom
        }

        private string GetDxLevel()
        {
            string ret = "";
            if (rb_dx8.Checked)
                ret = " -dxlevel 80";
            else if (rb_dx81.Checked)
                ret = " -dxlevel 81";
            else if (rb_dx90.Checked)
                ret = " -dxlevel 90";
            else if (rb_dx95.Checked)
                ret = " -dxlevel 95";
            else if (rb_dx98.Checked)
                ret = " -dxlevel 98";

            return ret;
        }

        private string PrintCvar(Cvar c)
        {
            if (c.Value != null)
                return c.Command + " " + c.Value;
            else
                return c.Command;
        }

        #endregion

        #region Configuration File Handling

        private void InitializeConfig()
        {
            IniFile ConfigFile = new IniFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TFBenchmark.ini");

            if (!File.Exists(ConfigFile.path))
            {
                // Write a new config
                ConfigFile.IniWriteValue("General", "TFPath", "");
                ConfigFile.IniWriteValue("General", "LaunchOptions", "-novid -w 1920 -h 1080 -fullscreen");
                ConfigFile.IniWriteValue("General", "DemoName", "benchmark01.dem");
            }

            // Load config
            string path = ConfigFile.IniReadValue("General", "TFPath");

            if (path.Length > 0)
            {
                TFPath = path;
                lbl_tf2path.Text = path;
            }

            txt_launchoptions.Text = ConfigFile.IniReadValue("General", "LaunchOptions");
            txt_demoname.Text = ConfigFile.IniReadValue("General", "DemoName");

            Log("Settings loaded.");
        }

        #endregion

        #region Hotkey

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312 && WorkerThread.IsBusy)
            {
                System.Media.SystemSounds.Hand.Play();

                WorkerThread.CancelAsync();
                btn_start.Text = "&Start";
                Log("Stopping benchmark after this run.");
            }
        }
    }

    #endregion
}
