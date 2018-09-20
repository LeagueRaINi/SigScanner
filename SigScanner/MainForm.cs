using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SigScanner.Helpers;

namespace SigScanner
{
    public partial class MainForm : Form
    {
        private ProcessMemory _lastProcess { get; set; }
        private List<Signature> _sigList { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = $@"SigScanSharp - {(Environment.Is64BitProcess ? "x64" : "x32")} Build";

            _lastProcess = null;
            _sigList = new List<Signature>();
        }

        private void SigPatternTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
                return;

            SigMaskTextBox.Enabled = textBox.Text.Contains(@"\x");
        }

        private void InstantSearchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SearchButton.Text = sender is CheckBox checkBox && checkBox.Checked
                ? "Refresh"
                : "Search";
        }

        private void ModuleNameTextBox_Enter(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
                return;
            if (!textBox.Text.Equals("Scan all"))
                return;

            textBox.Clear();
            textBox.ForeColor = SystemColors.WindowText;
        }

        private void ModuleNameTextBox_Leave(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
                return;
            if (textBox.Text.Any())
                return;

            textBox.Text = @"Scan all";
            textBox.ForeColor = SystemColors.GrayText;
        }

        private void SigMaskTextBox_Enter(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
                return;
            if (!textBox.Text.Equals(@"[xx??xx]"))
                return;

            textBox.Clear();
            textBox.ForeColor = SystemColors.WindowText;
        }

        private void SigMaskTextBox_Leave(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
                return;
            if (textBox.Text.Any())
                return;

            textBox.Text = @"[xx??xx]";
            textBox.ForeColor = SystemColors.GrayText;
        }

        private void SigMaskTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || Regex.IsMatch(e.KeyChar.ToString(), @"[x?]"))
                return;

            ErrorToolTip.Show("Only 'x' and '?' are allowed", SigMaskTextBox, 1000);

            e.Handled = true;
        }

        private void SigMaskTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is TextBox textBox))
                return;
            if (textBox.Text.Equals(@"[xx??xx]"))
                return;
            if (Regex.Matches(textBox.Text, @"[x?]").Count == textBox.Text.Length)
                return;

            textBox.Clear();

            ErrorToolTip.Show("Only 'x' and '?' are allowed", textBox, 1000);
        }

        private void SigsTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (!(sender is TreeView treeView))
                return;

            try
            {
                Clipboard.SetText(treeView.SelectedNode.Text);
                InfoToolTip.Show("Copied to clipboard!", ActiveForm, PointToClient(MousePosition) - new Size(10, 40), 750);
            }
            catch (Exception exception)
            {
                Logger.ShowDebug(exception.ToString());
            }
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
                Logger.ShowError("Pattern or mask is too small");
                return;
            }

            var sigListObj = _sigList.FirstOrDefault(x => string.Compare(x.Pattern, sigPattern, StringComparison.OrdinalIgnoreCase) == 0);
            if (sigListObj != null)
            {
                if (sigListObj.Offsets.Keys.Contains(sigModuleName))
                {
                    Logger.ShowError("Sig with this pattern already exists for this module");
                    return;
                }

                //sigListObj.Offsets.Add(sigModuleName, new List<IntPtr>());
            }
            else
            {
                var sigInfo = new Signature(sigModuleName, sigPattern, sigMask);
                if (!sigInfo.IsValid())
                    return;

                //sigInfo.Offsets.Add(sigModuleName, new List<IntPtr>());

                _sigList.Add(sigInfo);
            }

            this.UpdateTreeView();

            if (InstantSearchCheckBox.Checked)
                SearchButton.PerformClick();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (SigsTreeView.SelectedNode == null)
            {
                Logger.ShowError("You need to select something first");
                return;
            }

            var selectedNodeText = SigsTreeView.SelectedNode.Text;
            var parentNode = SigsTreeView.SelectedNode.Parent;

            if (parentNode == null)
            {
                var sigObj = _sigList.FirstOrDefault(x => x.Pattern.Equals(selectedNodeText));
                if (sigObj != null)
                    _sigList.Remove(sigObj);
            }
            else if (selectedNodeText.StartsWith("0x"))
            {
                var sigObj = _sigList.FirstOrDefault(x => x.Pattern.Equals(parentNode.Parent.Text));
                if (sigObj != null)
                    if (sigObj.Offsets.TryGetValue(parentNode.Text, out var offsets))
                        offsets.Remove((IntPtr)(Environment.Is64BitProcess
                            ? Convert.ToInt64(selectedNodeText, 16)
                            : Convert.ToInt32(selectedNodeText, 16)));
            }
            else
            {
                var sigObj = _sigList.FirstOrDefault(x => x.Pattern.Equals(parentNode.Text));
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

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (!_sigList.Any())
            {
                Logger.ShowError("No signatures to scan for");
                return;
            }

            if (!ProcNameTextBox.Text.Any())
            {
                Logger.ShowError("Process name cannot be empty");
                return;
            }

            if (_lastProcess == null || _lastProcess.Name != ProcNameTextBox.Text)
                _lastProcess = new ProcessMemory(ProcNameTextBox.Text);
            else
            if (!_lastProcess.IsValid())
                _lastProcess.Refresh();

            if (!_lastProcess.IsValid())
            {
                _lastProcess.Dispose();
                return;
            }

            Thread t = new Thread(delegate()
            {
                ProgressBar.Invoke((MethodInvoker) (() =>
                {
                    ProgressBar.Maximum = _sigList.Count;
                    ProgressBar.Value = ProgressBar.Minimum;
                }));

                Parallel.ForEach(_sigList, sig => {
                    _lastProcess.GetSignatureAddresses(sig);

                    ProgressBar.Invoke((MethodInvoker)(() => ProgressBar.Increment(1)));
                });

                ScanFinished();
            });

            t.Start();

        }

        private void ScanFinished()
        {
            this.UpdateTreeView();
            Logger.ShowDebug("Finished");
        }

        private void UpdateTreeView()
        {
            SigsTreeView.Invoke((MethodInvoker) (() =>
            {
                SigsTreeView.Nodes.Clear();

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
                    var multipleAdressesNodes = 0;

                    foreach (TreeNode node in sigNode.Nodes)
                    {
                        if (node.ForeColor.Equals(Color.Red))
                            emptyNodes++;
                        if (node.ForeColor.Equals(Color.Orange))
                            multipleAdressesNodes++;
                    }

                    var sigNodesCount = sigNode.GetNodeCount(false);

                    if (sigNodesCount != 0)
                        sigNode.ForeColor = multipleAdressesNodes != 0
                            ? Color.Orange
                            : sigNodesCount == emptyNodes
                                ? Color.Red
                                : Color.Green;
                    else
                        sigNode.ForeColor = Color.DarkRed;

                    SigsTreeView.Nodes.Add(sigNode);
                }
            }));

        }
    }
}
