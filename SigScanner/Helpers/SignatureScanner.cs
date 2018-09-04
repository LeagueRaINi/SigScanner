using System;
using System.Collections.Generic;

namespace SigScanner.Helpers
{
    public static class SignatureScanner
    {

        public static IntPtr FindPattern(byte[] moduleBuffer, Signature sig)
        {
            if (sig.Type == Signature.SigType.UNKNOWN) // invalid signature
                return IntPtr.Zero;

            // Loop the region and look for the pattern.
            for (int x = 0; x < moduleBuffer.Length; x++)
            {
                if (SequenceCheck(moduleBuffer, x, sig.Bytes, sig.Mask))
                {
                    // The pattern was found, return it.
                    // TODO: add base address
                    return new IntPtr(x /*+ sig.Offset*/);
                }
            }

            return IntPtr.Zero;
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
