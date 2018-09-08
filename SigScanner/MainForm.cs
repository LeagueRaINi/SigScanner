using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SigScanner.Helpers;

namespace SigScanner
{
    public partial class MainForm : Form
    {
        private ProcessMemory _lastProcess;
        private List<Signature> _sigList;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _lastProcess = null;
            _sigList = new List<Signature>();

            ModuleNameTextBox.Text = "Scan all";
            ModuleNameTextBox.ForeColor = SystemColors.GrayText;

            SigMaskTextBox.Text = "xx????xxxx";
            SigMaskTextBox.ForeColor = SystemColors.GrayText;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (!_sigList.Any())
            {
                MessageBox.Show("There are no Signatures?", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ProcNameTextBox.Text.Any())
            {
                MessageBox.Show("Process Name cannot be empty!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_lastProcess == null || _lastProcess.ProcessName != ProcNameTextBox.Text)
                _lastProcess = new ProcessMemory(ProcNameTextBox.Text, Helpers.Natives.Enums.ProcessAccessFlags.VirtualMemoryRead);
            else
                if (!_lastProcess.IsAlive() || !_lastProcess.HasHandle())
                    _lastProcess.GetProcess(Helpers.Natives.Enums.ProcessAccessFlags.VirtualMemoryRead);

            if (!_lastProcess.IsAlive())
            {
                _lastProcess.Dispose();
                return;
            }

            Parallel.ForEach(_sigList, (sig) => {
                _lastProcess.GetSignatureAddresses(sig);
            });

            this.UpdateTreeView();

            MessageBox.Show("Finished", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (sigPattern.Length < 5 || (SigMaskTextBox.Enabled && sigMask.Length < 5))
            {
                MessageBox.Show("Sig Pattern or Mask is to small", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sigListObj = _sigList.Where(x => string.Compare(x.Pattern, sigPattern, true) == 0).FirstOrDefault();
            if (sigListObj != null)
            {
                if (sigListObj.Offsets.Keys.Contains(sigModuleName))
                {
                    MessageBox.Show("Sig with this Pattern already exists for this Module", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                    sigListObj.Offsets.Add(sigModuleName, new List<IntPtr>());
            }
            else
            {
                var sigInfo = new Signature(sigModuleName, sigPattern, sigMask);
                if (!sigInfo.IsValid())
                    return;

                sigInfo.Offsets.Add(sigModuleName, new List<IntPtr>());

                _sigList.Add(sigInfo);
            }

            this.UpdateTreeView();

            if (InstantSearchCheckBox.Checked)
                SearchButton.PerformClick();
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

            var selectedNodeText = SigsTreeView.SelectedNode.Text;
            var parentNode = SigsTreeView.SelectedNode.Parent;

            if (parentNode == null)
            {
                var sigObj = _sigList.Where(x => x.Pattern.Equals(selectedNodeText)).FirstOrDefault();
                if (sigObj != null)
                    _sigList.Remove(sigObj);
            }
            else if (selectedNodeText.StartsWith("0x"))
            {
                var sigObj = _sigList.Where(x => x.Pattern.Equals(parentNode.Parent.Text)).FirstOrDefault();
                if (sigObj != null)
                    if (sigObj.Offsets.TryGetValue(parentNode.Text, out var offsets))
                        offsets.Remove((IntPtr)Convert.ToInt32(selectedNodeText, 16));
            }
            else
            {
                var sigObj = _sigList.Where(x => x.Pattern.Equals(parentNode.Text)).FirstOrDefault();
                if (sigObj != null)
                    if (sigObj.Offsets.ContainsKey(selectedNodeText))
                        sigObj.Offsets.Remove(selectedNodeText);

            }

            this.UpdateTreeView();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            if (!_sigList.Any())
                return;

            _sigList.Clear();

            this.UpdateTreeView();
        }

        private void UpdateTreeView()
        {
            SigsTreeView.Nodes.Clear();

            var sigNodeColor = Color.Black;
            var moduleNodeColor = Color.Black;

            foreach (var sig in _sigList)
            {
                var sigNode = new TreeNode(sig.Pattern);

                foreach (var module in sig.Offsets)
                {
                    var moduleNode = new TreeNode(module.Key)
                    {
                        ForeColor = module.Value.Count == 0
                            ? Color.Red
                            : module.Value.Count > 1
                                ? Color.Orange
                                : Color.Green
                    };

                    foreach (var offset in module.Value)
                        moduleNode.Nodes.Add($"0x{offset.ToString("X")}");

                    sigNode.Nodes.Add(moduleNode);
                }

                var emptyNodes = 0;
                var multiplyAddressesNodes = 0;
                foreach (TreeNode node in sigNode.Nodes)
                {
                    if (node.ForeColor.Equals(Color.Red))
                        emptyNodes++;
                    if (node.ForeColor.Equals(Color.Orange))
                        multiplyAddressesNodes++;
                }

                var sigNodesCount = sigNode.GetNodeCount(false);
                if (sigNodesCount != 0)
                    sigNode.ForeColor = multiplyAddressesNodes != 0
                        ? Color.Orange
                        : sigNodesCount == emptyNodes
                            ? Color.Red
                            : Color.Green;

                SigsTreeView.Nodes.Add(sigNode);
            }
        }
    }
}
