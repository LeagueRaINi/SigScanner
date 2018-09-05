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
            for (int x = 0; x < moduleBuffer.Length; x++)
            {
                if (SequenceCheck(moduleBuffer, x, sig.Bytes, sig.Mask))
                {
                    // The pattern was found, return it.
                    addressList.Add(new IntPtr(x));
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
    }
}
