namespace Terminal
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
            { "dir", (arg) => Dir() },
            { "lsdir", (arg) => Lsdir() },
            { "endir", (arg) => Endir(arg) },
        };

        public static readonly Dictionary<string, string> CommandDescriptions = new()
        {
            { "hello", "hello: Greets the user" },
            { "help", "Lists a description for each command" },
        };

        public static void Hello() => Helpers.OutputInformation(Terminal.outputBox, "Hello, Ashton!\n");

        public static void Help()
        {
            Helpers.OutputInformation(Terminal.outputBox, "List of commands: ");

            foreach (var kvp in CommandDescriptions)
            {
                string command = kvp.Key;
                string description = kvp.Value;

                Terminal.outputBox.AppendText($"- {command}: {description}\n");
            }
            Terminal.outputBox.AppendText("\n");
        }

        public static void Dir()
        {
            Helpers.OutputInformation(Terminal.outputBox, $"Current path: {_currentPath}\n");
        }

        public static void Lsdir()
        {
            string[] files = Directory.GetFileSystemEntries(_currentPath);
            int pathLength = _currentPath.Length;
            Helpers.OutputSuccess(Terminal.outputBox, $"All files from: {_currentPath}\n");

            foreach (string file in files)
            {
                DateTime timeStamp = File.GetLastWriteTime(file);
                string subFile = file.Substring(pathLength + 1);
                Helpers.OutputInformation(Terminal.outputBox, $"{timeStamp}  {subFile}");
            }
            Terminal.outputBox.AppendText("\n");
        }

        public static void Endir(string args)
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
    }
}
