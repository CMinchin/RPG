using System;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace RPG
{
    class Program
    {
        static void Options()
        {
            Print("There are no options.", "");
            Console.Write("\r                     \r");
            start();
        }
        static void quit()
        {
            Environment.Exit(1);
        }
        static void Print(string text, string end = "\n", int interval = 100)
        {
            foreach (char character in text)
            {
                Console.Write(character);
                Thread.Sleep(interval);
            }
            Console.Write(end);
        }

        static void start()
        {
            Console.Write("\r");
            string x = Console.ReadLine();
            switch (x.ToLower())
            {
                case "1":
                    break;
                case "start":
                    break;
                case "2":
                    Options();
                    break;
                case "options":
                    Options();
                    break;
                case "3":
                    quit();
                    break;
                case "exit":
                    quit();
                    break;
                default:
                    Print("Learn to spell\n");
                    start();
                    break;
            }
        }

        static void Main(string[] args)
        {

        }
    }
}