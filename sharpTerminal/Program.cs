using System;

class Program
{
    static void Main(string[] args)
    {
        var commands = new Commands();
        var commandMap = new Dictionary<string, Action>
        {
            { "hello", commands.Hello },
        };

        while (true)
        {
            Console.Write("Command: ");
            string input = Console.ReadLine().ToLower(); 

            if (input == "exit")
            {
                break;
            }

            if (commandMap.ContainsKey(input))
            {
                commandMap[input]();
            }
            else if (input == "")
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

class Commands
{
    public void Hello()
    {
        Console.WriteLine("Hello World!\n");
    }
}
