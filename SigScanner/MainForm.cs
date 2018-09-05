using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SigScanner.Helpers;

namespace SigScanner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ModuleNameTextBox.Text = "Scan all";
            ModuleNameTextBox.ForeColor = SystemColors.GrayText;

            SigMaskTextBox.Text = "xx????xxxx";
            SigMaskTextBox.ForeColor = SystemColors.GrayText;

            //DEBUG

            var sigNode = new TreeNode("75 0C 33 D2 89 57 14 BA ? ? ? ? FF E2");

            sigNode.ForeColor = Color.Green;

            SigsTreeView.Nodes.Add(sigNode);

            sigNode.Nodes.Add("0x836342");

            var sigNode2 = new TreeNode("89 0C 33 57 14 BA ? ? ? ? FF");

            sigNode2.ForeColor = Color.Orange;

            SigsTreeView.Nodes.Add(sigNode2);

            sigNode2.Nodes.Add("0x926342");
            sigNode2.Nodes.Add("0x126343");

            var sigNode3 = new TreeNode("BA 33 57 14 BA ? ? ? ? FF");

            SigsTreeView.Nodes.Add(sigNode3);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            // TODO:
        }

        private void SigsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var treeView = sender as TreeView;

            Clipboard.SetText(treeView.SelectedNode.Text);
        }

        private void SigTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            SigMaskTextBox.Enabled = textBox.Text.Contains(@"\x");
        }

        private void CheckAllModuleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;

            ModuleNameTextBox.Enabled = !checkBox.Checked;
        }

        private void AddSigButton_Click(object sender, EventArgs e)
        {
            if (SigPatternTextBox.Text.Length < 2 || (SigMaskTextBox.Enabled && SigMaskTextBox.Text.Length < 2))
            {
                MessageBox.Show("Sig Pattern or Mask is to small", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sig = new Signature(ModuleNameTextBox.Text, SigPatternTextBox.Text, SigMaskTextBox.Text);

            SigPatternTextBox.Clear();
            SigMaskTextBox.Clear();

            // TODO: reset module?

            //_sigs.Add(sig);

            if (imSearchCheckbox.Checked)
            {
                // TODO:
            }
        }

        private void ProcNameTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (Helpers.ProcessMemory.DoesProcessExist(textBox.Text, out var processList))
                textBox.ForeColor = Color.Green;
            else
                textBox.ForeColor = Color.OrangeRed;
        }

        private void imSearchCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;

            if (checkBox.Checked)
                SearchButton.Text = "Refresh";
            else
                SearchButton.Text = "Search";
        }

        private void ModuleNameTextBox_Enter(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text != "Scan all")
                return;

            textBox.Text = "";
            textBox.ForeColor = SystemColors.WindowText;
        }

        private void ModuleNameTextBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text.Length != 0)
                return;

            textBox.Text = "Scan all";
            textBox.ForeColor = SystemColors.GrayText;
        }

        private void SigMaskTextBox_Enter(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text != @"xx????xxxx")
                return;

            textBox.Text = "";
            textBox.ForeColor = SystemColors.WindowText;
        }

        private void SigMaskTextBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text.Length != 0)
                return;

            textBox.Text = @"xx????xxxx";
            textBox.ForeColor = SystemColors.GrayText;
        }

        private void SigMaskTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // disallow characters
            if (!char.IsControl(e.KeyChar) /*&& e.KeyChar != (char)Keys.Back*/ && !Regex.IsMatch(e.KeyChar.ToString(), @"[x|?]"))
            {
                // TODO: show tooltip to notify usage of disallowed character?
                e.Handled = true;
            }
        }

        private void SigMaskTextBox_TextChanged(object sender, EventArgs e)
        {
            // TODO: prevent pasting of disallowed characters
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (SigsTreeView.SelectedNode == null)
            {
                MessageBox.Show("You need to select something first", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // there SHOULD only be one sig with the same pattern in the list but just in case
            //var sigs = _sigs.Where(x => x.Pattern == SigsTreeView.SelectedNode.Text).ToList();
            //if (sigs.Any())
            //{
            //    foreach (var sig in sigs)
            //        _sigs.Remove(sig);
            //}

            SigsTreeView.SelectedNode.Remove();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            if (SigsTreeView.Nodes.Count < 1)
                return;

            //_sigs.Clear();

            SigsTreeView.Nodes.Clear();
        }
    }
}
