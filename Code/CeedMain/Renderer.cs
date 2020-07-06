using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CeedMain
{
    class Renderer
    {

        public static int GameStartMenu(bool gameContinuePressed)
        {


            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("CeeD");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Game Rules: The point of the game is to explore as many Solar Systems as possible. Each system has");
            Console.WriteLine("a random amount of Fuel and Resources. Fuel gives you fuel, and Resources give you Scanners, which");
            Console.WriteLine("is an ability to randomly scout out planets, and find out of their details.");
            Console.WriteLine("When you start a new game, youll see that you dont know any of the planets, and thats becaue you");
            Console.WriteLine("Havent discovered their location yet, you just see they exist. You use the scanner to find where they are,");
            Console.WriteLine("and if they are worth exploring. Exploring means going to the planet and taking its Resources and Fuel, but");
            Console.WriteLine("be warned, some planets have intelligent life, and can be Hazardous. If you explore a dangerous planet, youll");
            Console.WriteLine("take damage, try to not die! If you run out of scanners, you can use Wander, but that consumes fuel, which is limited");
            Console.WriteLine("too. Every fuel reserve gives you between 1-5 fuel.");
            Console.WriteLine("When referencing Planets, Index refers to the number inside the square brackets, for example:");
            Console.WriteLine("RANDOMNAME[1] <- in that case index is 1");
            Console.WriteLine("When using actions, press the index number after it, in the case of Explore, type numbers after it n press enter to submit.");
            Console.WriteLine("Minigame Controls:");
            Console.WriteLine("Movement: WASD");
            Console.WriteLine("Shoot: Spacebar");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Try not to die, Good luck ;)");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Start new game [SPACE]");
            Console.WriteLine();
            Console.WriteLine();
            if (gameContinuePressed == true)
            {
                Console.WriteLine("CURRENTLY UNSUPPORTED!");
            }
            else
            {
                Console.WriteLine("Continue game [L]");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Close game at any time: CTRL + C");
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.Spacebar)
            {
                return (1);
            }
            if (input.Key == ConsoleKey.L)
            {
                return (2);
            }
            if (input.Key == ConsoleKey.Escape)
            {
                return (3);
            }
            else
            {
                return (0);
            }
        }

        /*public static int SystemScreen(int scannerAmount, int fuelAmount, string systemName)
        {

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Welcome to System " + systemName);
            Console.WriteLine();
            Console.WriteLine();
            if (scannerAmount > 0)
            {
                Console.WriteLine("Scan System(-1 Scanner{Currently have: " + scannerAmount + ") [1]");
            }
            Console.WriteLine();
            if (fuelAmount > 2)
            {
                Console.WriteLine("Explore System(-2 Fuel{Currently have: " + fuelAmount + ") [2]");
            }
            Console.WriteLine();
            Console.WriteLine("Strand [3]");
            Console.WriteLine();
            Console.WriteLine("RageQuit [ESC]");

            while (true)
            {
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.NumPad1)
                {
                    return (1);
                }
                if (input.Key == ConsoleKey.NumPad2)
                {
                    return (2);
                }
                if (input.Key == ConsoleKey.NumPad3)
                {
                    return (3);
                }
                if (input.Key == ConsoleKey.Escape)
                {
                    return (4);

                }
                else
                {

                }
            }
        }*/ //DEAD CODE

        public static void DebugScan(SolarSystem newSolarSytem, List<Planet> planetList)
        {

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Scan results of the system " + newSolarSytem.name + ":");
            string hostilityConverted = "";

            switch (newSolarSytem.hostility) //Draw Hostility
            {
                case 0:
                    hostilityConverted = "Peaceful";
                    break;
                case 1:
                    hostilityConverted = "Passive";
                    break;
                case 2:
                    hostilityConverted = "Cautious";
                    break;
                case 3:
                    hostilityConverted = "Scary";
                    break;
                case 4:
                    hostilityConverted = "Dangerous";
                    break;
                case 5:
                    hostilityConverted = "Deadly";
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("System is " + hostilityConverted);


            if (newSolarSytem.hasAsteroids == true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("System contains Asteroid fields");
            }

            if (newSolarSytem.hasAsteroids == true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("System has electric field disturbences present");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("There are " + newSolarSytem.planetAmount + " planets:");

            Console.WriteLine();
            Console.WriteLine("-name | hostility | Resources | Fuel | Occupied");

            foreach (Planet a in planetList)
            {
                Console.WriteLine();
                Console.Write("- " + a.name);
                Console.Write("| " + a.hostility);
                Console.Write(" | " + a.areResources);
                Console.Write(" | " + a.isFuel);
                Console.Write(" | " + a.occupiedByLiving);
                a.isDiscovered = true;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("press any key to continue..");
            Console.WriteLine();
            Console.ReadKey();
        }

        public static int ChooseAction(SolarSystem newSolarSystem, List<Planet> planetList, PlayerShip playerShip)
        {

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Welcome to System " + newSolarSystem.name + ", What would you like to do?");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Your ship data:");
            Console.WriteLine();
            Console.WriteLine("-Systems Explored: " + playerShip.exploredSystemsAmount);
            Console.WriteLine("-Fuel: " + playerShip.fuelAmount);
            Console.WriteLine("-Health: " + playerShip.shipHealth);
            Console.WriteLine("-Money: " + playerShip.Money + "đ");
            Console.WriteLine("-Scanner Amount: " + playerShip.scannerAmount);
            Console.WriteLine("-Total ships destroyed: " + playerShip.totalKilledPirateAmnt);
            Console.WriteLine("-total asteroids destroyed: " + playerShip.totalDestroyedAsteroids);
            Console.WriteLine();
            Console.WriteLine();
            if (newSolarSystem.hasAsteroids)
            {
                Console.WriteLine("Solar System has asteroid belts, be wary!");
            }

            Console.WriteLine();

            if (newSolarSystem.shop)
            {
                Console.WriteLine("Enter Shop[B/5]");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("There are " + newSolarSystem.planetAmount + " planets:");
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < newSolarSystem.planetAmount; i++)
            {
                if (planetList[i].isDiscovered == true)
                {
                    string consoleOutput = "-" + planetList[i].name + "[" + i + "]";

                    if (planetList[i].hostility > 0)
                    {
                        consoleOutput += " |hostility:" + planetList[i].hostility;
                    }

                    if (planetList[i].isFuel)
                    {
                        consoleOutput += " |Fuel:" + planetList[i].isFuel;
                    }

                    if (planetList[i].areResources)
                    {
                        consoleOutput += " |Resources:" + planetList[i].areResources;
                    }
                    if (planetList[i].isExplored)
                    {
                        consoleOutput += "! HAS BEEN EXPLORED !";
                    }
                    Console.WriteLine(consoleOutput);
                }
                else
                {
                    Console.WriteLine("-" + "???");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Explore[E/1]");
            Console.WriteLine();
            Console.WriteLine("Scan[S/2]");
            Console.WriteLine();
            Console.WriteLine("Wander[W/3]");
            Console.WriteLine();
            Console.WriteLine("Move to next System(uses 10 fuel)[4,Q]");
            Console.WriteLine();
            Console.WriteLine("Ragequit[ESC]");

            while (true) //WAIT FOR INPUT
            {
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.NumPad1 || input.Key == ConsoleKey.E)
                {
                    return 1;
                }
                else if (input.Key == ConsoleKey.NumPad2 || input.Key == ConsoleKey.S)
                {
                    return (2);
                }
                else if (input.Key == ConsoleKey.NumPad3 || input.Key == ConsoleKey.W)
                {
                    return (3);
                }
                else if (input.Key == ConsoleKey.NumPad4 || input.Key == ConsoleKey.Q)
                {
                    return (4);
                }

                else if (input.Key == ConsoleKey.NumPad5 || input.Key == ConsoleKey.B)
                {
                    return (5);
                }

                else if (input.Key == ConsoleKey.Escape)
                {
                    return (6);
                }

            }

        }

        public static PlayerShip Shop(PlayerShip playerShip)
        {

            while (true)
            {

                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Welcome to the shop!");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Your ship data:");
                Console.WriteLine();
                Console.WriteLine("-Systems Explored: " + playerShip.exploredSystemsAmount);
                Console.WriteLine("-Fuel: " + playerShip.fuelAmount);
                Console.WriteLine("-Health: " + playerShip.shipHealth);
                Console.WriteLine("-Money: " + playerShip.Money + "đ");
                Console.WriteLine("-Scanner Amount: " + playerShip.scannerAmount);
                Console.WriteLine("-Total ships destroyed: " + playerShip.totalKilledPirateAmnt);
                Console.WriteLine("-total asteroids destroyed: " + playerShip.totalDestroyedAsteroids);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Fix ship(+1hp, -5đ)[1]");
                Console.WriteLine();
                Console.WriteLine("Buy 1 Scanner(10đ)[2]");
                Console.WriteLine();
                Console.WriteLine("Buy 1 Fuel(2đ)[3]");
                Console.WriteLine();
                Console.WriteLine("Exit[ESC]");

                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.NumPad1)
                {
                    if (playerShip.Money >= 5)
                    {
                        if (playerShip.shipHealth <= 20)
                        {
                            playerShip.Money -= 5;
                            playerShip.shipHealth += 1;
                        }
                        else
                        {
                            Console.WriteLine("Youre at full health/out of money!");
                            Console.ReadKey();
                        }
                    }
                }
                else if (input.Key == ConsoleKey.NumPad2)
                {
                    if (playerShip.Money >= 10)
                    {
                        playerShip.Money -= 10;
                        playerShip.scannerAmount += 1;
                    }
                    else
                    {
                        Console.WriteLine("Youre out of money!");
                        Console.ReadKey();
                    }
                }
                else if (input.Key == ConsoleKey.NumPad3 || input.Key == ConsoleKey.W)
                {
                    if (playerShip.Money >= 2)
                    {
                        playerShip.Money -= 2;
                        playerShip.fuelAmount += 1;
                    }
                    else
                    {
                        Console.WriteLine("Youre out of money!");
                        Console.ReadKey();
                    }
                }

                else if (input.Key == ConsoleKey.Escape)
                {

                    return playerShip;
                }


            }



        }
    }

}
