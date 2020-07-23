using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter;

namespace CeedMain
{
    class Program
    {
        static void Main()
        {
            int gameStartAction = 0;
            //GAMELOOP
            while (true)
            {
                int turnNumber = 0;
                Console.SetWindowSize(124, 50);
                //MAIN MENU
                if (turnNumber == 0)
                {
                    gameStartAction = Renderer.GameStartMenu(false);
                }
                if (gameStartAction == 2)
                {
                    gameStartAction = Renderer.GameStartMenu(true);
                }
                if (gameStartAction == 3)
                {
                    break;
                }
                if (gameStartAction == 1)                            
                {
                    while (true)//GAME STARTS HERE
                    {
                        
                        bool IWANTTOLEAVE = false;
                        //INIT PLAYERSHIP
                        PlayerShip playerShip = new PlayerShip
                        {
                            shipHealth = 20,
                            shipAttackDmg = 1,
                            shipSpeed = 1,
                            scannerAmount = 5,
                            fuelAmount = 20,
                            Money = 0,
                            totalDestroyedAsteroids = 0,
                            totalKilledPirateAmnt = 0
                        };
                        while (true) //SYSTEM STATS HERE
                        {
                            //setup console
                            Console.SetWindowSize(124, 50);

                            //increment explred systems
                            playerShip.exploredSystemsAmount++;

                            //INIT SOLAR SYSTEM
                            SolarSystem newSolarSystem = new SolarSystem
                            {
                                hasAsteroids = SolarSystem.GetRandomBool(),
                                planetAmount = SolarSystem.SetPlanetAmount(),
                                hasElectricClouds = SolarSystem.GetRandomBool(),
                                name = SolarSystem.GenerateName(),
                                hostility = 0
                            };

                            //CREATE AND STORE PLANETS ON A LIST
                            for (int i = 0; i < newSolarSystem.planetAmount; i++)
                            {
                                Planet newPlanet = new Planet
                                {
                                    areResources = Planet.SetResources(),
                                    isFuel = Planet.SetFuel(),
                                    occupiedByLiving = Planet.SetOccupation(),
                                    name = Planet.GenerateName(),
                                    isDiscovered = false
                                };

                                //IF THERES INTELIGENT LIFE(newplnt.living==2) SET ITS HOSTILITY AND UPDATE SYSTEMS HOSTILITY TO THE NEW HIGHEST ONE
                                if (newPlanet.occupiedByLiving == 2) 
                                {
                                    newPlanet.hostility = Planet.SetHostility();
                                    if (newPlanet.hostility > newSolarSystem.hostility)
                                    {
                                        newSolarSystem.hostility = newPlanet.hostility;
                                    }
                                    
                                }
                                else
                                {
                                    newPlanet.hostility = 0;
                                }

                                //IF PRIMITIVE LIFE = FUEL.TRUE; IF INTELIGENT LIFE=RESORUCES.TRUE, FUEL.TRUE
                                if (newPlanet.occupiedByLiving == 1) 
                                {
                                    newPlanet.isFuel = true;

                                }
                                else if (newPlanet.occupiedByLiving == 2)
                                {
                                    newPlanet.isFuel = true;
                                    newPlanet.areResources = true;
                                }

                                newSolarSystem.planetsList.Add(newPlanet);

                            }

                            //GAME STARTS HERE
                            while (true)
                            {
                                Console.SetWindowSize(124, 50);

                                if (playerShip.fuelAmount <= 0 || playerShip.shipHealth <= 0)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Clear();
                                    Console.WriteLine();
                                    Console.WriteLine("You ran out of either HP or Fuel, so you lost!");
                                    Console.WriteLine("Press any key to go to main menu...");
                                    Console.ReadKey();
                                    IWANTTOLEAVE = true;
                                    break;
                                }

                                //IF ITS THE 5TH TURN, ADD SHOP TO SOLARSYSTEM
                                if (playerShip.exploredSystemsAmount % 3 == 0)
                                {
                                    newSolarSystem.shop = true;
                                }
                                

                                //RENDER GUI
                                int input1 = Renderer.ChooseAction(newSolarSystem, newSolarSystem.planetsList, playerShip);
                                
                                //PLAYER WANTS EXPLORE
                                if (input1 == 1)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("type the index number of the planet you want to explore[ENTER]...");
                                    int input2 = Convert.ToInt32(Console.ReadLine()); //GETS PLAYERS INDEX OF EXPLORE

                                    // CHECKS IF THE PLANET CAN BE EXPLORED
                                    if (newSolarSystem.planetsList[input2].isDiscovered) 
                                    {

                                        newSolarSystem.planetsList[input2].isExplored = true;

                                        //IF PLANET HAS FUEL, ADD FUEL TO SHIP
                                        if (newSolarSystem.planetsList[input2].isFuel) 
                                        {
                                            playerShip.fuelAmount += Planet.AddRandomFuelAmount();
                                            newSolarSystem.planetsList[input2].isFuel = false;
                                        }

                                        //STARTS MINIGAME IF HOSTILITY > 0
                                        if(newSolarSystem.planetsList[input2].hostility > 0 || newSolarSystem.hasAsteroids)
                                        {
                                            playerShip = SpaceShooter.MainProgram.ShooterMain(playerShip, newSolarSystem, input2);
                                        }
                                        

                                        if (newSolarSystem.planetsList[input2].areResources)
                                        {
                                            newSolarSystem.planetsList[input2].areResources = false;
                                            playerShip.scannerAmount += 1;
                                        }
                                    }

                                    // IF THE PLAYERS INDEX ISNT EPLOREABLE
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("You cant explore this planet, you havent discovered it yet");
                                        Console.WriteLine("Press Any key to continue...");
                                        Console.ReadKey();

                                    }
                                }
                                else if (input1 == 2) //PLAYER WANTS TO SCAN
                                {
                                    if (playerShip.scannerAmount > 0)
                                    {
                                        playerShip.scannerAmount -= 1;
                                        int[] scanIndexes = PlayerShip.GetScanIndexes(newSolarSystem); //RETURNS ARRAY OF RANDOM INDEXES TO SCAN
                                        foreach (int a in scanIndexes) //MAKES INDEXES DISCOVERED
                                        {
                                            newSolarSystem.planetsList[a].isDiscovered = true;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You dont have enough Scanners!");
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                    }
                                }
                                else if (input1 == 3) //PLAYER WANDERS
                                {
                                    int randomPlanetIndex = newSolarSystem.planetsList.IndexOf(newSolarSystem.planetsList[PlayerShip.Wander(newSolarSystem)]);
                                    playerShip = SpaceShooter.MainProgram.ShooterMain(playerShip,newSolarSystem , randomPlanetIndex);
                                    newSolarSystem.planetsList[randomPlanetIndex].isDiscovered = true;
                                    newSolarSystem.planetsList[randomPlanetIndex].isExplored = true;
                                    if (newSolarSystem.planetsList[randomPlanetIndex].isFuel)
                                    {
                                        playerShip.fuelAmount += Planet.AddRandomFuelAmount();
                                        newSolarSystem.planetsList[randomPlanetIndex].isFuel = false;
                                    }
                                    if (newSolarSystem.planetsList[randomPlanetIndex].areResources)
                                    {
                                        playerShip.scannerAmount += 1;
                                        newSolarSystem.planetsList[randomPlanetIndex].areResources = false;
                                    }
                                    playerShip.fuelAmount -= 3;

                                }
                                else if (input1 == 4) //NEW GALAXY
                                {
                                    playerShip.fuelAmount -= 10;
                                    break;
                                }
                                else if(input1 == 5)//SHOP
                                {
                                    playerShip = Renderer.Shop(playerShip);
                                }
                                else if(input1 == 6)
                                {
                                    IWANTTOLEAVE = true;
                                    break;
                                }

                            }
                            
                            if(IWANTTOLEAVE)
                            {
                                break;
                            }

                        }
                        if (IWANTTOLEAVE)
                        {
                            IWANTTOLEAVE = false;
                            break;
                        }
                    }


                }
            }



        }
    }
}
