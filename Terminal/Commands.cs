﻿namespace Terminal
{
    public static class Commands
    {
        private static string _currentPath = @"C:\Users\adunderdale";

        public static string CurrentPath
        {
            get => _currentPath;
            set => _currentPath = value;
        }

        public static readonly Dictionary<string, Action<string>> CommandMap = new()
        {
            { "hello", (_) => Hello() },
            { "help", (_) => Help() },
            { "clear", (_) => Clear() },
            { "dir", (arg) => Dir() },
            { "ls", (arg) => Ls() },
            { "en", (arg) => En(arg) },
            { "re", (arg) => Re() },
        };

        public static readonly Dictionary<string, string> CommandDescriptions = new()
        {
            { "HELLO", "Greets the user" },
            { "HELP", "Lists a description for each command" },
            { "CLEAR", "Clears the terminal of all text" },
            { "DIR", "Displays the current directory" },
            { "LS", "lists all files and folders in current directory" },
            { "EN", "Enters a directory from a given argument" },
            { "RE", "retreats out one directory level" },
            // CRT filename
            // DEL filename + confirmation
        };

        public static void Hello() => Helpers.OutputInformation(Terminal.outputBox, "Hello, Ashton!\n");

        public static void Help()
        {
            Helpers.OutputInformation(Terminal.outputBox, "List of commands: \n");

            foreach (var kvp in CommandDescriptions)
            {
                string command = kvp.Key;
                string description = kvp.Value;

                Terminal.outputBox.AppendText($"- {command,-10} {description}\n");
            }
            Terminal.outputBox.AppendText("\n");
        }

        public static void Clear() 
        { 
            Terminal.outputBox.Clear();
            Terminal.outputBox.AppendText("Sharp terminal [Version 0.0.1]\n\n");
        }

        public static void Dir() => Helpers.OutputInformation(Terminal.outputBox, $"Current path: {_currentPath}\n");

        public static void Ls()
        {
            string[] files = Directory.GetFileSystemEntries(_currentPath);
            int pathLength = _currentPath.Length;
            Helpers.OutputSuccess(Terminal.outputBox, $"All files from: {_currentPath}\n");

            foreach (string file in files)
            {
                if (!IsExcludedEntry(file))
                {
                    DateTime timeStamp = File.GetLastWriteTime(file);
                    string subFile = file.Substring(pathLength + 1);
                    Helpers.OutputInformation(Terminal.outputBox, $"{timeStamp}  {subFile}");
                }
            }
            Terminal.outputBox.AppendText("\n");
        }

        private static bool IsExcludedEntry(string entry)
        {
            string[] excludedEntries = { "NTUSER.DAT", "TM.blf", "regtrans-ms", "ntuser" };

            foreach (string excludedEntry in excludedEntries) if (entry.Contains(excludedEntry)) return true;
            return false;
        }

        public static void En(string args)
        {
            string fullPath = Path.Combine(_currentPath, args);

            if (Directory.Exists(fullPath))
            {
                string[] files = Directory.GetFiles(fullPath);
                string[] directories = Directory.GetDirectories(fullPath);

                if (args == "") Helpers.OutputError(Terminal.outputBox, $"No files or folders found at the provided path.\n");
                else if (files.Length > 0 || directories.Length > 0) Helpers.OutputSuccess(Terminal.outputBox, $"Entered: {fullPath}\n");
                else Helpers.OutputError(Terminal.outputBox, "No files or folders found at the provided path.\n");
            }
            else Helpers.OutputError(Terminal.outputBox, "The provided path does not exist.\n");

            Terminal.outputBox.AppendText("\n");
            _currentPath = fullPath;
        }

        public static void Re()
        {
            string parentPath = Directory.GetParent(_currentPath)?.FullName;

            if (parentPath != null)
            {
                _currentPath = parentPath;
                Helpers.OutputSuccess(Terminal.outputBox, $"Navigated back to: {_currentPath}\n");
            }
            else
            {
                Helpers.OutputError(Terminal.outputBox, "Already at the root directory. Cannot go back.\n");
            }

            Terminal.outputBox.AppendText("\n");
        }
    }
}
