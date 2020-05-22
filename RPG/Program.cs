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
            Console.Clear();
            Main(new string[] { });
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
            Console.Clear();

        }

        public class weapon
        {
            public string name;
            public int damage;
            public int maxDurability;
            public int brokenDamage;
            public int durability;
            public weapon(string Name, int Damage, int BrokenDamage, int MaxDurability)
            {
                name = Name;
                damage = Damage;
                brokenDamage = BrokenDamage;
                maxDurability = MaxDurability;
            }

            public int attack()
            {
                if (durability > 0) 
                {
                    durability -= 1;
                    return damage;
                } else if (durability == 0)
                {
                    return brokenDamage;
                } else
                {
                    return damage;
                }
            }
        }

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
            while (expected.Length > f && input.Length > f)
            {
                if (input[input.Length - f-1] != expected[expected.Length - f-1])
                {
                    break;
                }
                ++f;
            }
            return f + i; 
        }
        public static int choice(string[] options)
        {
            for (int i = 0; i < options.Length; ++i)
            {
                Console.WriteLine(Convert.ToString(i+1)+". "+options[i]);
            }
            string input = Console.ReadLine();
            if (int.TryParse(input, out _))
            {
                return Convert.ToInt32(input);
            } 
            int lowest = -1;
            int highestScore = 0;
            for (int i = 0; i < options.Length; ++i)
            {
                if (hamming(input, options[i])>highestScore)
                {
                    highestScore = hamming(input.ToLower(), options[i].ToLower());
                    lowest = i;
                }
            }
            return lowest+1; //error if lowest == -1
        }
        static void Main(string[] args)
        {
            Console.WriteAscii("RPG");
            int selected = choice(new string[] { "Start", "Options", "Exit" });
            if (selected == 1)
            { // start
                start();
            } else if (selected == 2)
            { // options
                Options();
            }
            Console.WriteLine("Exit");
            Thread.Sleep(1000);
        }
    }
}