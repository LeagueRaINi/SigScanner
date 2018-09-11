using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SigScanner.Helpers
{
    public class BoyerMooreHorspool
    {
        public static List<IntPtr> FindPattern(byte[] moduleBuffer, Signature sig, IntPtr baseAddress)
        {
            var addressList = new List<IntPtr>();
            if (!sig.IsValid() || moduleBuffer.Length < sig.Bytes.Count)
            {
                MessageBox.Show("Could not find Pattern. Module Buffer was too small", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return addressList;
            }

            foreach (var address in Search(moduleBuffer, sig.Bytes.ToArray(), sig.MaskBool, GetBadMatchingsTable(sig)))
                addressList.Add(IntPtr.Add(baseAddress, address));

            return addressList;
        }

        private static int[] GetBadMatchingsTable(Signature sig)
        {
            var badMatchingsTable = new int[256];
            var lastPatternByteIndex = sig.Bytes.Count - 1;

            var lastDiff = lastPatternByteIndex - Array.LastIndexOf(sig.MaskBool, false);
            var firstDiff = lastPatternByteIndex - Array.IndexOf(sig.MaskBool, false);

            var diff = firstDiff > lastDiff ? firstDiff : lastDiff;
            if (diff == 0)
                diff = 1;

            for (int i = 0; i < 256; i++)
                badMatchingsTable[i] = diff;
            for (int i = 0; i < lastPatternByteIndex; i++)
                badMatchingsTable[sig.Bytes[i] & 0xFF] = lastPatternByteIndex - i;

            return badMatchingsTable;
        }

        private static List<int> Search(byte[] haystack, byte[] needle, bool[] mask, int[] badMatchingTable)
        {
            var index = 0;
            var lastPatternByteIndex = needle.Length - 1;
            var addresses = new List<int>();

            while (index <= (haystack.Length - needle.Length))
            {
                for (var i = lastPatternByteIndex; haystack[index + i] == needle[i] || !mask[i]; --i)
                    if (i == 0)
                    {
                        addresses.Add(index);
                        break;
                    }

                index += badMatchingTable[haystack[index + lastPatternByteIndex] & 0xFF];
            }

            return addresses;
        }
    }
}
