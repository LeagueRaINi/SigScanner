using System;
using System.Collections.Generic;

namespace SigScanner.Helpers
{
    public static class SignatureScanner
    {

        public static List<IntPtr> FindPattern(byte[] moduleBuffer, Signature sig)
        {
            List<IntPtr> addressList = new List<IntPtr>();

            // invalid signature
            if (sig.Type == Signature.SigType.UNKNOWN)
                return addressList;

            // Loop the region and look for the pattern.
            for (int i = 0; i < moduleBuffer.Length - sig.Pattern.Length; i++)
            {
                if (SequenceCheck(moduleBuffer, i, sig.Bytes, sig.Mask))
                {
                    // The pattern was found, return it.
                    addressList.Add(new IntPtr(i));
                }
            }

            return addressList;
        }

        public static List<IntPtr> FindPatternHorspool(byte[] moduleBuffer, Signature sig, IntPtr baseAddress)
        {
            List<IntPtr> addressList = new List<IntPtr>();
            int addr = -1;

            // invalid signature
            if (sig.Type == Signature.SigType.UNKNOWN)
                return addressList;

            // TODO: move accumulation of multiple occurences to HorspoolSearch (avoids reinit of same pattern)
            do
            {
                addr = HorspoolSearch(moduleBuffer, sig.Bytes.ToArray(), sig.GetMaskBool(), addr < 0 ? 0 : addr + sig.Bytes.Count);
                if (addr < 0)   // no more occurences found
                    break;

                addressList.Add(IntPtr.Add(baseAddress, addr));
            } while (true);

            return addressList;
        }

        public static List<IntPtr> FindPatternSunday(byte[] moduleBuffer, Signature sig, IntPtr baseAddress)
        {
            List<IntPtr> addressList = new List<IntPtr>();

            // invalid signature
            if (sig.Type == Signature.SigType.UNKNOWN)
                return addressList;

            SundaySearch(moduleBuffer, sig.Bytes.ToArray(), sig.GetMaskBool()).ForEach(a =>
                addressList.Add(IntPtr.Add(baseAddress, a)));

            return addressList;
        }

        private static bool SequenceCheck(byte[] buffer, int offset, List<byte> pattern, string mask)
        {
            for (int x = 0; x < pattern.Count; x++)
            {
                if (mask[x] == '?')
                    continue;

                // compare pattern to memory
                if (mask[x] == 'x' && pattern[x] != buffer[offset + x])
                    return false;
            }

            // loop succeeded, pattern found
            return true;
        }

        private static IntPtr BoyerMoore(byte[] haystack, byte[] needle)
        {
            Int64 skip = 0;
            Int64[] badCharacters = new Int64[256];
            Int64 lastPatternByte = needle.Length - 1;

            for (Int64 i = 0; i < 256; ++i)
                badCharacters[i] = needle.Length;

            for (Int64 i = 0; i < lastPatternByte; ++i)
                badCharacters[needle[i]] = lastPatternByte - i;

            while (haystack.Length - skip >= needle.Length)
            {
                int i = needle.Length - 1;

                while (haystack[skip + i] == needle[i])
                {
                    if (i == 0)
                        return new IntPtr(skip);

                    i--;
                }

                skip = skip + badCharacters[haystack[(skip + lastPatternByte)]];
            }

            return IntPtr.Zero;
        }

        private static int HorspoolSearch(byte[] haystack, byte[] needle, bool[] mask, int startOffset = 0)
        {
            int i = startOffset;
            int j;
            int needleLength = needle.Length;
            int haystackLength = haystack.Length;
            int[] occ = new int[256];

            void OccInit()
            {
                int a;

                for (a = 0; a < 256; a++)
                    occ[a] = -1;

                for (j = 0; j < needleLength - 1; j++)
                {
                    a = needle[j];
                    occ[a] = j;
                }
            }

            OccInit();

            while (i <= haystackLength - needleLength)
            {
                j = needleLength - 1;

                while (j >= 0 && (needle[j] == haystack[i + j] || !mask[j]))
                    j--;

                if (j < 0)
                    return i;

                i += needleLength - 1;
                i -= occ[haystack[i]];
            }

            return -1;
        }

        private static List<int> SundaySearch(byte[] haystack, byte[] needle, bool[] mask)
        {
            int i = 0;
            int needleLength = needle.Length;
            int haystackLength = haystack.Length;
            int[] occ = new int[256];

            List<int> addressList = new List<int>();

            bool MatchesAt(int pos)
            {
                int index = 0;

                while (index < needleLength && (!mask[index] || needle[index] == haystack[pos + index]))
                    index++;

                return index == needleLength;
            }

            void OccInit()
            {
                int a;

                for (a = 0; a < 256; a++)
                    occ[a] = -1;

                for (int ji = 0; ji < needleLength; ji++)
                {
                    a = needle[ji];
                    occ[a] = ji;
                }
            }

            OccInit();

            while (i <= haystackLength - needleLength)
            {
                if (MatchesAt(i))
                    addressList.Add(i);

                i += needleLength;
                if (i < haystackLength)
                    i -= occ[haystack[i]];
            }

            return addressList;
        }
    }
}
