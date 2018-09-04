using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SigScanner.Helpers.Natives.Enums.ProcessAccessFlags;

namespace SigScanner
{
    class ProcessMemory
    {
        private const string OffsetPattern = "(\\+|\\-){0,1}(0x){0,1}[a-fA-F0-9]{1,}";

        private Process process;
        private IntPtr processHandle;

        private static int iNumberOfBytesRead;
        //private static int iNumberOfBytesWritten;


        public void Initialize(string processName)
        {
            // TODO: prompt if multiple instances found
            process = Process.GetProcessesByName(processName)[0];
            processHandle = OpenProcess((int)(VirtualMemoryOperation | VirtualMemoryRead), false, process.Id);

            if (processHandle == IntPtr.Zero)
                throw new InvalidOperationException("Could not open handle to process");
        }

        public T ReadMemory<T>(int address) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[ByteSize];
            ReadProcessMemory((int)processHandle, address, buffer, buffer.Length, ref iNumberOfBytesRead);
            return ByteArrayToStructure<T>(buffer);
        }

        public float[] ReadMatrix<T>(int address, int matrixSize) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[ByteSize * matrixSize];
            ReadProcessMemory((int)processHandle, address, buffer, buffer.Length, ref iNumberOfBytesRead);
            return ConvertToFloatArray(buffer);
        }
/*
        public static void WriteMemory<T>(int address, object value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);
            WriteProcessMemory((int)processHandle, address, buffer, buffer.Length, out iNumberOfBytesWritten);
        }
*/

        public ProcessModule FindModule(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.ToLower().Equals(name.ToLower()))
                    return module;
            }
            return null;
        }

#region GetAddress Helpers (deref)

        public IntPtr GetAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            string moduleName = null;
            int index = address.IndexOf('"');
            if (index != -1)
            {
                // Module name at the beginning
                int endIndex = address.IndexOf('"', index + 1);

                if (endIndex == -1)
                    throw new ArgumentException("Invalid module name. Could not find matching \"");

                moduleName = address.Substring(index + 1, endIndex - 1);
                address = address.Substring(endIndex + 1);
            }

            int[] offsets = GetAddressOffsets(address);
            int[] _offsets = null;

            IntPtr baseAddress = offsets != null && offsets.Length > 0 ?
                (IntPtr)offsets[0] : IntPtr.Zero;

            if (offsets != null && offsets.Length > 1)
            {
                _offsets = new int[offsets.Length - 1];

                for (int i = 0; i < offsets.Length - 1; i++)
                    _offsets[i] = offsets[i + 1];
            }

            if (moduleName != null)
                return GetAddress(moduleName, baseAddress, _offsets);

            return GetAddress(baseAddress, _offsets);
        }

        public IntPtr GetAddress(string moduleName, IntPtr baseAddress, int[] offsets)
        {
            if (string.IsNullOrEmpty(moduleName))
                throw new ArgumentNullException(nameof(moduleName));

            ProcessModule module = FindModule(moduleName);
            if (module == null)
                return IntPtr.Zero;

            int address = module.BaseAddress.ToInt32() + baseAddress.ToInt32();

            return GetAddress((IntPtr)address, offsets);
        }

        private IntPtr GetAddress(IntPtr baseAddress, int[] offsets)
        {
            if (baseAddress == IntPtr.Zero)
                throw new ArgumentException("Invalid base address");

            int address = baseAddress.ToInt32();

            if (offsets != null && offsets.Length > 0)
            {
                foreach (int offset in offsets)
                    address = ReadMemory<Int32>(address) + offset;
            }

            return (IntPtr)address;
        }

        protected static int[] GetAddressOffsets(string address)
        {
            if (string.IsNullOrEmpty(address))
                return new int[0];

            MatchCollection matches = Regex.Matches(address, OffsetPattern);
            int[] offsets = new int[matches.Count];
            string value;
            char ch;

            for (int i = 0; i < matches.Count; i++)
            {
                ch = matches[i].Value[0];
                if (ch == '+' || ch == '-')
                    value = matches[i].Value.Substring(1);
                else
                    value = matches[i].Value;

                offsets[i] = Convert.ToInt32(value, 16);

                if (ch == '-')
                    offsets[i] = -offsets[i];
            }

            return offsets;
        }

#endregion



#region Transformation

        public static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentException();

            float[] floats = new float[bytes.Length / 4];

            for (int i = 0; i < floats.Length; i++)
                floats[i] = BitConverter.ToSingle(bytes, i * 4);

            return floats;
        }

        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        private static byte[] StructureToByteArray(object obj)
        {
            int length = Marshal.SizeOf(obj);
            byte[] array = new byte[length];
            IntPtr pointer = Marshal.AllocHGlobal(length);

            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, array, 0, length);
            Marshal.FreeHGlobal(pointer);

            return array;
        }

#endregion

#region DllImports

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);

#endregion
    }
}
