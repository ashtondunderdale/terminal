using System.Data;

namespace sharpTerminal
{
    class Commands
    {
        private Dictionary<string, Action> CommandMap;
        private Dictionary<string, string> CommandDescription;


        public void SetCommandMap(Dictionary<string, Action> commandMap, Dictionary<string, string> commandDescription)
        {
            CommandMap = commandMap;
            CommandDescription = commandDescription;
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
            string expression = Console.ReadLine();

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
            string phrase = Console.ReadLine();
            Console.WriteLine($"You said: {phrase}\n");
        }
    }
}




