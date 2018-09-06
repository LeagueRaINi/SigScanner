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
    }
}
