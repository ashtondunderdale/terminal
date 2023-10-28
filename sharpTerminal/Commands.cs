using NCalc;

namespace sharpTerminal
{
    class Commands
    {
        private Dictionary<string, Action> CommandMap = new();
        private Dictionary<string, string> CommandDescription = new();
        private List<string> CommandLogs = new();
        Dictionary<string, string> TerminalSettings = new();

        private const string VERSION = "0.0.1";
        public void CommandConfig(Dictionary<string, Action> commandMap, Dictionary<string, string> commandDescription, List<string> commandLogs, Dictionary<string, string> terminalSettings)
        {
            CommandMap = commandMap;
            CommandDescription = commandDescription;
            CommandLogs = commandLogs;
            TerminalSettings = terminalSettings;
        }

        public static void Hello() => Console.WriteLine($"Hello {Environment.MachineName}!\n");

        public static void Exit() => Program.Active = false;

        public void Help()
        {
            Console.WriteLine("List of Commands:\n");

            foreach (var command in CommandMap.Keys)
            {
                if (CommandDescription.ContainsKey(command))
                {
                    Console.WriteLine($"{command.PadRight(10)} - {CommandDescription[command]}");
                }
                else
                {
                    Console.WriteLine($"{command.PadRight(10)} - No description for this command");
                }
            }
            Console.WriteLine();
        }

        public void Clear()
        {
            if (TerminalSettings.TryGetValue("autoClear", out string? autoClearSetting) && autoClearSetting == "true")

            {
                return;
            }

            Console.Clear(); 
        }

        public static void Dt() => Console.WriteLine($"{DateTime.UtcNow}\n");

        public static void Rnd() => Console.WriteLine(new Random().Next(1, 101));

        public static void Eval()
        {
            Console.Write("Enter an expression: ");
            string? expression = Console.ReadLine();

            if (string.IsNullOrEmpty(expression))
            {
                Console.WriteLine("Expression is empty.");
                return;
            }

            try
            {
                Expression e = new Expression(expression);

                e.Parameters["true"] = true;
                e.Parameters["false"] = false;

                object result = e.Evaluate();

                if (result is bool)
                {
                    Console.WriteLine($"Result: {result}\n");
                }
                else if (result is double)
                {
                    Console.WriteLine($"Result: {(double)result}\n");
                }
                else
                {
                    Console.WriteLine($"Result: {result}\n");
                }
            }
            catch (EvaluationException ex)
            {
                Console.WriteLine($"Result: {ex.Message}\n");
            }
        }

        public static void Say()
        {
            Console.Write("Enter a phrase to repeat: ");

            string? phrase = Console.ReadLine();
            if (!string.IsNullOrEmpty(phrase)) Console.WriteLine($"You said: {phrase}\n");
        }

        public void Hist()
        {
            if (CommandLogs.Count == 0)
            {
                Console.WriteLine("Command History is empty\n");
                return;
            }

            int countToDisplay = Math.Min(10, CommandLogs.Count);
            int startIndex = CommandLogs.Count - countToDisplay;

            for (int i = startIndex; i < CommandLogs.Count; i++)
            {
                Console.WriteLine(CommandLogs[i]);
            }
            Console.WriteLine();
        }

        public static void Beep() { Console.Beep(); Console.WriteLine(); }

        public static void Tm()
        {
            Console.WriteLine("Timer started. Press any key to stop...");
            Console.ReadKey();

            TimeSpan elapsed = DateTime.Now - DateTime.Now;

            Console.WriteLine($"Elapsed time: {elapsed}\n");
        }

        public static void Ver() => Console.WriteLine($"sharpTerminal Version>{VERSION}\n");

        public void Set()
        {
            Console.Clear();
            Console.WriteLine($"Settings>\n\n  AutoClear> {TerminalSettings["autoClear"]}\n");

            while (true)
            {
                Console.Write("Command>");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                if (input == "ac true")
                {
                    TerminalSettings["autoClear"] = "true";
                    Console.WriteLine("AutoClear set to true\n");
                }
                else if (input == "ac false")
                {
                    TerminalSettings["autoClear"] = "false";
                    Console.WriteLine("AutoClear set to false\n");
                }
                else if (input == "exit")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine($"'{input}' is not recognized as a command.\n");
                }
            }
        }

        public void ReadClear()
        {
            Console.ReadKey();
            Console.Clear();
        }
    }
}
