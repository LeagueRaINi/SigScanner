using System;
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

        public ProcessMemory()
        {

        }

        ~ProcessMemory()
        {
            this.CloseHandle();

            this.Process = null;
            this.ProcessName = string.Empty;
            this.ProcessHandle = IntPtr.Zero;
        }

        public void Dispose()
        {
            this.CloseHandle();
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

            var processList = Process.GetProcessesByName(this.ProcessName);
            if (processList.Count() == 0)
            {
                MessageBox.Show($"Could not find Process: {this.ProcessName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Process = processList.FirstOrDefault();
            this.ProcessHandle = Natives.Imports.OpenProcess(handleAccess, false, this.Process.Id);

            if (this.ProcessHandle == IntPtr.Zero)
                MessageBox.Show($"Could not Open Handle to Process: {this.ProcessName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CloseHandle()
        {
            if (!this.IsAlive() && !this.HasHandle())
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
    }
}
