using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

        private static Regex _idaRegex = new Regex(@"(\s?[a-fA-F0-9?]{1,2}\s?)");
        private static Regex _codeRegex = new Regex(@"(\\x)([a-fA-F0-9]{1,2})");

        public Signature(string moduleName, string pattern, string mask = "")
        {
            this.Pattern = pattern.ToLower();
            this.Mask = mask.ToLower();
            this.ModuleName = moduleName;
            this.Type = SigType.UNKNOWN;

            var idaMatches = _idaRegex.Matches(this.Pattern);
            if (idaMatches.Count > this.Pattern.Length / 3)
            {
                var splitPattern = this.Pattern.Split(' ');
                if (splitPattern.Count() == idaMatches.Count)
                {
                    this.Type = SigType.IDA;
                    this.Bytes = new List<byte>();

                    foreach(var hex in splitPattern)
                        this.Bytes.Add(hex.Equals("?") || hex.Equals("??")
                            ? (byte)0x0
                            : Convert.ToByte(hex, 16));

                    return;
                }
            }

            var codeMatches = _codeRegex.Matches(this.Pattern);
            if (codeMatches.Count == (this.Pattern.Length / 4))
            {
                if (IsValidMask())
                {
                    this.Type = SigType.CODE;
                    this.Bytes = new List<byte>();

                    var parsed = this.Pattern.Replace("\\x", "");
                    for (int i = 0; i < this.Mask.Length; i++)
                        this.Bytes.Add(Convert.ToByte(parsed.Substring(i * 2, 2), 16));

                    return;
                }

                MessageBox.Show("Invalid Mask", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("Unknown Pattern format", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool IsValidMask()
        {
            if (this.Mask != string.Empty)
                foreach (char charr in this.Mask)
                    if (!charr.Equals('x') && !charr.Equals('?'))
                        return false;

            return true;
        }
    }
}
