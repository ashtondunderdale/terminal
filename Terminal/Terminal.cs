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
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string command = commandBox.Text.Trim();

                if (string.IsNullOrEmpty(command))
                {
                    outputBox.SelectionColor = Color.Gold;
                    outputBox.AppendText("> ");
                    outputBox.AppendText(Environment.NewLine);
                    outputBox.SelectionColor = outputBox.ForeColor;
                }

/*                if (command is not "")
                {
                    outputBox.SelectionColor = Color.Gold;
                    outputBox.AppendText(">");
                    outputBox.SelectionColor = outputBox.ForeColor;
                    outputBox.AppendText($" {command}\n");
                }*/

                if (string.Equals(command, "hello", StringComparison.OrdinalIgnoreCase))
                {
                    OutputInformation("Hello User!");
                }
                else if (!string.IsNullOrEmpty(command))
                {
                    OutputError($"\'{command}\' is not recognised as a command");
                }
                commandBox.Clear();
                outputBox.ScrollToCaret();
            }
        }

        private void OutputSuccess(string message)
        {
            string successSymbol = "+";

            outputBox.SelectionColor = Color.Green;
            outputBox.AppendText($"{successSymbol} ");
            outputBox.SelectionColor = outputBox.ForeColor;
            outputBox.AppendText($"{message}" + Environment.NewLine + "\n");
        }

        private void OutputError(string message)
        {
            string errorSymbol = "-";

            outputBox.SelectionColor = Color.Red;
            outputBox.AppendText($"{errorSymbol} ");
            outputBox.SelectionColor = outputBox.ForeColor;
            outputBox.AppendText($"{message}" + Environment.NewLine + "\n");
        }

        private void OutputInformation(string message)
        {
            string informationSymbol = "i";

            outputBox.SelectionColor = Color.CornflowerBlue;
            outputBox.AppendText($"{informationSymbol} ");
            outputBox.SelectionColor = outputBox.ForeColor;
            outputBox.AppendText($"{message}" + Environment.NewLine + "\n");
        }
    }
}
