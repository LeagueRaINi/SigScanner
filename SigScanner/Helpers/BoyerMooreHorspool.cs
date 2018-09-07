using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

            var sigByteArray = sig.Bytes.ToArray();
            foreach (var address in Search(moduleBuffer, sigByteArray, sig.MaskBool, GetBadMatchingTable(sigByteArray)))
                addressList.Add(IntPtr.Add(baseAddress, address));

            return addressList;
        }

        // INFO:
        // - 99% sure that this isnt 100% correct cause it skips the address where our pattern is supposed to be
        // - it can also be improved
        private static int[] GetBadMatchingTable(byte[] pattern)
        {
            var badMatchingTable = new int[256];

            for (int i = 0; i < 256; i++)
                badMatchingTable[i] = pattern.Length;
            for (int i = 0; i < pattern.Length - 1; i++)
                badMatchingTable[pattern[i] & 0xFF] = pattern.Length - i - 1;

            return badMatchingTable;
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

                //index += badMatchingTable[haystack[index + lastPatternByteIndex] & 0xFF];
                index++; // temp fix!
            }

            return addresses;
        }
    }
}
