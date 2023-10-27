using System.Data;
using System.Timers;

namespace sharpTerminal
{
    class Commands
    {
        private Dictionary<string, Action> CommandMap;
        private Dictionary<string, string> CommandDescription;
        private List<string> CommandLogs;

        public void SetCommandMap(Dictionary<string, Action> commandMap, Dictionary<string, string> commandDescription, List<string> commandLogs)
        {
            CommandMap = commandMap;
            CommandDescription = commandDescription;
            CommandLogs = commandLogs;
        }

        public void HelloWorld()
        {
            Console.WriteLine("Hello World!\n");
        }

        public void Exit()
        {
            Program.Active = false;
        }

        public void Help()
        {
            Console.WriteLine("List of Commands:\n");

            foreach (var command in CommandMap.Keys)
            {
                Console.WriteLine($"{command.PadRight(10)} - {CommandDescription[command]}");
            }
            Console.WriteLine();
        }
        public void Clear()
        {
            Console.Clear();
        }

        public void Dt()
        {
            Console.WriteLine($"{DateTime.UtcNow}\n");
        }

        public void Rnd()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101); 
            Console.WriteLine($"{randomNumber}\n");
        }

        public void Eval()
        {
            Console.Write("Enter a mathematical expression: ");
            string? expression = Console.ReadLine();

            if (string.IsNullOrEmpty(expression))
            {
                Console.WriteLine();
                return;
            }

            try
            {
                var result = new DataTable().Compute(expression, null);
                Console.WriteLine($"Result: {result}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}\n");
            }
        }

        public void Say()
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

        public void Beep()
        {
            Console.Beep();
            Console.WriteLine();
        }

        public void Tm()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            Console.WriteLine("Timer started. Press any key to stop...");

            DateTime startTime = DateTime.Now;

            timer.Start();
            Console.ReadKey();
            timer.Stop();

            DateTime stopTime = DateTime.Now;

            Console.WriteLine($"Elapsed time: {stopTime - startTime}\n");
        }
    }
}
