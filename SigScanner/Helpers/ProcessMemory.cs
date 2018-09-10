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
        public bool Disposed { get; private set; }
        public Dictionary<ProcessModule, byte[]> ModuleCache { get; private set; }

        public ProcessMemory(string processName, Natives.Enums.ProcessAccessFlags handleAccess)
        {
            this.ProcessName = processName;
            this.ModuleCache = new Dictionary<ProcessModule, byte[]>();

            this.GetProcess(handleAccess);
        }

        ~ProcessMemory()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.Disposed)
                return;

            if (disposing)
            {
                // Free any managed objects here.
                //

                this.CloseHandle();
            }

            // Free any unmanaged objects here.
            //

            if (this.ModuleCache != null)
                this.ModuleCache.Clear();

            this.Disposed = true;
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
            this.ModuleCache.Clear();

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

        public void GetSignatureAddresses(Signature sig)
        {
            var moduleList = new List<ProcessModule>();

            if (!this.IsAlive())
            {
                MessageBox.Show("Process is dead", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sig.ModuleName != null && !sig.ModuleName.Equals("Scan all"))
                moduleList.Add(GetModule(sig.ModuleName));
            else
                foreach (ProcessModule processModule in this.Process.Modules)
                {
                    // TODO: check if dll is not a system dll
                    if (string.Compare(processModule.ModuleName, "KERNEL32.dll", true) == 0)
                        continue;

                    moduleList.Add(processModule);
                }

            if (moduleList.Count == 0)
            {
                MessageBox.Show("Could not find any Module", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var mod in moduleList)
            {
                var addresses = BoyerMooreHorspool.FindPattern(this.DumpModule(mod), sig, mod.BaseAddress);
                if (addresses.Count > 0)
                    if (sig.Offsets.TryGetValue(mod.ModuleName, out var offsets))
                    {
                        offsets.Clear();

                        foreach (var address in addresses)
                            offsets.Add(address);
                    }
                    else
                        sig.Offsets.Add(mod.ModuleName, addresses);
            }
        }

        public byte[] DumpModule(ProcessModule module)
        {
            if (this.ModuleCache.TryGetValue(module, out var moduleBuffer) && moduleBuffer.Any())
                return moduleBuffer;

            var buffer = new byte[module.ModuleMemorySize];
            if (ReadMemory(module.BaseAddress, module.ModuleMemorySize, out buffer))
            {
                this.ModuleCache[module] = buffer;
                return buffer;
            }

            MessageBox.Show($"Failed to Dump Module: {module.ModuleName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return buffer;
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

        public static bool DoesProcessExist(string processName, out Process[] processList)
        {
            processList = Process.GetProcessesByName(processName);
            return processList.Any();
        }
    }
}
