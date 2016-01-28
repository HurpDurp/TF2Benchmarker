using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace TF2_Benchmarker
{
    public struct Cvar
    {
        public string Command;
        public string Value;
    }

    public partial class Benchmarker : Form
    {
        Dictionary<string, string> Settings;
        bool Benchmarking = false;
        
        public Benchmarker()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>();

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

            Log("Started");
        }

        #region Buttons

        private void btn_start_Click(object sender, EventArgs e)
        {
            string path;

            if (!Benchmarking && Settings.TryGetValue("TFPath", out path))
            {
                btn_start.Text = "&Stop";
                Benchmarking = true;

                Log("Starting...");

                PrepDirectory(path);
                
                // Generate a baseline benchmark

                Log("Generating baseline benchmark...");

                List<string> fpsconfig = new List<string>();
                string args;

                // Back up old config.cfg
                FileInfo config = new FileInfo(path + @"\tf\cfg\config.cfg");
                if (config.Exists)
                {
                    config.CopyTo(path + @"\tf\cfg\config.cfg.bak", true);
                    config.Delete();
                }

                // Generate a fresh config.cfg with the first run of the benchmark, and set directX level if we need to
                if (rb_defaultconfig.Checked)
                {
                    // Use default config
                    args = "-steam -game tf " + txt_launchoptions.Text + GetDxLevel() + " -default +timedemoquit " + txt_demoname.Text;

                    fpsconfig.Add("host_writeconfig \"config\" full");

                    WriteCfg(fpsconfig, path);
                    StartBenchmark(args, path, "Baseline");

                    fpsconfig.Clear();
                }
                else
                {
                    // Use fps config instead of defaults
                    args = "-steam -game tf " + txt_launchoptions.Text + GetDxLevel() + " +timedemoquit " + txt_demoname.Text;

                    foreach (ListViewItem item in lv_commands.Items)
                        if (item.Checked)
                            fpsconfig.Add(String.Format("{0} \"{1}\"", item.Text, item.SubItems[1].Text));

                    WriteCfg(fpsconfig, path);
                    StartBenchmark(args, path, "Baseline");
                }

                // Set it to read only, so we don't overwrite it while benchmarking
                config.Refresh();
                if (config.Exists)
                    config.IsReadOnly = true;

                Log("Done.");

                // Main benchmark loop

                List<string> ConfigToWrite;
                args = "-steam -game tf " + txt_launchoptions.Text + " +timedemoquit " + txt_demoname.Text;

                foreach (ListViewItem item in lv_benchmarkcvars.Items)
                {
                    if (!item.Checked || !Benchmarking)
                        continue;

                    string name;
                    if (item.Tag == null)
                        name = item.Text + " " + item.SubItems[1].Text;
                    else
                        name = item.Text;

                    Log("Benchmarking " + name + "...");

                    ConfigToWrite = MergeConfig(fpsconfig, item);

                    PrepDirectory(path);
                    WriteCfg(ConfigToWrite, path);
                    
                    StartBenchmark(args, path, name);
                    
                    Log("Done.");
                }

                // Clean up, delete our generated config
                config.Refresh();
                if (config.Exists)
                {
                    config.IsReadOnly = false;
                    config.Delete();
                }

                // Replace it with the original
                config = new FileInfo(path + @"\tf\cfg\config.cfg.bak");
                if (config.Exists)
                {
                    config.CopyTo(path + @"\tf\cfg\config.cfg");
                    config.Delete();
                }

                Log("Benchmark Complete.");

                btn_start.Text = "&Start";
                Benchmarking = false;
            }
            else
            {
                Log("Stopping benchmark");

                btn_start.Text = "&Start";
                Benchmarking = false;
            }
        }

        private void btn_tfpath_Click(object sender, EventArgs e)
        {
            var TFFolderDialog = new FolderBrowserDialog();
            TFFolderDialog.Description = "Select Team Fortress 2 Directory";
            
            if (TFFolderDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(TFFolderDialog.SelectedPath + @"\hl2.exe");

                if (f.Exists)
                {
                    Settings["TFPath"] = TFFolderDialog.SelectedPath;
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
                                Cvar c;
                                c.Command = cmds[i].Substring(0, cmds[i].IndexOf(" "));
                                c.Value = cmds[i].Substring(cmds[i].IndexOf(" ") + 1);

                                MultiLineCmd.Add(c);
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

        #region Benchmark Functions

        public void StartBenchmark(string args, string path, string comment)
        {
            StartGame(args, path);

            // Parse csv file

            FileInfo results = new FileInfo(path + @"\tf\sourcebench.csv");
            if (!results.Exists)
            {
                Log("Benchmark results file not found.");
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
                    lv_results.Items.Add(li);
                }
            }
        }

        public void StartGame(string args, string path)
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

        public void PrepDirectory(string path)
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

        public void WriteCfg(List<string> config, string path)
        {
            using (StreamWriter file = new StreamWriter(path + @"\tf\custom\tfbench\cfg\autoexec.cfg", false))
            {
                foreach (string line in config)
                    file.WriteLine(line);
            }
        }

        public List<string> MergeConfig(List<string> dest, ListViewItem item)
        {
            var tempconfig = new List<string>(dest);
            bool added = false;

            if (item.Tag == null)
            {
                for (int i = 0; i < tempconfig.Count; i++)
                {
                    if (tempconfig[i].Contains(item.Text))
                    {
                        tempconfig[i] = String.Format("{0} \"{1}\"", item.Text, item.SubItems[1].Text);
                        added = true;
                    }
                }

                if (!added)
                    tempconfig.Add(String.Format("{0} \"{1}\"", item.Text, item.SubItems[1].Text));
            }
            else
            {
                List<Cvar> commands = (List<Cvar>)item.Tag;

                foreach (Cvar c in commands)
                {
                    added = false;

                    for (int i = 0; i < tempconfig.Count; i++)
                    {
                        if (tempconfig[i].Contains(c.Command))
                        {
                            tempconfig[i] = String.Format("{0} \"{1}\"", c.Command, c.Value);
                            added = true;
                        }
                    }

                    if (!added)
                        tempconfig.Add(String.Format("{0} \"{1}\"", c.Command, c.Value));
                }
            }

            return tempconfig;
        }

        #endregion

        #region Helper Methods

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

        #endregion
    }
}
