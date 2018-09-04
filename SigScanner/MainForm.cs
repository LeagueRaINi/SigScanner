using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
            if (imSearchCheckbox.Checked)
            {
                // TODO
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
    }
}
