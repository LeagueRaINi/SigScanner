using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SigScanner.Helpers
{
    public class ProcessMemory : IDisposable
    {
        public Process Process { get; private set; }
        public string ProcessName { get; private set; }
        public IntPtr ProcessHandle { get; private set; }

        public ProcessMemory(string processName, Natives.Enums.ProcessAccessFlags handleAccess)
        {
            this.ProcessName = processName;
            this.GetProcess(handleAccess);
        }

        ~ProcessMemory()
        {
            this.CloseHandle();
        }

        public void Dispose()
        {
            this.CloseHandle();
        }

        public static bool DoesProcessExist(string processName, out Process[] processList)
        {
            processList = Process.GetProcessesByName(processName);
            return processList.Any();
        }

        public bool IsAlive()
        {
            return this.Process != null && !this.Process.HasExited;
        }

        public bool HasHandle()
        {
            return this.ProcessHandle != null && this.ProcessHandle != IntPtr.Zero;
        }

        public void GetProcess(Natives.Enums.ProcessAccessFlags handleAccess)
        {
            if (this.IsAlive() && this.HasHandle())
                return;

            this.Process = null;
            this.ProcessHandle = IntPtr.Zero;

            if (!DoesProcessExist(this.ProcessName, out var processList))
            {
                MessageBox.Show($"Could not find Process: {this.ProcessName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (processList.Count() > 1 &&
                MessageBox.Show(
                    $"Found {processList.Count()} Processes with the Name {this.ProcessName}. Do you want to select one or let me pick a random one?",
                    "Warning!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var processSelection = new ProcessSelectForm(processList);

                processSelection.FormClosing += (sender, e) =>
                {
                    var form = sender as ProcessSelectForm;
                    if (form.SelectedProcess != null)
                        this.Process = form.SelectedProcess;
                };

                processSelection.ShowDialog();

                if (!this.IsAlive())
                {
                    MessageBox.Show("Selected Process is invalid or has exited", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
                this.Process = processList.FirstOrDefault();

            this.ProcessHandle = Natives.Imports.OpenProcess(handleAccess, false, this.Process.Id);

            if (this.ProcessHandle == IntPtr.Zero)
                MessageBox.Show($"Could not Open Handle to Process: {this.ProcessName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CloseHandle()
        {
            if (!this.IsAlive() || !this.HasHandle())
                return;

            Natives.Imports.CloseHandle(this.ProcessHandle);
        }

        public ProcessModule GetModule(string moduleName)
        {
            if (!this.IsAlive())
            {
                MessageBox.Show("Process is dead", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            foreach (ProcessModule module in this.Process.Modules)
                if (module.ModuleName.ToLower().Equals(moduleName.ToLower()))
                    return module;

            MessageBox.Show($"Failed to find Module: {moduleName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        public bool ReadMemory(IntPtr address, int size, out byte[] bytes)
        {
            bytes = new byte[size];

            if (!this.IsAlive())
            {
                MessageBox.Show("Process is dead", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!this.HasHandle())
            {
                MessageBox.Show("Process Handle is invalid", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return Natives.Imports.NtReadVirtualMemory(this.ProcessHandle, address, bytes, size, out var lpNumberOfBytesRead) == 0;
        }

        public void GetSignatureAddresses(Signature sig)
        {
            Dictionary<string, byte[]> memList = new Dictionary<string, byte[]>();

            memList = DumpModule(sig.ModuleName);

            if (memList.Count == 0) // could not find module!
            {
                // TODO: msgbox
                return;
            }

            foreach (var mem in memList)
            {
                var addresses = SignatureScanner.FindPattern(mem.Value, sig);

                if (addresses.Count > 1)
                    sig.Offsets.Add(mem.Key, addresses);
            }
        }

        private Dictionary<string, byte[]> DumpModule(string moduleNames = null)
        {
            var bufferList = new Dictionary<string, byte[]>();

            foreach (ProcessModule module in Process.Modules)
            {
                if (moduleNames != null && !moduleNames.Equals("Scan All"))
                    if (!moduleNames.Contains(module.ModuleName))
                        continue;

                if (!ReadMemory(module.BaseAddress, module.ModuleMemorySize, out var bytes))
                    continue;

                bufferList.Add(module.ModuleName, bytes);
            }

            return bufferList;
        }
    }
}
