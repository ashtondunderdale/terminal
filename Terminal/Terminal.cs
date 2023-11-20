using System;
using System.Drawing;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Terminal : Form
    {
        public Terminal()
        {
            InitializeComponent();
            commandBox.KeyDown += InputTextBox_KeyDown;

            outputBox.ReadOnly = true;
            outputBox.AppendText("Sharp terminal [Version 0.0.1]\n\n");
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string input = commandBox.Text.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    outputBox.SelectionColor = Color.Gold;
                    outputBox.AppendText("> ");
                    outputBox.AppendText(Environment.NewLine);
                    outputBox.SelectionColor = outputBox.ForeColor;
                }

                var commandMap = new Dictionary<string, Action>
                {
                    { "hello", Commands.Hello },
                };


                if (commandMap.TryGetValue(input, out Action? command)) command();
                else if (input != "") Helpers.OutputError(outputBox, $"{input} is not recognised as a valid command.");

                commandBox.Clear();
                outputBox.ScrollToCaret();
            }
        }
    }
}
