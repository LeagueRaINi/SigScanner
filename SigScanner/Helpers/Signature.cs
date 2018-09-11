using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SigScanner.Helpers
{
    public class Signature
    {
        public enum SigType
        {
            Ida,
            Code,
            Unknown
        }

        public string Pattern { get; private set; }
        public bool[] MaskBool { get; private set; }
        public SigType Type { get; private set; }
        public List<byte> Bytes { get; private set; }
        public string ModuleName { get; private set; }
        public Dictionary<string, List<IntPtr>> Offsets { get; set; }

        private static readonly Regex _idaRegex = new Regex(@"(\s?[a-fA-F0-9?]{1,2}\s?)");
        private static readonly Regex _codeRegex = new Regex(@"(\\x)([a-fA-F0-9]{1,2})");

        public Signature(string moduleName, string pattern, string mask = "")
        {
            this.Pattern = pattern;
            this.ModuleName = moduleName;
            this.Type = GetSigType(pattern, mask);
            this.Bytes = GetSigBytes(this.Type, pattern, mask);
            this.MaskBool = GetMaskBool(this.Type, mask, pattern);
            this.Offsets = new Dictionary<string, List<IntPtr>>();
        }

        public bool IsValid()
        {
            return this.Type != SigType.Unknown && this.Bytes.Any();
        }

        private static bool IsValidMaskFormat(int patternBytes, string mask)
        {
            if (string.IsNullOrEmpty(mask))
                return false;

            foreach (var c in mask)
                if (!c.Equals('x') && !c.Equals('?'))
                    return false;

            return patternBytes == mask.Length;
        }

        private static SigType GetSigType(string pattern, string mask = "")
        {
            var idaMatches = _idaRegex.Matches(pattern);
            if (idaMatches.Count > pattern.Length / 3)
            {
                var splitPattern = pattern.Split(' ');
                if (splitPattern.Length == idaMatches.Count)
                    return SigType.Ida;
            }

            var codeMatches = _codeRegex.Matches(pattern);
            if (codeMatches.Count == pattern.Length / 4)
            {
                if (IsValidMaskFormat(codeMatches.Count, mask))
                    return SigType.Code;

                Logger.ShowError("Invalid Mask");
            }

            Logger.ShowError("Unknown Pattern format");

            return SigType.Unknown;
        }

        private static List<byte> GetSigBytes(SigType type, string pattern, string mask = "")
        {
            var bytes = new List<byte>();

            switch (type)
            {
                case SigType.Ida:
                    bytes.AddRange(from hex in pattern.Split(' ')
                        select hex.Contains("?")
                            ? (byte) 0x0
                            : Convert.ToByte(hex, 16));
                    break;
                case SigType.Code:
                    var strippedPattern = pattern.Replace("\\x", "");
                    for (var i = 0; i < mask.Length; i++)
                        bytes.Add(Convert.ToByte(strippedPattern.Substring(i * 2, 2), 16));
                    break;
                case SigType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return bytes;
        }

        private static bool[] GetMaskBool(SigType type, string mask, string pattern = "")
        {
            if (type != SigType.Ida || string.IsNullOrEmpty(pattern))
                return mask.Select(x => x == 'x').ToArray();

            var sb = new StringBuilder();
            foreach (var hex in pattern.Split(' '))
                sb.Append(hex.Equals("?") || hex.Equals("??") ? '?' : 'x');

            return sb.ToString().Select(x => x == 'x').ToArray();
        }
    }
}
