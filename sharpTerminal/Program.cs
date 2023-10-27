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
                { "time", commands.Time },
            };

            var commandDescriptions = new Dictionary<string, string>
            {
                { "hello", "Greets the user" },
                { "exit", "Exits the program" },
                { "help", "Displays a list of available commands" },
                { "clear", "Clears the console" },
                { "time", "Displays the current time (UTC)" },
            };

            commands.SetCommandMap(commandMap, commandDescriptions);

            while (Active)
            {
                Console.Write("Command>");
                string input = Console.ReadLine().ToLower();

                if (commandMap.ContainsKey(input))
                {
                    commandMap[input]();
                }
                else if (string.IsNullOrEmpty(input))
                {
                    continue;
                }
                else
                {
                    Console.WriteLine($"'{input}' is not recognized as a command.\n");
                }
            }
        }
    }
}


