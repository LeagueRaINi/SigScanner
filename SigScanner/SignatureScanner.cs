using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigScanner
{
    static class SignatureScanner
    {

        public static IntPtr FindPattern(byte[] mem, ISignature sig)
        {
            byte[] pattern = sig.GetPattern();
            string mask = sig.GetMask();

            if (mask.Length != pattern.Length)     // invalid signature
                return IntPtr.Zero;

            // Loop the region and look for the pattern.
            for (int x = 0; x < mem.Length; x++)
            {
                if (SequenceCheck(mem, x, pattern, mask))
                {
                    // The pattern was found, return it.
                    // TODO: add base address
                    return new IntPtr(x + sig.Offset);
                }
            }

            return IntPtr.Zero;
        }

        private static bool SequenceCheck(byte[] mem, int offset, byte[] pattern, string mask)
        {
            for (int x = 0; x < pattern.Length; x++)
            {
                if (mask[x] == '?')
                    continue;

                // compare pattern to memory
                if (mask[x] == 'x' && pattern[x] != mem[offset + x])
                    return false;
            }
            // loop succeeded, pattern found
            return true;
        }
    }
}
