using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SigScanner.Helpers.Natives;

namespace SigScanner.Helpers
{
    public class ProcessMemory : IDisposable
    {
        public string Name { get; private set; }
        public Process Process { get; private set; }
        public IntPtr Handle { get; private set; }
        public Dictionary<ProcessModule, byte[]> ModuleCache { get; private set; }
        public bool Disposed { get; private set; }

        public ProcessMemory(string procName)
        {
            if (string.IsNullOrEmpty(procName))
                throw new Exception("Failed to initialise ProcessMemory. Process Name was invalid");

            this.Name = procName;
            this.Process = GetProcess(procName);
            this.ModuleCache = new Dictionary<ProcessModule, byte[]>();

            if (IsValidProcess(this.Process))
                this.Handle = OpenHandle(this.Process, Enums.ProcessAccessFlags.VirtualMemoryRead);
        }

        public ProcessMemory(Process proc)
        {
            if (!IsValidProcess(proc))
                throw new Exception("Failed to initialise ProcessMemory. Process was invalid");

            this.Name = proc.ProcessName;
            this.Process = proc;
            this.Handle = OpenHandle(proc, Enums.ProcessAccessFlags.VirtualMemoryRead);
            this.ModuleCache = new Dictionary<ProcessModule, byte[]>();
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

                if (this.IsValid())
                    Imports.CloseHandle(this.Handle);
            }

            // Free any unmanaged objects here.
            //

            this.ModuleCache?.Clear();

            this.Disposed = true;
        }

        public bool IsValid()
        {
            return IsValidProcess(this.Process) && this.HasHandle();
        }

        public bool HasHandle()
        {
            return this.Handle != IntPtr.Zero;
        }

        public void Refresh()
        {
            if (IsValidProcess(this.Process))
                return;
            if ((this.Process = GetProcess(this.Name)) == null)
                return;

            this.Handle = OpenHandle(this.Process, Enums.ProcessAccessFlags.VirtualMemoryRead);
            this.ModuleCache.Clear();
        }

        public ProcessModule GetModule(string moduleName)
        {
            if (!IsValidProcess(this.Process))
            {
                Logger.ShowError("Failed to get Module. Process is invalid");
                return null;
            }

            var cachedModule = this.ModuleCache.Keys.FirstOrDefault(x => string.Compare(x.ModuleName, moduleName, StringComparison.OrdinalIgnoreCase) == 0);
            if (cachedModule != null)
                return cachedModule;

            foreach (ProcessModule module in this.Process.Modules)
                if (string.Compare(module.ModuleName, moduleName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.ModuleCache.Add(module, new byte[] { });
                    return module;
                }

            Logger.ShowError($"Could not find Module: {moduleName}");
            return null;
        }

        public byte[] DumpModule(ProcessModule module)
        {
            if (this.ModuleCache.TryGetValue(module, out var moduleBuffer) && moduleBuffer.Any())
                return moduleBuffer;

            if (ReadMemory(module.BaseAddress, module.ModuleMemorySize, out var buf))
            {
                this.ModuleCache[module] = buf;
                return buf;
            }

            Logger.ShowError($"Failed to Dump Module: {module.ModuleName}");
            return buf;
        }

        public void GetSignatureAddresses(Signature sig)
        {
            var moduleList = new List<ProcessModule>();

            if (!IsValidProcess(this.Process))
            {
                Logger.ShowError("Failed to get Signature Addresses. Process is invalid");
                return;
            }

            if (!string.IsNullOrEmpty(sig.ModuleName) && string.Compare(sig.ModuleName, "Scan all", StringComparison.OrdinalIgnoreCase) != 0)
                moduleList.Add(GetModule(sig.ModuleName));
            else
                foreach (ProcessModule processModule in this.Process.Modules)
                {
                    // TODO: check if dll is not a system dll
                    if (string.Compare(processModule.ModuleName, "KERNEL32.dll", StringComparison.OrdinalIgnoreCase) == 0)
                        continue;

                    moduleList.Add(processModule);
                }

            if (moduleList.Count == 0)
            {
                Logger.ShowError("Failed to get Signature Addresses. Could not find Modules");
                return;
            }

            foreach (var mod in moduleList)
            {
                var addresses = BoyerMooreHorspool.FindPattern(this.DumpModule(mod), sig, mod.BaseAddress);
                if (addresses.Count <= 0)
                    continue;

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

        public bool ReadMemory(IntPtr address, int size, out byte[] bytes)
        {
            bytes = new byte[size];

            if (this.IsValid())
                return Imports.NtReadVirtualMemory(this.Handle, address, bytes, size, out var lpNumberOfBytesRead) == 0;

            Logger.ShowError("Failed to read Memory. Process is invalid");
            return false;

        }

        public static bool IsValidProcess(Process proc)
        {
            return proc != null && !proc.HasExited;
        }

        public static Process GetProcess(string procName)
        {
            var procList = Process.GetProcessesByName(procName);
            if (procList.Length == 0)
            {
                Logger.ShowError($"Could not find Process: {procName}");
                return null;
            }

            if (procList.Length == 1
                || Logger.ShowInfo("Found one or more Processes do you want to select a specific one?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return procList.FirstOrDefault();

            var procSelectionForm = new ProcessSelectionForm(procList);

            procSelectionForm.ShowDialog();

            return procSelectionForm.SelectedProcess;
        }

        public static IntPtr OpenHandle(Process proc, Enums.ProcessAccessFlags accessFlags)
        {
            if (!IsValidProcess(proc))
            {
                Logger.ShowError("Failed to open Handle. Process is invalid");
                return IntPtr.Zero;
            }

            var handle = Imports.OpenProcess(accessFlags, false, proc.Id);
            if (handle == IntPtr.Zero)
                Logger.ShowError($"Failed to open Handle to Process: {proc.ProcessName}");

            return handle;
        }
    }
}
