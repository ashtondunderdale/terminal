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

        public void Time()
        {
            Console.WriteLine(DateTime.UtcNow);
        }
    }
}




