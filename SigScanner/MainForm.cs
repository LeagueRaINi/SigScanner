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
            // TODO:
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
            if (imSearchCheckbox.Checked)
            {
                // TODO
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

            if (textBox.Text != "xx????xxxx")
                return;

            textBox.Text = "";
            textBox.ForeColor = SystemColors.WindowText;
        }

        private void SigMaskTextBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text.Length != 0)
                return;

            textBox.Text = "xx????xxxx";
            textBox.ForeColor = SystemColors.GrayText;
        }

        private void SigMaskTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // disallow characters
            if (!char.IsControl(e.KeyChar) /*&& e.KeyChar != (char)Keys.Back*/ && !Regex.IsMatch(e.KeyChar.ToString(), @"[x|?]"))
            {
                e.Handled = true;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            //
        }
    }
}
