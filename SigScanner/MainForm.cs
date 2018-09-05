using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SigScanner.Helpers;

namespace SigScanner
{
    public partial class MainForm : Form
    {
        private ProcessMemory _lastProcess;
        private Dictionary<string, List<Signature>> _moduleSignatures;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _lastProcess = null;
            _moduleSignatures = new Dictionary<string, List<Signature>>();

            ModuleNameTextBox.Text = "Scan all";
            ModuleNameTextBox.ForeColor = SystemColors.GrayText;

            SigMaskTextBox.Text = "xx????xxxx";
            SigMaskTextBox.ForeColor = SystemColors.GrayText;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (!ProcNameTextBox.Text.Any())
            {
                MessageBox.Show("Process Name cannot be empty!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ProcNameTextBox.ForeColor != Color.Green)
            {
                MessageBox.Show("Process doesnt exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_lastProcess == null || _lastProcess.ProcessName != ProcNameTextBox.Text)
                _lastProcess = new ProcessMemory(ProcNameTextBox.Text, Helpers.Natives.Enums.ProcessAccessFlags.VirtualMemoryRead);
            else
                if (!_lastProcess.IsAlive() || !_lastProcess.HasHandle())
                    _lastProcess.GetProcess(Helpers.Natives.Enums.ProcessAccessFlags.VirtualMemoryRead);

            if (!_lastProcess.IsAlive())
                return;

            // TODO:
        }

        private void SigsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var treeView = sender as TreeView;

            Clipboard.SetText(treeView.SelectedNode.Text);

            ToolTip.Show("Copied to Clipboard!", ActiveForm, PointToClient(MousePosition), 500);
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
            var sigMask = SigMaskTextBox.Text;
            var sigPattern = SigPatternTextBox.Text;
            var sigModuleName = ModuleNameTextBox.Text;

            SigMaskTextBox.Clear();
            SigPatternTextBox.Clear();
            //ModuleNameTextBox.Clear();

            if (sigPattern.Length < 2 || (SigMaskTextBox.Enabled && sigMask.Length < 2))
            {
                MessageBox.Show("Sig Pattern or Mask is to small", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_moduleSignatures.TryGetValue(sigModuleName, out var sigs))
            {
                foreach (var sig in sigs)
                    if (string.Compare(sig.Pattern, sigPattern, true) == 0)
                    {
                        MessageBox.Show("Sig with this Pattern already exists in this Module", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }
            else
                _moduleSignatures.Add(sigModuleName, new List<Signature>());

            var sigInfo = new Signature(sigModuleName, sigPattern, sigMask);
            if (!sigInfo.IsValid())
                return;

            _moduleSignatures[sigModuleName].Add(sigInfo);

            this.UpdateTreeView();

            if (InstantSearchCheckBox.Checked)
            {
                // TODO:
            }
        }

        private void ProcNameTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (ProcessMemory.DoesProcessExist(textBox.Text, out var processList))
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

            if (!textBox.Text.Equals(@"xx????xxxx"))
                return;

            textBox.Text = "";
            textBox.ForeColor = SystemColors.WindowText;
        }

        private void SigMaskTextBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text.Any())
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

            // TODO:
            // determine if the selected node is an address, pattern or module #regex?

            //if (!_moduleSignatures.TryGetValue(moduleNode.Text, out var sigs))
            //    return;

            //foreach (var sig in sigs)
            //{
            //    if (string.Compare(sig.Pattern, SigsTreeView.SelectedNode.Text) != 0)
            //        continue;
            //
            //    sigs.Remove(sig);
            //    break;
            //}

            //this.UpdateTreeView();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            if (!_moduleSignatures.Any())
                return;

            _moduleSignatures.Clear();

            this.UpdateTreeView();
        }

        private void UpdateTreeView()
        {
            SigsTreeView.Nodes.Clear();

            foreach (var module in _moduleSignatures)
            {
                var moduleNode = new TreeNode(module.Key);

                foreach (var sig in module.Value)
                {
                    var sigNode = new TreeNode(sig.Pattern);

                    moduleNode.Nodes.Add(sigNode);

                    if (sig.Offset != IntPtr.Zero)
                        sigNode.Nodes.Add($"0x{sig.Offset.ToString("")}");

                    sigNode.ForeColor = sig.Offset != IntPtr.Zero
                        ? Color.Green
                        : Color.Red;
                }

                SigsTreeView.Nodes.Add(moduleNode);
            }
        }
    }
}
