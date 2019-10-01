using System;
using System.Collections.Generic;

namespace SigScanner.Helpers
{
    /// <summary>
    /// Pattern Scanning Implementation using the Boyer Moore Horspool Algorithm
    /// </summary>
    public static class BoyerMooreHorspool
    {
        /// <summary>
        /// Searches for a Signature in the given Buffer
        /// </summary>
        /// <param name="moduleBuffer">Buffer to search the Pattern in</param>
        /// <param name="sig">Signature to search in the Buffer</param>
        /// <param name="baseAddress">Base Address that gets add to the Addresses</param>
        /// <returns>Address Array</returns>
        public static List<IntPtr> FindPattern(byte[] buffer, Signature sig, IntPtr baseAddress)
        {
            if (!sig.IsValid())
            {
                Logger.ShowError("Could not find Pattern. Signature is invalid");
                return null;
            }

            if (buffer.Length < sig.Bytes.Count)
            {
                Logger.ShowError("Could not find Pattern. Module Buffer cannot be smaller than the Signature");
                return null;
            }

            return sig.SearchPattern(buffer, baseAddress);
        }

        /// <summary>
        /// Creates the Skip Table
        /// </summary>
        /// <param name="sig">Signature object</param>
        /// <returns>Skip Table Array</returns>
        private static int[] CreateMatchingsTable(this Signature sig)
        {
            var skipTable = new int[256];
            var lastIndex = sig.Bytes.Count - 1;

            // TODO:
            // not the best way but it works for now
            var diff = lastIndex - Math.Max(Array.LastIndexOf(sig.MaskBool, false), 0);
            if (diff == 0)
            {
                diff = 1;
            }

            for (var i = 0; i < skipTable.Length; i++)
            {
                skipTable[i] = diff;
            }

            for (var i = lastIndex - diff; i < lastIndex; i++)
            {
                skipTable[sig.Bytes[i]] = lastIndex - i;
            }

            return skipTable;
        }

        /// <summary>
        /// Searches for a Pattern in a Byte Array
        /// </summary>
        /// <param name="sig">Signature to search for</param>
        /// <param name="data">Our Haystack</param>
        /// <param name="baseAddress">Base Address that gets add to the Addresses</param>
        /// <returns>List of Addresses it found</returns>
        public static List<IntPtr> SearchPattern(this Signature sig, byte[] data, IntPtr baseAddress)
        {
            var patternLen = sig.Bytes.Count;
            var lastPatternIndex = patternLen - 1;
            var skipTable = sig.CreateMatchingsTable();
            var adressList = new List<IntPtr>();

            for (var i = 0; i <= data.Length - patternLen; i += Math.Max(skipTable[data[i + lastPatternIndex] & 0xFF], 1))
            {
                for (var j = lastPatternIndex; !sig.MaskBool[j] || data[i + j] == sig.Bytes[j]; --j)
                {
                    if (j == 0)
                    {
                        adressList.Add(IntPtr.Add(baseAddress, i));
                        break;
                    }
                }
            }

            return adressList;
        }
    }
}
