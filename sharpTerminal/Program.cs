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
            };

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
                    Console.WriteLine($"'{input}' is not recognised as a command.\n");
                }
            }
        }
    }
}



