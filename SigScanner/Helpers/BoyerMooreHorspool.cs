using System;
using System.Linq;
using System.Collections.Generic;

namespace SigScanner.Helpers
{
    public static class BoyerMooreHorspool
    {
        public static List<IntPtr> FindPattern(byte[] moduleBuffer, Signature sig, IntPtr baseAddress)
        {
            var addressList = new List<IntPtr>();
            if (!sig.IsValid() || moduleBuffer.Length < sig.Bytes.Count)
            {
                Logger.ShowError("Could not find Pattern. Module Buffer was too small");
                return addressList;
            }

            addressList.AddRange(moduleBuffer.Search(sig.Bytes.ToArray(), sig.MaskBool, sig.GetBadMatchingTable())
                .Select(address => IntPtr.Add(baseAddress, address)));

            return addressList;
        }

        private static int[] GetBadMatchingTable(this Signature sig)
        {
            var badMatchingTable = new int[256];
            var lastPatternByteIndex = sig.Bytes.Count - 1;

            var lastDiff = lastPatternByteIndex - Array.LastIndexOf(sig.MaskBool, false);
            var firstDiff = lastPatternByteIndex - Array.IndexOf(sig.MaskBool, false);

            var diff = firstDiff > lastDiff ? firstDiff : lastDiff;
            if (diff == 0)
                diff = 1;

            for (var i = 0; i < 256; i++)
                badMatchingTable[i] = diff;
            for (var i = 0; i < lastPatternByteIndex; i++)
                badMatchingTable[sig.Bytes[i] & 0xFF] = lastPatternByteIndex - i;

            return badMatchingTable;
        }

        private static IEnumerable<int> Search(this IReadOnlyList<byte> haystack, IReadOnlyList<byte> needle, IReadOnlyList<bool> mask, IReadOnlyList<int> badMatchingTable)
        {
            var index = 0;
            var lastPatternByteIndex = needle.Count - 1;
            var addresses = new List<int>();

            while (index <= haystack.Count - needle.Count)
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
