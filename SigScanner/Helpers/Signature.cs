using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SigScanner.Helpers
{
    public class Signature
    {
        public enum SigType
        {
            IDA,
            CODE,
            UNKNOWN
        }

        public string Pattern { get; private set; }
        public string Mask { get; private set; }
        public SigType Type { get; private set; }
        public List<byte> Bytes { get; private set; }
        public string ModuleName { get; private set; }
        public List<IntPtr> Offsets { get; set; }

        private static Regex _idaRegex = new Regex(@"(\s?[a-fA-F0-9?]{1,2}\s?)");
        private static Regex _codeRegex = new Regex(@"(\\x)([a-fA-F0-9]{1,2})");

        public Signature(string moduleName, string pattern, string mask = "")
        {
            this.Pattern = pattern;
            this.Mask = mask.ToLower();
            this.ModuleName = moduleName;
            this.Type = GetSigType(pattern, mask, true);
            this.Bytes = GetSigBytes(this.Type, pattern, mask);
            this.Offsets = new List<IntPtr>();
        }

        public bool IsValid()
        {
            return this.Type != SigType.UNKNOWN && this.Bytes.Any();
        }

        public static SigType GetSigType(string pattern, string mask = "", bool showError = false)
        {
            var idaMatches = _idaRegex.Matches(pattern);
            if (idaMatches.Count > pattern.Length / 3)
            {
                var splitPattern = pattern.Split(' ');
                if (splitPattern.Count() == idaMatches.Count)
                    return SigType.IDA;
            }

            var codeMatches = _codeRegex.Matches(pattern);
            if (codeMatches.Count == (pattern.Length / 4))
            {
                if (IsValidMask(mask))
                    return SigType.CODE;
                if (showError)
                    MessageBox.Show("Invalid Mask", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (showError)
                MessageBox.Show("Unknown Pattern format", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return SigType.UNKNOWN;
        }

        public static List<byte> GetSigBytes(SigType type, string pattern, string mask = "")
        {
            var bytes = new List<byte>();

            switch (type)
            {
                case SigType.IDA:
                    foreach (var hex in pattern.Split(' '))
                        bytes.Add(hex.Equals("?") || hex.Equals("??")
                            ? (byte)0x0
                            : Convert.ToByte(hex, 16));
                    return bytes;
                case SigType.CODE:
                    var strippedPattern = pattern.Replace("\\x", "");
                    for (int i = 0; i < mask.Length; i++)
                        bytes.Add(Convert.ToByte(strippedPattern.Substring(i * 2, 2), 16));
                    return bytes;
                default:
                    return bytes;
            }
        }

        public bool[] GetMaskBool()
        {
            return Mask.Select(x => x == 'x').ToArray();
        }

        public static bool IsValidMask(string mask)
        {
            if (string.IsNullOrEmpty(mask))
                return false;

            foreach (char charr in mask)
                if (!charr.Equals('x') && !charr.Equals('?'))
                    return false;

            return true;
        }
    }
}
