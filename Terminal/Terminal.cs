namespace Terminal;

public partial class Terminal : Form
{
    public Terminal()
    {
        InitializeComponent();
        commandBox.KeyDown += InputTextBox_KeyDown;

        outputBox.ReadOnly = true;
        outputBox.AppendText("Sharp terminal [Version 0.0.1]\n\n");
    }

    public void InputTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            string input = commandBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                outputBox.SelectionColor = Color.White;
                outputBox.AppendText("> ");
                outputBox.AppendText(Environment.NewLine);
                outputBox.SelectionColor = outputBox.ForeColor;
            }

            if (input.Contains(' '))
            {
                string[] parts = input.Split(' ', 2);

                string command = parts[0];
                string argument = parts[1];

                if (Commands.CommandMap.TryGetValue(command, out Action<string>? commandAction)) commandAction?.Invoke(argument);
                else Helpers.OutputError(outputBox, $"\'{command}\' is not recognised as a valid command.");
            }
            else if (Commands.CommandMap.TryGetValue(input, out Action<string>? command)) command?.Invoke("");
            else if (input != "") Helpers.OutputError(outputBox, $"\'{input}\' is not recognised as a valid command.");

            commandBox.Clear();
            outputBox.ScrollToCaret();
        }
    }
}
