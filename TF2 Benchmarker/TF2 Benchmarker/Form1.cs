using System;
using System.Collections;
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
        FolderBrowserDialog TFFolderDialog;
        FileDialog ConfigDialog;

        Dictionary<string, string> Settings;
        
        
        public Benchmarker()
        {
            InitializeComponent();

            TFFolderDialog = new FolderBrowserDialog();
            ConfigDialog = new OpenFileDialog();

            Settings = new Dictionary<string, string>();

            // Radio buttons
            rb_dx98.Checked = true;
            rb_defaultconfig.Checked = true;

            // Listview
            lv_commands.View = View.Details;
            
        }

        #region Buttons

        private void btn_start_Click(object sender, EventArgs e)
        {

        }

        private void btn_tfpath_Click(object sender, EventArgs e)
        {
            TFFolderDialog.Description = "Select Team Fortress 2 Directory";
            
            if (TFFolderDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo f = new FileInfo(TFFolderDialog.SelectedPath + @"\hl2.exe");

                if (f.Exists)
                {
                    Settings["TFPath"] = TFFolderDialog.SelectedPath;
                    lbl_tf2path.Text = TFFolderDialog.SelectedPath;
                }
                else
                    MessageBox.Show("Invalid directory, could not locate hl2.exe");
            }
        }

        private void btn_loadconfig_Click(object sender, EventArgs e)
        {
            ConfigDialog.Title = "Open FPS Config";
            ConfigDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";

            if (ConfigDialog.ShowDialog() == DialogResult.OK)
            {
                var FPSConfig = new ArrayList();

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
                lv_commands.CheckBoxes = true;

                lv_commands.Columns.Add("Command", 310);
                lv_commands.Columns.Add("Value", 100);

                foreach (string s in FPSConfig)
                {
                    string cvar = s.Substring(0, s.IndexOf(" ")).Trim();
                    string val = s.Substring(s.IndexOf(" ") + 1).Trim();

                    lv_commands.Items.Add(cvar).SubItems.Add(val);
                }

                foreach (ListViewItem item in lv_commands.Items)
                    item.Checked = true;
            }
        }

        private void btn_configadditem_Click(object sender, EventArgs e)
        {
            if (txt_configaddname.Text != "Command" && txt_configaddvalue.Text != "Value"
                && txt_configaddname.Text.Length > 0 && txt_configaddvalue.Text.Length > 0)
            {
                lv_commands.Items.Add(txt_configaddname.Text).SubItems.Add(txt_configaddvalue.Text);
            }
        }

        #endregion

        #region ConfigRadioButtons

        private void rb_defaultconfig_CheckedChanged(object sender, EventArgs e)
        {
            btn_loadconfig.Enabled = false;
            lbl_configstatus.Text = "Disabled";
            lbl_configstatus.Enabled = false;
            lbl_configadditem.Enabled = false;
            txt_configaddname.Enabled = false;
            txt_configaddvalue.Enabled = false;
            btn_configadditem.Enabled = false;
            
            lv_commands.Clear();
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

        #region Text Fields

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



        #endregion
    }
}
