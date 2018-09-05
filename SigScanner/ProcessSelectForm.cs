using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SigScanner
{
    public partial class ProcessSelectForm : Form
    {
        private Process[] _processList;
        public Process SelectedProcess { get; private set; }

        public ProcessSelectForm(Process[] processList = null)
        {
            InitializeComponent();

            _processList = processList;
        }

        private void ProcessSelectForm_Load(object sender, EventArgs e)
        {
            ProcessListView.View = View.Details;
            ProcessListView.Columns.Add("Id", 50);
            ProcessListView.Columns.Add("Name", ProcessListView.Width - 75);

            if (_processList == null || _processList.Count() < 2)
                _processList = Process.GetProcesses();

            foreach (Process process in _processList)
                ProcessListView.Items.Add(new ListViewItem(new[] { process.Id.ToString(), process.ProcessName }));
        }

        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            if (ProcessListView.SelectedItems == null)
            {
                MessageBox.Show("You need to select a Process first", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedProcess = Process.GetProcessById(Convert.ToInt32(ProcessListView.SelectedItems[0].Text));

            this.Close();
        }
    }
}
