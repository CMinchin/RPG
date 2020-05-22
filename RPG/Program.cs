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
            int maxHealth = 100;
            int health = 100;
            weapon playerWeapon = new weapon("fists", 10, 0, -1);
            inventory.Add(playerWeapon);
            weapon irradiatedClub = new weapon("irradiated club", 1, 0, 5);
            Console.Clear();
            Print("You wake in a world unlike your own.");
            Thread.Sleep(1000);
            Print("It's surfaces ravaged by radiation and disease");
            Thread.Sleep(1000);
            Console.Clear();
            Print("Before you can even take a breath an irradiated barbarian slams his club beside your head with a thump.");
            Thread.Sleep(1000);
            combat(10,ref irradiatedClub,maxHealth,ref health,ref playerWeapon);
            Console.Clear();
            Print("you put the pour creature out of it's missery with one mighty punch. It was only a child.");
            Print("you notice a potion and the club he slammed beside your head laying beside his sad corpse and decide to pick them up.");
            Thread.Sleep(2000);
            Console.Clear();
            Print("you decide to continue on and forget the horrors you have committed in an attempt to keep your tragic life.");
            Thread.Sleep(2000);
            Print("not before long you are met with another barbarian, this one much larger much stronger...");
            Thread.Sleep(2000);
            weapon largerIrradiatedClub = new weapon("irradiated club", 50, 25, 50);
            Print("you remember that you picked up a miysterious potion of the last one as well as it's weapon.");
            Thread.Sleep(1000);
            combat(100, ref largerIrradiatedClub, maxHealth, ref health, ref playerWeapon);
            Print("although defeated, you decide the world would be better off without you.");

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
                durability = maxDurability;
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
            while (enemyHealth > 0 && run != true && playerHealth > 0)
            {// your turn
                Console.Clear();
                Console.WriteLine("Your health: {0}\nEnemy Health: {1}\nCurrent Weapon: {2}",playerHealth,enemyHealth,playerWeapon.name);
                bool skip = false;
                while (!skip) { // if you need to exit out of one of the menus
                    skip = true;
                    int selection = choice(new string[] {"Attack","Use Item or Switch Weapon","Run"});
                    switch (selection)
                    {
                        case 1:
                            int damage = playerWeapon.attack();
                            enemyHealth -= damage;
                            Print("you attack for "+Convert.ToString(damage)+" damage");
                            break;
                        case 2:
                            int item = useItem();
                            if (item == 0 || item == inventory.Count + 1) // either nothing was entered or back was selected
                            {
                                skip = false;
                            }
                            else if (inventory[item-1] is potion)
                            {
                                playerHealth = Math.Min(playerHealth + ((potion)inventory[item - 1]).healingEffect, playerMaxHealth); // heal the player to at most their max health
                            } else
                            {
                                playerWeapon = (weapon)inventory[item-1];
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
                if (enemyHealth <= 0)
                {
                    // enemy turn
                    int enemyDamage = enemyWeapon.attack();
                    playerHealth -= enemyDamage;
                    Print("the enemy hit you for " + Convert.ToString(enemyDamage) + " and you are now on " + Convert.ToString(playerHealth) + " HP");
                    Console.Clear();
                }
            }
            if (playerHealth == 0)
            {
                Print("you have been defeated, your health is 0");
                Thread.Sleep(1000);
            }
            else
            {
                Print("your foe has been defeated");
            }
            Thread.Sleep(1000);
            Console.Clear();
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