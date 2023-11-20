namespace Terminal;

internal class Helpers
{
    public static void OutputSuccess(RichTextBox outputBox, string message)
    {
        AppendColoredText(outputBox, Color.DarkSlateBlue, "+", message);
    }

    public static void OutputError(RichTextBox outputBox, string message)
    {
        AppendColoredText(outputBox, Color.IndianRed, "-", message);
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
        outputBox.AppendText($"{message}{Environment.NewLine}");
    }
}
