using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SigScanner.Helpers;

namespace SigScanner
{
    public partial class ProcessSelectionForm : Form
    {
        public Process SelectedProcess { get; private set; }

        public ProcessSelectionForm(Process[] processList = null)
        {
            InitializeComponent();

            if (processList == null)
                processList = Process.GetProcesses();

            this.UpdateListView(processList);
        }

        private void ProcessSelectionForm_SizeChanged(object sender, EventArgs e)
        {
            var width = ProcessListView.Width - 75;
            ProcessListView.Columns[1].Width = width / 2;
            ProcessListView.Columns[2].Width = width / 2;
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (ProcessListView.SelectedItems.Count < 1)
            {
                Logger.ShowError("You need to select a Process first");
                return;
            }

            if (!int.TryParse(ProcessListView.SelectedItems[0].Text, out var procId))
                throw new Exception("Failed to Parse Process Id");

            Process selectedProc;

            try
{
                selectedProc = Process.GetProcessById(procId);
            }
            catch
            {
                selectedProc = null;
            }

            if (!ProcessMemory.IsValidProcess(selectedProc))
            {
                Logger.ShowError("Failed to select Process. Process has already exited");

                this.UpdateListView(Process.GetProcesses());

                return;
            }

            this.SelectedProcess = selectedProc;
            this.Close();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            this.UpdateListView(Process.GetProcesses());
        }

		private void ProcessSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.Equals((sender as Button)?.Name, @"CloseButton"))
                this.SelectedProcess = null;
        }
		
        private void UpdateListView(IEnumerable<Process> procList)
        {
            ProcessListView.Items.Clear();

			var filterText = FilterTextBox.Text.ToLower();
			
            foreach (var proc in procList)
            {
                if (!string.IsNullOrEmpty(FilterTextBox.Text))
                    if (!proc.ProcessName.ToLower().Contains(filterText))
                        continue;

                ProcessListView.Items.Add(new ListViewItem(new[]
                    {
                        proc.Id.ToString(),
                        proc.ProcessName,
                        proc.MainWindowTitle
                    }));
            }
        }
    }
}
