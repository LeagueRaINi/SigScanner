using System.Diagnostics;
using System.Windows.Forms;

namespace SigScanner.Helpers
{
    public static class Logger
    {
        public static DialogResult ShowInfo(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(message, @"Info", buttons, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarning(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(message, @"Warning", buttons, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowError(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(message, @"Error", buttons, MessageBoxIcon.Error);
        }

        [Conditional("DEBUG")]
        public static void ShowDebug(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            MessageBox.Show(message, @"Debug", buttons, MessageBoxIcon.None);
        }
    }
}
