using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SigScanner.Helpers
{
    public class ProcessMemory
    {
        public Process Process { get; private set; }
        public IntPtr ProcessHandle { get; private set; }

        public ProcessMemory(string processName, Natives.Enums.ProcessAccessFlags handleAccess)
        {
            var processList = Process.GetProcessesByName(processName);
            if (processList.Count() == 0)
            {
                MessageBox.Show($"Could not find Process: {processName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Process = processList.FirstOrDefault();
            this.ProcessHandle = Natives.Imports.OpenProcess(handleAccess, false, this.Process.Id);

            if (this.ProcessHandle == IntPtr.Zero)
                MessageBox.Show($"Could not Open Handle to Process: {processName}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        ~ProcessMemory()
        {
            this.CloseHandle();
        }

        public bool CanRPM()
        {
            return this.IsAlive() && this.HasHandle();
        }

        public bool IsAlive()
        {
            return this.Process != null && !this.Process.HasExited;
        }

        public bool HasHandle()
        {
            return this.ProcessHandle != null && this.ProcessHandle != IntPtr.Zero;
        }

        public void CloseHandle()
        {
            if (!this.IsAlive())
                return;
            if (!this.HasHandle())
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
