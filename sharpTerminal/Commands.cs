using System;


namespace sharpTerminal
{
    class Commands
    {
        public void HelloWorld()
        {
            Console.WriteLine("Hello World!\n");
        }

        public void Exit()
        {
            Program.Active = false;
        }
    }
}
