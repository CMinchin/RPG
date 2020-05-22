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
            //start();
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

        /*static void start()
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
        }*/
        public static int hamming(string input, string expected)
        {
            int i = 0;
            while (i < input.Length)
            {
                if (input[i] != expected[i])
                {
                    break;
                }
                ++i;
            }
            int f = 0;
            while (f - expected.Length > 0 && f - input.Length > 0)
            {
                if (input[f - input.Length] != expected[f - expected.Length])
                {
                    break;
                }
                ++f;
            }
            
            return f + i; 
        }
        public static int option(string[] options)
        {
            for (int i = 0; i < options.Length; ++i)
            {
                Console.WriteLine(Convert.ToString(i)+". "+options[i]);
            }
            string input = Console.ReadLine();
            
            int lowest = -1;
            int highestScore = 0;
            for (int i = 0; i < options.Length; ++i)
            {
                if (hamming(input, options[i])>highestScore)
                {
                    highestScore = hamming(input, options[i]);
                    lowest = i;
                }
            }

            return lowest; //error if lowest == -1
        }
        static void Main(string[] args)
        {
            
        }
    }
}