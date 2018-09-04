using System;
using System.Runtime.InteropServices;

namespace SigScanner.Helpers.Natives
{
    public class Imports
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtReadVirtualMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(Enums.ProcessAccessFlags processAccess, bool bInheritHandle, int processId);
    }
}
