using NCalc;
using System.Management;

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
                Console.Clear();
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
            if (TerminalSettings.TryGetValue("th", out string? trackHist) && trackHist == "true")
            {
                if (CommandLogs.Count == 0)
                {
                    Console.WriteLine("Command History is empty\n");
                    return;
                }
                else
                {
                    int countToDisplay = Math.Min(10, CommandLogs.Count);
                    int startIndex = CommandLogs.Count - countToDisplay;

                    for (int i = startIndex; i < CommandLogs.Count; i++)
                    {
                        Console.WriteLine(CommandLogs[i]);
                    }
                }
            }
            else
            {
                CommandLogs.Clear();
                Console.WriteLine("History is not being tracked");
            }

            Console.WriteLine();
        }

        public static void Beep() { Console.Beep(); Console.WriteLine(); }

        public static void Tm()
        {
            DateTime startTime = DateTime.Now;

            Console.WriteLine("Timer started. Press any key to stop...");
            Console.ReadKey();

            DateTime stopTime = DateTime.Now;
            TimeSpan elapsed = stopTime - startTime;

            Console.WriteLine($"Elapsed time: {elapsed}\n");
        }


        public static void Ver() => Console.WriteLine($"sharpTerminal Version>{VERSION}\n");

        public void Set()
        {
            Console.Clear();
            Console.WriteLine($"Settings>\n\n  " +
                $"AutoClear>           {TerminalSettings["ac"]}\n  " +
                $"TrackHistory>        {TerminalSettings["th"]}\n  " +
                $"TextColour>          {TerminalSettings["tc"]}\n  " +
                $"BackgroundColour>    {TerminalSettings["bc"]}\n");

            while (true)
            {
                Console.Write("Command>");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                if (input == "ac true" || input == "ac false" || input == "th true" || input == "th false")
                {
                    string settingKey = input.Substring(0, 2);
                    string settingValue = input.Substring(3);

                    TerminalSettings[settingKey] = settingValue;

                    Console.WriteLine($"{settingKey} set to {settingValue}");

                    if (TerminalSettings.TryGetValue("ac", out string? autoClear) && autoClear == "true")
                    {
                        return;
                    }
                    ReadClear();

                    return;
                }
                else if (input.StartsWith("bc "))
                {
                    string color = input.Substring(3); 
                    if (IsConsoleColorValid(color))
                    {
                        TerminalSettings["bc"] = color;
                        Console.BackgroundColor = GetConsoleColorFromString(color);
                        Console.Clear();
                        Console.ForegroundColor = GetConsoleColorFromString(TerminalSettings["tc"]);
                        Console.WriteLine($"Background color set to {color}");

                        if (TerminalSettings.TryGetValue("ac", out string? autoClear) && autoClear == "true")
                        {
                            return;
                        }
                        ReadClear();

                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid background color: {color}.");
                    }
                }
                else if (input.StartsWith("tc "))
                {
                    string color = input.Substring(3);
                    if (IsConsoleColorValid(color))
                    {
                        TerminalSettings["tc"] = color;
                        Console.ForegroundColor = GetConsoleColorFromString(color);
                        Console.WriteLine($"Text colour set to {color}");
                        ReadClear();
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid text colour: {color}.");
                    }
                }
                else if (TerminalSettings.TryGetValue("ac", out string? autoClear) && autoClear == "true" && input == "exit")
                {
                    Console.Write("Press Enter>");

                    return;
                }
                else if (TerminalSettings.TryGetValue("ac", out autoClear) && autoClear == "false" && input == "exit")
                {
                    Console.Write("Press Enter>");
                    ReadClear();
                    return;
                }
                else
                {
                    Console.WriteLine($"'{input}' is not recognized as a command.\n");
                }
            }
        }

        private bool IsConsoleColorValid(string color)
        {
            try
            {
                Enum.Parse<ConsoleColor>(color, true); 
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ConsoleColor GetConsoleColorFromString(string color) => Enum.Parse<ConsoleColor>(color, true); 

        public static void Sys_os()
        {
            Console.WriteLine("Operating System Information:\n");

            try
            {
                ObjectQuery osQuery = new("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher osSearcher = new ManagementObjectSearcher(osQuery);
                ManagementObjectCollection osCollection = osSearcher.Get();

                foreach (ManagementObject osInfo in osCollection)
                {
                    Console.WriteLine("Operating System: " + osInfo["Caption"] + " " + osInfo["Version"]);
                    Console.WriteLine("System Type: " + osInfo["OSArchitecture"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
        }


        public static void Sys_cpu()
        {
            Console.WriteLine("CPU Information:\n");

            try
            {
                ObjectQuery cpuQuery = new ObjectQuery("SELECT * FROM Win32_Processor");
                ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher(cpuQuery);
                ManagementObjectCollection cpuCollection = cpuSearcher.Get();

                foreach (ManagementObject cpuInfo in cpuCollection)
                {
                    Console.WriteLine("CPU Manufacturer: " + cpuInfo["Manufacturer"]);
                    Console.WriteLine("Number of Processors: " + cpuInfo["NumberOfCores"]);
                    Console.WriteLine("Processor Speed: " + cpuInfo["MaxClockSpeed"] + " MHz");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
        }

        public static void Sys_mem()
        {
            Console.WriteLine("Memory Information:\n");

            try
            {
                ObjectQuery memQuery = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
                ManagementObjectSearcher memSearcher = new ManagementObjectSearcher(memQuery);
                ManagementObjectCollection memCollection = memSearcher.Get();

                foreach (ManagementObject memInfo in memCollection)
                {
                    ulong totalPhysicalMemory = Convert.ToUInt64(memInfo["TotalPhysicalMemory"]);
                    Console.WriteLine("Total Physical Memory: " + (totalPhysicalMemory / (1024 * 1024)) + " MB");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
        }

        public void ReadClear()
        {
            Console.ReadKey();
            Console.Clear();
        }
    }
}

