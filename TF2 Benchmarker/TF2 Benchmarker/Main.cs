using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TF2_Benchmarker
{
    public partial class Benchmarker : Form
    {
        const string version = "v1.6";

        string TFPath;
        volatile bool RunBaseline;
        
        #region Setup / Teardown

        public Benchmarker()
        {
            InitializeComponent();

            TFPath = "";
            RunBaseline = true;

            // Set the version in title.
            this.Text += version;

            // Radio buttons.
            rb_dxnone.Checked = true;
            rb_defaultconfig.Checked = true;

            // Listview setup.
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

            // Hide these columns from the UI, they are only used when the
            // user exports results to a csv file.
            lv_results.Columns.Add("totaltime", 0);
            lv_results.Columns.Add("numframes", 0);
            lv_results.Columns.Add("cmdline", 0);
            // ------

            lv_results.Columns.Add("Comment", -2);

            // Register Shift+F10 hotkey.
            RegisterHotKey(this.Handle, 0, (int)KeyModifier.Shift, Keys.F10.GetHashCode());

            // Set up the worker thread, which handles file generation 
            // and runs the game during the benchmark.
            WorkerThread.WorkerSupportsCancellation = true;
            WorkerThread.WorkerReportsProgress = true;

            InitializeConfig();
        }

        private void Benchmarker_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Remove the tfbench temporary directory if it exists.
            
            if (IsValidTFPath(TFPath))
            {
                string path = TFPath + @"\tf\custom\tfbench";
                try
                {
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                }
                catch { }
            }

            // Unregister Shift+F10 hotkey.
            UnregisterHotKey(this.Handle, 0);
        }

        #endregion

        #region Buttons

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (!IsValidTFPath(TFPath))
            {
                Log("TF2 path not set, aborting.");
                return;
            }

            if (!WorkerThread.IsBusy)
            {
                SetUIEnabledState(false);

                // Read the FPS config commands.
                var FPSConfig = new List<Cvar>();
                foreach (ListViewItem item in lv_commands.Items)
                {
                    if (item.Checked)
                        FPSConfig.Add(new Cvar(item.Text, item.SubItems[1].Text));
                }

                // Read the benchmark commands.
                var BenchmarkCvars = new List<Cvar>();
                foreach (ListViewItem item in lv_benchmarkcvars.Items)
                {
                    if (item.Checked)
                        if (item.Tag == null)
                            BenchmarkCvars.Add(new Cvar(item.Text, item.SubItems[1].Text));
                        else
                            BenchmarkCvars.Add(new Cvar(item.Text, item.Tag as List<Cvar>));
                }
                
                var Args = new Object[] { FPSConfig, BenchmarkCvars };

                WorkerThread.RunWorkerAsync(Args);
            }
            else
            {
                Log("Stopping benchmark after this run.");    
                WorkerThread.CancelAsync();
            }
        }

        private void btn_runbaseline_Click(object sender, EventArgs e)
        {
            if (!IsValidTFPath(TFPath))
            {
                Log("TF2 path not set, aborting.");
                return;
            }

            if (!WorkerThread.IsBusy)
            {
                RunBaseline = true;

                SetUIEnabledState(false);

                // Read the FPS config commands.
                var FPSConfig = new List<Cvar>();
                foreach (ListViewItem item in lv_commands.Items)
                {
                    if (item.Checked)
                        FPSConfig.Add(new Cvar(item.Text, item.SubItems[1].Text));
                }

                // Nothing to benchmark for the baseline, but pass the list anyway.
                var BenchmarkCvars = new List<Cvar>();
                
                var Args = new Object[] { FPSConfig, BenchmarkCvars };

                WorkerThread.RunWorkerAsync(Args);
            }
        }

        private void btn_tfpath_Click(object sender, EventArgs e)
        {
            var TFFolderDialog = new FolderBrowserDialog();
            TFFolderDialog.Description = "Select the \"Team Fortress 2\" Directory.";
            
            if (TFFolderDialog.ShowDialog() == DialogResult.OK)
            {
                if (IsValidTFPath(TFFolderDialog.SelectedPath))
                {
                    TFPath = TFFolderDialog.SelectedPath;
                    lbl_tf2path.Text = TFFolderDialog.SelectedPath;

                    WarnExistingAutoexec(TFPath);

                    Log("TF2 path set.");
                }
                else
                    MessageBox.Show("Invalid directory, could not locate hl2.exe.");
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

                // Read the FPS config from file, and parse.
                string noComments;

                using (var sr = new StreamReader(ConfigDialog.FileName))
                {
                    while (sr.Peek() != -1)
                    {
                        noComments = StripComments(sr.ReadLine());
                        noComments = noComments.Replace("\"", "");

                        if (noComments.Trim().Length > 0)
                            FPSConfig.Add(noComments);
                    }
                }

                // Add each command to the listview.
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
                RunBaseline = true;

                var item = new ListViewItem(txt_configaddname.Text);
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

            // Read the bench config from file.
            if (ConfigDialog.ShowDialog() == DialogResult.OK)
            {
                var BenchConfig = new List<string>();
                string noComments;

                // Remove any comments and doublequotes.
                using (var sr = new StreamReader(ConfigDialog.FileName))
                {
                    while (sr.Peek() != -1)
                    {
                        noComments = StripComments(sr.ReadLine());
                        noComments = noComments.Replace("\"", "");

                        if (noComments.Trim().Length > 0)
                            BenchConfig.Add(noComments);
                    }
                }
                
                foreach (string s in BenchConfig)
                {
                    if (!s.Contains("|"))
                    {
                        // Parse the command normally.
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
                        // Parse a multiline command.
                        // Multiline commands contain a list of cvars that should be run together.
                        // Ex: MyCommandListName | command_1 "value" | command_2 "value"
                        var MultiLineCmd = new List<Cvar>();

                        string[] cmds = s.Split('|');
                        string name = "";

                        for(int i = 0; i < cmds.Length; i++)
                        {
                            cmds[i] = cmds[i].Trim();

                            if (i == 0)
                            {
                                // First statement of a multiline contains the name.
                                name = cmds[i].Trim();
                                continue;
                            }

                            try
                            {
                                // The other parts are the actual cvars themselves.
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
                            var lv = new ListViewItem();
                            lv.Text = "Custom: " + name;
                            lv.Tag = MultiLineCmd;
                            lv.Checked = true;

                            lv_benchmarkcvars.Items.Add(lv);
                        }
                    }
                }

                Log("Benchmark config added.");
            }
        }

        private void btn_benchadditem_Click(object sender, EventArgs e)
        {
            if (txt_benchcommand.Text != "Command" && txt_benchmarkval.Text != "Value"
                && txt_benchcommand.Text.Length > 0 && txt_benchmarkval.Text.Length > 0)
            {
                var item = new ListViewItem(txt_benchcommand.Text);
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

            RunBaseline = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var ConfigFile = new IniFile(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) + @"\TFBenchmark.ini");
            
            if (IsValidTFPath(TFPath))
                ConfigFile.IniWriteValue("General", "TFPath", TFPath);

            ConfigFile.IniWriteValue("General", "LaunchOptions", txt_launchoptions.Text);
            ConfigFile.IniWriteValue("General", "DemoName", txt_demoname.Text);

            Log("Settings saved.");
        }

        private void btn_exportresults_Click(object sender, EventArgs e)
        {
            var SaveDialog = new SaveFileDialog();

            SaveDialog.FileName = "Results.csv";
            SaveDialog.Filter = "Comma separated values file (*.csv)|*.csv";
            SaveDialog.OverwritePrompt = true;

            // Write a csv of the current results list.
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                // Header
                var ResultFile = new StringBuilder();

                foreach (ColumnHeader item in lv_results.Columns)
                {
                    ResultFile.Append(item.Text + ",");
                }
                ResultFile.AppendLine();

                // Data rows
                foreach (ListViewItem item in lv_results.Items)
                {
                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        ResultFile.Append(item.SubItems[i].Text);
                        ResultFile.Append(",");
                    }

                    ResultFile.AppendLine();
                }

                try
                {
                    File.WriteAllText(SaveDialog.FileName, ResultFile.ToString());

                    Log("Results exported.");
                }
                catch (Exception ex)
                {
                    Log("Could not export results: " + ex.Message);
                }
            }
        }

        private void btn_clearresults_Click(object sender, EventArgs e)
        {
            var ConfirmResult = MessageBox.Show("Clear Results List?", "Confirm Action", MessageBoxButtons.YesNo);
            if (ConfirmResult == DialogResult.Yes)
            {
                lv_results.Items.Clear();
            }
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

            var rb = sender as RadioButton;

            if (rb != null && rb.Checked)
                RunBaseline = true;
        }

        private void rb_customconfig_CheckedChanged(object sender, EventArgs e)
        {
            btn_loadconfig.Enabled = true;
            lbl_configadditem.Enabled = true;
            txt_configaddname.Enabled = true;
            txt_configaddvalue.Enabled = true;
            btn_configadditem.Enabled = true;
            btn_clearfps.Enabled = true;

            var rb = sender as RadioButton;

            if (rb != null && rb.Checked)
                RunBaseline = true;
        }

        private void rb_dx8_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            if (rb != null && rb.Checked)
                RunBaseline = true;
        }

        private void rb_dx81_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            if (rb != null && rb.Checked)
                RunBaseline = true;
        }

        private void rb_dx90_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            if (rb != null && rb.Checked)
                RunBaseline = true;
        }

        private void rb_dx95_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            if (rb != null && rb.Checked)
                RunBaseline = true;
        }

        private void rb_dx98_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            if (rb != null && rb.Checked)
                RunBaseline = true;
        }

        #endregion

        #region UI

        // Move focus over to the right when a user presses enter in the "add command" sections.
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

        // Remove the placeholder text in the "add command" sections.
        private void txt_configaddname_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txt_configaddname.Clear();
            });
        }

        private void txt_configaddvalue_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txt_configaddvalue.Clear();
            });
        }

        private void txt_benchcommand_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txt_benchcommand.Clear();
            });
        }

        private void txt_benchmarkval_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txt_benchmarkval.Clear();
            });
        }

        // Set the proper RunBaseline state when the user changes settings.
        private void txt_launchoptions_TextChanged(object sender, EventArgs e)
        {
            RunBaseline = true;
        }

        private void txt_demoname_TextChanged(object sender, EventArgs e)
        {
            RunBaseline = true;
        }

        private void lv_commands_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            RunBaseline = true;
        }

        // Hide some columns that aren't particularly useful in the results listview.
        private void lv_results_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            int i = e.ColumnIndex;
            if (i >= 3 && i <= 5)
            {
                e.Cancel = true;
                e.NewWidth = 0;
            }
        }

        #endregion

        #region Worker Thread

        private void WorkerThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!File.Exists(TFPath + @"\tf\" + txt_demoname.Text))
            {
                WorkerThread.ReportProgress(0, "Could not find demo file " + txt_demoname.Text + ", aborting.");
                return;
            }

            WorkerThread.ReportProgress(0, "Starting...");

            string args;
            var WorkerArgs = e.Argument as Object[];
            var FPSConfig = WorkerArgs[0] as List<Cvar>;
            var BenchCvars = WorkerArgs[1] as List<Cvar>;

            // Back up existing config.cfg.
            var config = new FileInfo(TFPath + @"\tf\cfg\config.cfg");
            if (config.Exists)
            {
                try
                {
                    config.IsReadOnly = false;
                    config.CopyTo(TFPath + @"\tf\cfg\config.cfg.bak", true);
                    config.Delete();
                }
                catch (Exception ex)
                {
                    WorkerThread.ReportProgress(0, "Error: Could not modify config.cfg: " + ex.Message + ", aborting.");
                    return;
                }
            }

            // Back up existing sourcebench.csv.
            var sourcebenchcsv = new FileInfo(TFPath + @"\tf\sourcebench.csv");
            if (sourcebenchcsv.Exists)
                sourcebenchcsv.CopyTo(TFPath + @"\tf\sourcebench.csv.bak", true);
            
            // Run a baseline test with no commands to compare against if we need to.
            if (RunBaseline)
            {
                WorkerThread.ReportProgress(0, "Generating baseline benchmark...");
                PrepDirectory(TFPath);

                // Create a new config.cfg with host_writeconfig to prevent any leftover commands from polluting the benchmark.

                if (rb_defaultconfig.Checked)
                {
                    // Use default config
                    args = "-steam -game tf -default -timedemo_comment \"Baseline\" " + txt_launchoptions.Text
                            + GetDxLevel() + " +timedemoquit " + txt_demoname.Text;

                    // We cannot specify the timedemo_runcount when using -default. See issue #5 on github.

                    FPSConfig.Add(new Cvar("host_writeconfig", "\"config\" full"));

                    WriteCfg(FPSConfig, TFPath, false);
                    StartBenchmark(args, TFPath);

                    rb_dxnone.Checked = true;
                    FPSConfig.Clear();
                }
                else
                {
                    // Use specified fps config instead of defaults.
                    args = "-steam -game tf -timedemo_comment \"Baseline\" " + txt_launchoptions.Text
                            + GetDxLevel() + GetRunCount() + " +timedemoquit " + txt_demoname.Text;

                    var DefaultConfig = new List<Cvar>(FPSConfig);
                    DefaultConfig.Add(new Cvar("host_writeconfig", "\"config\" full"));

                    WriteCfg(DefaultConfig, TFPath);
                    StartBenchmark(args, TFPath);
                }

                // Set the newly generated config.cfg it to read only,
                // so TF2 doesn't overwrite it during the benchmark.

                config.Refresh();
                if (config.Exists)
                    config.IsReadOnly = true;

                RunBaseline = false;
            }

            // Main benchmark loop.

            List<Cvar> AutoexecConfig;
            
            foreach (var item in BenchCvars)
            {
                if (WorkerThread.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                WorkerThread.ReportProgress(0, "Benchmarking " + item.Print() + "...");

                AutoexecConfig = MergeConfig(FPSConfig, item);

                PrepDirectory(TFPath);
                WriteCfg(AutoexecConfig, TFPath);

                args = "-steam -game tf -timedemo_comment \"" + item.Print() + "\" " 
                        + txt_launchoptions.Text + GetRunCount() + " +timedemoquit " + txt_demoname.Text;

                StartBenchmark(args, TFPath);
            }

            // Delete the generated config.cfg.
            config.Refresh();
            if (config.Exists)
            {
                config.IsReadOnly = false;
                config.Delete();
            }

            // Replace it with the original.
            config = new FileInfo(TFPath + @"\tf\cfg\config.cfg.bak");
            if (config.Exists)
            {
                config.CopyTo(TFPath + @"\tf\cfg\config.cfg");
                config.Delete();
            }

            // Copy the original sourcebench.csv back, and delete the backup file.
            var backupcsv = new FileInfo(TFPath + @"\tf\sourcebench.csv.bak");
            if (backupcsv.Exists)
            {
                backupcsv.CopyTo(sourcebenchcsv.FullName, true);
                backupcsv.Delete();
            }

            WorkerThread.ReportProgress(0, "Benchmark Complete.");
        }

        private void WorkerThread_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Log(e.UserState as String);
        }

        private void WorkerThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            SetUIEnabledState(true);
        }

        #endregion

        #region Benchmark Functions

        private void StartBenchmark(string args, string path)
        {
            StartGame(args, path);

            // Parse the csv file generated by TF2.

            var results = new FileInfo(path + @"\tf\sourcebench.csv");
            if (!results.Exists)
            {
                WorkerThread.ReportProgress(0, "Benchmark results file not found.");
                return;
            }

            using (var parser = new TextFieldParser(results.FullName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = false;
                parser.TrimWhiteSpace = true;

                int lineCount = File.ReadLines(results.FullName).Count();

                // Skip the first header line of the file.
                parser.ReadLine();

                while (parser.PeekChars(1) != null)
                {
                    var li = new ListViewItem();

                    // Read every other line if benchmarking twice per command.
                    if (cb_runtwice.Checked && lineCount > 2 && parser.LineNumber % 2 == 0)
                    {
                        parser.ReadLine();
                        continue;
                    }

                    string[] row = parser.ReadFields();
                    for (int i = 0; i < row.Length; i++)
                    {
                        switch(i)
                        {
                            case 0:
                                li.Text = row[i];
                                break;
                            case 1: case 2: case 3: case 4: case 12: case 23:
                                li.SubItems.Add(row[i]);
                                break;
                        }
                    }

                    // Add the results to the listview.
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
            // Run the game, and wait until the benchmark completes.

            var TF2Proc = new ProcessStartInfo();
            TF2Proc.CreateNoWindow = true;
            TF2Proc.Arguments = args;
            TF2Proc.FileName = path + @"\hl2.exe";

            using (var proc = Process.Start(TF2Proc))
            {
                proc.WaitForExit();
            }
        }

        private void PrepDirectory(string path)
        {
            // Delete any existing sourcebench.csv file.

            var benchcsv = new FileInfo(path + @"\tf\sourcebench.csv");
            if (benchcsv.Exists)
                benchcsv.Delete();

            // Create the tfbench directory for the autoexec.cfg if it doesn't already exist.

            string cfgpath = path + @"\tf\custom\tfbench\cfg";
            Directory.CreateDirectory(cfgpath);

            // Delete any existing autoexec.cfg.

            var autoexec = new FileInfo(cfgpath + @"\autoexec.cfg");
            if (autoexec.Exists)
                autoexec.Delete();
        }

        private void WriteCfg(List<Cvar> config, string path, bool format = true)
        {
            using (var file = new StreamWriter(path + @"\tf\custom\tfbench\cfg\autoexec.cfg", false))
            {
                foreach (Cvar c in config)
                    if (format)
                        file.WriteLine(String.Format("{0} \"{1}\"", c.Command, c.Value));
                    else
                        file.WriteLine(c.Print());
            }
        }

        private List<Cvar> MergeConfig(List<Cvar> dest, Cvar c)
        {
            // Overwrite any commands in the FPS config that could override a command that is being tested.

            var tempconfig = new List<Cvar>(dest);
            bool added = false;

            if (c.Value != null)
            {
                // Normal command.

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
                // Multiline command.

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
            lb_log.Items.Add(String.Format("[{0}] {1}", DateTime.Now.ToString("h:mm:ss tt"), message));
            lb_log.TopIndex = lb_log.Items.Count - 1; // Scroll to bottom.
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

        private string GetRunCount()
        {
            if (cb_runtwice.Checked)
                return " +timedemo_runcount 2";
            else
                return "";
        }

        private void SetUIEnabledState(bool Enabled)
        {
            if (Enabled)
                btn_start.Text = "&Start";
            else
                btn_start.Text = "&Stop";
            
            btn_runbaseline.Enabled = Enabled;
            btn_tfpath.Enabled = Enabled;

            rb_dx8.Enabled = Enabled;
            rb_dx81.Enabled = Enabled;
            rb_dx90.Enabled = Enabled;
            rb_dx95.Enabled = Enabled;
            rb_dx98.Enabled = Enabled;
            rb_dxnone.Enabled = Enabled;

            txt_launchoptions.Enabled = Enabled;
            
            rb_customconfig.Enabled = Enabled;
            rb_defaultconfig.Enabled = Enabled;
            
            if (rb_defaultconfig.Checked)
            {
                txt_configaddname.Enabled = Enabled;
                txt_configaddvalue.Enabled = Enabled;
                btn_configadditem.Enabled = Enabled;
            }

            cb_runtwice.Enabled = Enabled;
            txt_demoname.Enabled = Enabled;
        }

        private bool IsValidTFPath(string path)
        {
            var hl2exe = new FileInfo(path + @"\hl2.exe");

            if (hl2exe.Exists)
                return true;

            return false;
        }

        private void WarnExistingAutoexec(string path)
        {
            // Scan for existing autoexec.cfg files and warn if any are found.
            var customPath = path + @"\tf\custom";
            var cfgPath = path + @"\tf\cfg";
            var customConfs = new DirectoryInfo(customPath).GetFiles("autoexec.cfg", System.IO.SearchOption.AllDirectories);
            var confs = new DirectoryInfo(cfgPath).GetFiles("autoexec.cfg", System.IO.SearchOption.AllDirectories);

            if (customConfs.Length > 0 || confs.Length > 0)
                Log("WARNING: An existing autoexec.cfg was found.");

            MessageBox.Show("An existing autoexec.cfg was found.\n" +
                "Please remove or rename it to ensure accurate benchmark results.",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }

        #endregion

        #region Configuration File Handling

        private void InitializeConfig()
        {
            var ConfigFile = new IniFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TFBenchmark.ini");

            if (!File.Exists(ConfigFile.path))
            {
                // Write a new configuration .ini file if it doesn't exist.
                ConfigFile.IniWriteValue("General", "TFPath", "");
                ConfigFile.IniWriteValue("General", "LaunchOptions", "-novid -w 1920 -h 1080 -fullscreen");
                ConfigFile.IniWriteValue("General", "DemoName", "benchmark01.dem");
            }

            // Load the configuration file.
            string path = ConfigFile.IniReadValue("General", "TFPath");

            if (path.Length > 0)
            {
                TFPath = path;
                lbl_tf2path.Text = path;

                WarnExistingAutoexec(path);
            }

            txt_launchoptions.Text = ConfigFile.IniReadValue("General", "LaunchOptions");
            txt_demoname.Text = ConfigFile.IniReadValue("General", "DemoName");

            Log("Settings loaded.");
        }

        #endregion

        #region Hotkey

        // Functions for handling Shift+F10 shortcut.

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
                System.Media.SystemSounds.Asterisk.Play();

                WorkerThread.CancelAsync();
                Log("Stopping benchmark after this run.");
            }
        }
    }

    #endregion
}
