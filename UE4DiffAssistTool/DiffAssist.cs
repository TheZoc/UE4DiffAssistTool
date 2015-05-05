using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace UE4DiffAssistTool
{
    public partial class DiffAssist : Form
    {
        private String DiffCommand = "";
        public DiffAssist()
        {
            InitializeComponent();
        }

        private void txtFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtOldFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txtOldFile.Text = files[0];
                UpdateDiffCommandDisplay();
            }
        }

        private void txtNewFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txtNewFile.Text = files[0];
                UpdateDiffCommandDisplay();
            }
        }

        private void txtEditorPath_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                if (files[0].EndsWith("UE4Editor.exe", true, System.Globalization.CultureInfo.InvariantCulture) == false)
                {
                    // Steal Focus!
                    this.TopMost = true;
                    this.TopMost = false;
                    MessageBox.Show("Drag and Drop UE4Editor.exe version you want to use in this box.\nThis seems to be an invalid file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtEditorPath.Text = files[0];
                    UpdateDiffCommandDisplay();
                }
            }
        }

        private void DiffAssist_Load(object sender, EventArgs e)
        {
            this.Size           = new System.Drawing.Size(Properties.Settings.Default.FormWidth, Properties.Settings.Default.FormHeight);
            txtEditorPath.Text  = Properties.Settings.Default.UE4EditorPath;
            txtOldFile.Text     = Properties.Settings.Default.LastOldFile;
            txtNewFile.Text     = Properties.Settings.Default.LastNewFile;
            UpdateDiffCommandDisplay();
        }

        private void txtEditorPath_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.UE4EditorPath = txtEditorPath.Text;
            Properties.Settings.Default.Save();
            UpdateDiffCommandDisplay();
        }

        private void txtOldFile_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastOldFile = txtOldFile.Text;
            Properties.Settings.Default.Save();
            UpdateDiffCommandDisplay();
        }

        private void txtNewfile_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastNewFile = txtNewFile.Text;
            Properties.Settings.Default.Save();
            UpdateDiffCommandDisplay();
        }

        private void btnShowDiff_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowDiff = !Properties.Settings.Default.ShowDiff;
            Properties.Settings.Default.Save();
            UpdateDiffCommandDisplay();
        }

        private void DiffAssist_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormWidth = this.Width;
            Properties.Settings.Default.FormHeight = this.Height;
            Properties.Settings.Default.Save();
        }

        private void btnDiff_Click(object sender, EventArgs e)
        {
            UpdateDiffCommandDisplay();
            ProcessStartInfo diffProcess = new ProcessStartInfo();
            diffProcess.Arguments = "-diff \"" + txtNewFile.Text + "\" \"" + txtOldFile.Text + "\"";
            diffProcess.FileName = txtEditorPath.Text;
            Process.Start(diffProcess);
        }

        private void UpdateDiffCommandDisplay()
        {
            if (Properties.Settings.Default.ShowDiff == false)
            {
                btnShowDiff.Text = "Show Diff Command";
                txtDiffCommand.Text = "";
                return;
            }

            btnShowDiff.Text = "Hide Diff Command";

            DiffCommand = txtEditorPath.Text
                        + " -diff \""
                        + txtNewFile.Text
                        + "\" \""
                        + txtOldFile.Text
                        + "\"";

            txtDiffCommand.Text = DiffCommand;

        }
    }
}
