using System;
using System.Runtime.InteropServices;

namespace SigScanner.Helpers.Natives
{
    public class Imports
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtReadVirtualMemory(IntPtr processHandle, IntPtr baseAddress, [In, Out] byte[] buffer, int size, out IntPtr numberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(Enums.ProcessAccessFlags processAccess, bool inheritHandle, int processId);
    }
}