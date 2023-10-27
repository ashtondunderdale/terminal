using System;


namespace sharpTerminal
{
    class Program
    {
        public static bool Active = true;

        static void Main(string[] args)
        {
            var commands = new Commands();
            var commandMap = new Dictionary<string, Action>
            {
                { "hello", commands.HelloWorld },
                { "exit", commands.Exit },
                { "help", commands.Help },
                { "clear", commands.Clear },
                { "dt", commands.Dt },
                { "rnd", commands.Rnd },
                { "eval", commands.Eval },
                { "say", commands.Say },
                { "hist", commands.Hist },
            };

            var commandDescriptions = new Dictionary<string, string>
            {
                { "hello", "Greets the user" },
                { "exit", "Exits the program" },
                { "help", "Displays a list of available commands" },
                { "clear", "Clears the console" },
                { "dt", "Displays the current time (UTC)" },
                { "rnd", "Generates a random number (1 - 100)" },
                { "eval", "Evaluates a given mathematical expression" },             // more complex help commands such as "help dt"
                { "say", "Repeats a given phrase" },
                { "hist", "Displays the last 10 used commands" },
            };

            List<string> commandLogs = new List<string>();

            commands.SetCommandMap(commandMap, commandDescriptions, commandLogs);

            while (Active)
            {
                Console.Write("Command>");
                string? input = Console.ReadLine()?.ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }
                else if (commandMap.ContainsKey(input))
                {
                    commandMap[input]();
                    commandLogs.Add(input); 
                }
                else
                {
                    Console.WriteLine($"'{input}' is not recognized as a command.\n");
                }
            }
        }
    }
}


