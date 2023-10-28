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
                { "hello", Commands.Hello },
                { "exit", Commands.Exit },
                { "help", commands.Help },
                { "clear", commands.Clear },
                { "dt", Commands.Dt },
                { "rnd", Commands.Rnd },
                { "eval", Commands.Eval },
                { "say", Commands.Say },
                { "hist", commands.Hist },
                { "beep", Commands.Beep },
                { "tm", Commands.Tm },
                { "ver", Commands.Ver },
                { "set", commands.Set },
                { "sys os", Commands.Sys_os },
                { "sys cpu", Commands.Sys_cpu },
                { "sys mem", Commands.Sys_mem },

            };

            var commandDescriptions = new Dictionary<string, string>
            {
                { "hello", "Greets the user" },
                { "exit", "Exits the program" },
                { "help", "Displays a list of available commands" },
                { "clear", "Clears the console" },
                { "dt", "Displays the current time (UTC)" },
                { "rnd", "Generates a random number (1 - 100)" },
                { "eval", "Evaluates a given mathematical expression" },            // more complex help commands such as "help dt"
                { "say", "Repeats a given phrase" },                                // verhist
                { "hist", "Displays the last 10 used commands" },
                { "beep", "The terminal will beep" },
                { "ver", "Displays the current version of the terminal" },
                { "set", "Opens the settings menu for the terminal" },
                { "sys os", "Displays info on users os: name, version..." },
                { "sys cpu", "Displays info on users cpu: cores, speed..." },
                { "sys mem", "Displays info on users memory: physical memory..." },
            };

            List<string> commandLogs = new List<string>();

            var terminalSettings = new Dictionary<string, string>
            {
                { "autoClear", "false" }
            };

            commands.CommandConfig(commandMap, commandDescriptions, commandLogs, terminalSettings);

            while (Active)
            {
                Console.Write("Command>");
                string? input = Console.ReadLine()?.ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                else if (commandMap.TryGetValue(input, out Action? command))
                {
                    command();
                    commandLogs.Add($"{DateTime.Now}: {input}");

                    if (terminalSettings.TryGetValue("autoClear", out string? autoClear) && autoClear == "true" && input != "clear" && input != "exit")
                    {
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                else
                {
                    Console.WriteLine($"'{input}' is not recognized as a command.\n");

                    if (terminalSettings.TryGetValue("autoClear", out string? autoClear) && autoClear == "true")
                    {
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }
    }
}


