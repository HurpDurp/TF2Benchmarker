using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace TF2_Benchmarker
{
    public partial class Benchmarker : Form
    {
        Dictionary<string, string> Settings;

        bool Benchmarking = false;
                
        public Benchmarker()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>();

            // Radio buttons
            rb_dx98.Checked = true;
            rb_defaultconfig.Checked = true;

            // Listviews
            lv_commands.View = View.Details;
            lv_benchmarkcvars.View = View.Details;

            lv_commands.CheckBoxes = true;
            lv_commands.Columns.Add("Command", 310);
            lv_commands.Columns.Add("Value", 130);

            lv_benchmarkcvars.CheckBoxes = true;
            lv_benchmarkcvars.Columns.Add("Command", 310);
            lv_benchmarkcvars.Columns.Add("Values", 130);

            Log("Initialized");
        }

        #region Buttons

        private void btn_start_Click(object sender, EventArgs e)
        {
            string path;

            if (!Benchmarking && Settings.TryGetValue("TFPath", out path))
            {
                btn_start.Text = "&Stop";
                Benchmarking = true;

                // Prep the directory

                FileInfo benchcsv = new FileInfo(path + @"\tf\sourcebench.csv");
                if (benchcsv.Exists)
                {
                    benchcsv.CopyTo("sourcebench.csv.bak");
                    benchcsv.Delete();
                }

                path += @"\tf\custom\tfbench\cfg";
                Directory.CreateDirectory(path);
                
                // Create a config for this benchmark

                List<string> output = new List<string>();
                List<string> fpsconfig = new List<string>();

                foreach (ListViewItem item in lv_commands.Items)
                {
                    if (item.Checked)
                        fpsconfig.Add(String.Format("{0} \"{1}\"", item.Text, item.SubItems[1].Text));
                }

                List<string> tempconfig;

                foreach (ListViewItem item in lv_benchmarkcvars.Items)
                {
                    tempconfig = new List<string>(fpsconfig);

                    for (int i = 0; i < tempconfig.Count; i++)
                    {
                        if (tempconfig[i].Contains(item.Text))
                        {
                            tempconfig[i] = String.Format("{0} \"{1}\"", item.Text, item.SubItems[1].Text);
                        }
                    }
                }

                // Write to autoexec

                // run game
            }
            else
            {
                // Do something to stop the tf2 process

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

                lbl_configstatus.Text = "Loaded";

                // Read FPS config
                string noComments;

                using (StreamReader sr = new StreamReader(ConfigDialog.FileName))
                {
                    do
                    {
                        noComments = StripComments(sr.ReadLine());
                        noComments = noComments.Replace("\"", "");

                        if (noComments.Trim().Length > 0)
                            FPSConfig.Add(noComments);

                    } while (sr.Peek() != -1);
                }

                // Populate command listview
                foreach (string s in FPSConfig)
                {
                    string cvar = s.Substring(0, s.IndexOf(" ")).Trim();
                    string val = s.Substring(s.IndexOf(" ") + 1).Trim();

                    lv_commands.Items.Add(cvar).SubItems.Add(val);
                }

                foreach (ListViewItem item in lv_commands.Items)
                    item.Checked = true;

                Log("FPS config loaded");
            }
        }

        private void btn_configadditem_Click(object sender, EventArgs e)
        {
            if (txt_configaddname.Text != "Command" && txt_configaddvalue.Text != "Value"
                && txt_configaddname.Text.Length > 0 && txt_configaddvalue.Text.Length > 0)
            {
                lv_commands.Items.Add(txt_configaddname.Text).SubItems.Add(txt_configaddvalue.Text);

                txt_configaddname.Clear();
                txt_configaddvalue.Clear();

                Log("Command added");
            }
        }

        private void btn_loadbenchconfig_Click(object sender, EventArgs e)
        {
            var ConfigDialog = new OpenFileDialog();

            ConfigDialog.Title = "Open Benchmark Config";
            ConfigDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";

            if (ConfigDialog.ShowDialog() == DialogResult.OK)
            {
                var FPSConfig = new List<string>();

                lbl_benchconfig.Text = "Loaded";

                // Read FPS config
                string noComments;

                using (StreamReader sr = new StreamReader(ConfigDialog.FileName))
                {
                    do
                    {
                        noComments = StripComments(sr.ReadLine());
                        noComments = noComments.Replace("\"", "");

                        if (noComments.Trim().Length > 0)
                            FPSConfig.Add(noComments);

                    } while (sr.Peek() != -1);
                }

                // Populate command listview
                foreach (string s in FPSConfig)
                {
                    string cvar = s.Substring(0, s.IndexOf(" ")).Trim();
                    string val = s.Substring(s.IndexOf(" ") + 1).Trim();

                    lv_benchmarkcvars.Items.Add(cvar).SubItems.Add(val);
                }

                foreach (ListViewItem item in lv_benchmarkcvars.Items)
                    item.Checked = true;

                Log("Benchmark config loaded");
            }
        }

        private void btn_benchadditem_Click(object sender, EventArgs e)
        {
            if (txt_benchcommand.Text != "Command" && txt_benchcommand.Text.Length > 0)
            {
                lv_benchmarkcvars.Items.Add(txt_benchcommand.Text).SubItems.Add(txt_benchmarkval.Text);

                txt_benchcommand.Clear();
                txt_benchmarkval.Clear();

                Log("Benchmark command added");
            }
        }

        #endregion

        #region Radio Buttons

        private void rb_defaultconfig_CheckedChanged(object sender, EventArgs e)
        {
            btn_loadconfig.Enabled = false;
            lbl_configstatus.Text = "Disabled";
            lbl_configstatus.Enabled = false;
            lbl_configadditem.Enabled = false;
            txt_configaddname.Enabled = false;
            txt_configaddvalue.Enabled = false;
            btn_configadditem.Enabled = false;
            
            lv_commands.Items.Clear();
        }

        private void rb_customconfig_CheckedChanged(object sender, EventArgs e)
        {
            btn_loadconfig.Enabled = true;
            lbl_configstatus.Text = "Not Loaded";
            lbl_configstatus.Enabled = true;
            lbl_configadditem.Enabled = true;
            txt_configaddname.Enabled = true;
            txt_configaddvalue.Enabled = true;
            btn_configadditem.Enabled = true;
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


        #endregion
    }
}
