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
            //

            ModuleNameTextBox.Text = "Scan all";
            ModuleNameTextBox.ForeColor = SystemColors.GrayText;

            SigMaskTextBox.Text = "xx????xxxx";
            SigMaskTextBox.ForeColor = SystemColors.GrayText;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            //
        }

        private void OffsetsListView_DoubleClick(object sender, EventArgs e)
        {
            var selectedItems = OffsetsListView.SelectedItems;
            if (selectedItems.Count < 1)
                return;

            var selectedItemsSubItems = selectedItems[0].SubItems;
            if (selectedItemsSubItems.Count < 1)
                return;

            Clipboard.SetText(selectedItemsSubItems[1].Text);
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
            if (SigPatternTextBox.Text.Length < 2 || (SigMaskTextBox.Enabled && SigMaskTextBox.Text.Length < 1))
            {
                // TODO: msgbox?
                return;
            }

            Signature sig = new Signature(ModuleNameTextBox.Text, SigPatternTextBox.Text, SigMaskTextBox.Text);

            SigPatternTextBox.Text = "";
            SigMaskTextBox.Text = "";
            // TODO: reset module?

            // add sig to list
            sigs.Add(sig);

            if (imSearchCheckbox.Checked)
            {
                // TODO:
            }
        }

        private void ProcNameTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            // TODO: check for process' existence
        }

        // meme
        private void MainForm_MouseEnter(object sender, EventArgs e)
        {
            ProgressBar.Value = new Random().Next(0, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
    }
}
