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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        private void SignatureTextBox_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Contains(@"\x"))
                MessageBox.Show("miep!");
        }

        private void CheckAllModuleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var cbox = sender as CheckBox;

            ModuleNameTextBox.Enabled = !cbox.Checked;
        }
    }
}
