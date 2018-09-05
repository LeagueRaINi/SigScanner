﻿using System;
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

        public ProcessMemory()
        {
            //
        }

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

        public IntPtr GetSignatureAddress(Signature sig)
        {
            var buf = this.DumpModule(sig.ModuleName);
            return SignatureScanner.FindPattern(buf, sig);
        }

        private byte[] DumpModule(string moduleName = "")
        {
            var bytes = new byte[]{};

            if (string.IsNullOrWhiteSpace(moduleName))
            {
                IEnumerable<byte> modDump = new List<byte>();

                foreach (ProcessModule mod in Process.Modules)
                {
                    ReadMemory(mod.BaseAddress, mod.ModuleMemorySize, out bytes);
                    modDump = modDump.Concat(bytes);
                }

                bytes = modDump.ToArray();
            }
            else
            {
                var module = GetModule(moduleName);
                ReadMemory(module.BaseAddress, module.ModuleMemorySize, out bytes);
            }

            return bytes;
        }
    }
}
