using NCalc;

namespace sharpTerminal
{
    class Commands
    {
        private Dictionary<string, Action> CommandMap = new();
        private Dictionary<string, string> CommandDescription = new();
        private List<string> CommandLogs = new();

        private const string VERSION = "0.0.1";
        public void CommandConfig(Dictionary<string, Action> commandMap, Dictionary<string, string> commandDescription, List<string> commandLogs)
        {
            CommandMap = commandMap;
            CommandDescription = commandDescription;
            CommandLogs = commandLogs;
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
        public static void Clear() => Console.Clear();

        public static void Dt() => Console.WriteLine($"{DateTime.UtcNow}\n");

        public static void Rnd()
        {
            int randomNumber = new Random().Next(1, 101);
            Console.WriteLine($"{randomNumber}\n");
        }

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
                    Console.WriteLine("Result: " + result);
                }
                else if (result is double)
                {
                    Console.WriteLine("Result (as double): " + (double)result);
                }
                else
                {
                    Console.WriteLine("Result: " + result);
                }
            }
            catch (EvaluationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        public static void Say()
        {
            Console.Write("Enter a phrase to repeat: ");
            string? phrase = Console.ReadLine();

            if (string.IsNullOrEmpty(phrase))
            {
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"You said: {phrase}\n");
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

            DateTime startTime = DateTime.Now;
            Console.ReadKey();
            DateTime stopTime = DateTime.Now;

            TimeSpan elapsed = stopTime - startTime;
            Console.WriteLine($"Elapsed time: {elapsed}\n");
        }

        public void Ver() => Console.WriteLine($"sharpTerminal Version -- >{VERSION}");
    }
}
