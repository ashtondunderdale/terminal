using System;
using System.Drawing;
using System.Windows.Forms;

namespace Terminal
{
    internal class Helpers
    {
        public static void OutputSuccess(RichTextBox outputBox, string message)
        {
            AppendColoredText(outputBox, Color.Green, "+", message);
        }

        public static void OutputError(RichTextBox outputBox, string message)
        {
            AppendColoredText(outputBox, Color.Red, "-", message);
        }

        public static void OutputInformation(RichTextBox outputBox, string message)
        {
            AppendColoredText(outputBox, Color.CornflowerBlue, "i", message);
        }

        private static void AppendColoredText(RichTextBox outputBox, Color textColor, string symbol, string message)
        {
            outputBox.SelectionColor = textColor;
            outputBox.AppendText($"{symbol} ");
            outputBox.SelectionColor = outputBox.ForeColor;
            outputBox.AppendText($"{message}{Environment.NewLine}\n");
        }
    }
}
