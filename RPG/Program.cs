using System;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.Collections.Generic;

namespace RPG
{
    class Program
    {
        public static List<Object> inventory = new List<Object> { };
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

        public class potion
        {
            public string name;
            public int healingEffect;
            public potion(string Name, int HealingEffect)
            {
                name = Name;
                healingEffect = HealingEffect;
            }
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

        public static int useItem()
        {
            // display the inventory
            List<string> inventoryNames = new List<string>();
            foreach(Object item in inventory)
            {
                if (item is potion)
                {
                    inventoryNames.Add(((potion)item).name);
                } 
                else if (item is weapon)
                {
                    inventoryNames.Add(((weapon)item).name);
                }
                else
                {
                    throw new NotImplementedException(); // there is something in my inventory that there should not be
                }
            }

            inventoryNames.Add("Back");// put in a back feature

            choice(inventoryNames.ToArray());

            return 1;
        }

        public static int combat(int enemyMaxHealth, ref weapon enemyWeapon, int playerMaxHealth, ref int playerHealth, ref weapon playerWeapon)
        {
            bool run = false;
            int enemyHealth = enemyMaxHealth;
            while (enemyHealth > 0 && run != true)
            {// your turn
                Console.WriteLine("Your health: {0}\nEnemy Health: {1}\nCurrent Weapon: {3}",playerHealth,enemyHealth,playerWeapon.name);
                bool skip = false;
                while (!skip) { // if you need to exit out of one of the menus
                    skip = false;
                    int selection = choice(new string[] {"Attack","Use Item or Switch Weapon","Run"});
                    switch (selection)
                    {
                        case 1:
                            enemyHealth -= playerWeapon.attack();
                            break;
                        case 2:
                            int item = useItem();
                            if (item == 0 || item == inventory.Count + 1) // either nothing was entered or back was selected
                            {
                                skip = true;
                            }
                            else if (inventory[item-1] is potion)
                            {
                                playerHealth = Math.Min(playerHealth + ((potion)inventory[item - 1]).healingEffect, playerMaxHealth); // heal the player to at most their max health
                            }
                            break;
                        case 3:
                            run = new Random().Next(0, 1) == 1;
                            if (run)
                            {
                                Console.WriteLine("You successfully ran");
                            }
                            else
                            {
                                Console.WriteLine("You couldn't escape");
                            }
                            break;
                        default:
                            Console.WriteLine("You Flinched!");
                            break;
                    }
                }
                // enemy turn
                int enemyDamage = enemyWeapon.attack();
                playerHealth -= enemyDamage;
                Print("the enemy hit you for " + Convert.ToString(enemyDamage) + " and you are now on " + Convert.ToString(playerHealth) + " HP");
                Console.Clear();
            }
            return -1;
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