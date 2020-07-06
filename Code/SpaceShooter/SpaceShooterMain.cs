using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CeedMain;

namespace SpaceShooter
{
    class MainProgram
    {
        private static int pirateShipByHostilityAmount = 0;
        private readonly static Random _random = new Random();
        public static PlayerShip ShooterMain(PlayerShip _playerShip, SolarSystem _solarSystem, int planetIndex)
        {
            //initialise console
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Render _render = new Render();
            Console.Clear();
            
            //setup
            Asteroid _asteroids = new Asteroid();
            List<Projectile> projectileList = new List<Projectile>();
            List<Asteroid> asteroidList = new List<Asteroid>();
            List<Pirate> pirateList = new List<Pirate>();
            _playerShip.killedPirateAmnt = 0;

            //set graphic
            _playerShip.shipModel = PlayerShip.SetShipModel();
            //THREAD THAT MANAGES INPUT
            new Thread(() =>
            {
                while (true)
                {
                    Thread.CurrentThread.IsBackground = true;
                    _playerShip = GameLogic.Controller(_playerShip);
                }
            }).Start();

            while (true)
            {
                
                //DOES PIRATE STUFF 
                //makes sure hostility num = amount of pirate ships
                if (pirateList.Count <= _solarSystem.planetsList[planetIndex].hostility)
                {

                    int pirateAmnt = _random.Next(0, 15);
                    if (pirateAmnt == 4)
                    {
                        Pirate tempPirate = new Pirate
                        {
                            shipPosX = _random.Next(6, 26),
                            shipModel = Pirate.SetShipModel()
                        };
                        pirateList.Add(tempPirate);
                    }
                }
                for (int i = 0; i < pirateList.Count; i++)
                {
                    pirateList[i] = GameLogic.PirateLogic(pirateList[i], _playerShip);
                    if (pirateList[i].isOutsideBounds)
                    {
                        pirateList.Remove(pirateList[i]);
                    }
                }


                //IF ASTEROIDS IN SYSTEM, PREPARES ASTEROIDS EACH FRAME
                
                if(_solarSystem.hasAsteroids)
                {
                
                    int asteroidAmnt = _random.Next(0, 20);
                    if(asteroidAmnt == 3)
                {
                    for (int i = 0; i < asteroidAmnt; i++)
                    {
                        Asteroid tempAsteroid = new Asteroid();
                        tempAsteroid = GameLogic.AsteroidGenerator();
                        asteroidList.Add(tempAsteroid);
                    }
                }
                
                    //move asteroids
                    for (int i = 0; i < asteroidList.Count; i++)
                    {
                        asteroidList[i] = GameLogic.MoveAsteroid(asteroidList[i]);
                        if (asteroidList[i].asteroidPosY > Render.yGridSize)
                        {
                            asteroidList.Remove(asteroidList[i]);
                        }
                    }
                }
                //HANDLE PROJECTILES
                
                //pirate
                for (int i = 0; i < pirateList.Count; i++)
                {
                    if (pirateList[i].isFiring)
                    {
                        int[] pos = { pirateList[i].shipPosY, pirateList[i].shipPosX };
                        Projectile _projectile = Projectile.CreateProjectile(pos, false);
                        projectileList.Add(_projectile);
                        pirateList[i].isFiring = false;
                    }

                }

                //player
                if (_playerShip.isFiring)
                {
                    int[] pos = { _playerShip.shipPosY, _playerShip.shipPosX };
                    Projectile _projectile = Projectile.CreateProjectile(pos, true);
                    projectileList.Add(_projectile);
                    _playerShip.isFiring = false;
                }
                if (projectileList.Count > 0)
                {
                    for (int i = 0; i < projectileList.Count; i++)
                    {
                        if (projectileList[i].isOutsideBounds)
                        {
                            projectileList.Remove(projectileList[i]);
                        }
                        else
                        {
                            projectileList[i] = Projectile.MoveProjectie(projectileList[i]);
                        }
                    }
                }


                //COLLISION STUFF
                int[] collisionDataShipProjectile = GameLogic.CollisionShipProjectile(_playerShip, projectileList);
                if(collisionDataShipProjectile[1] > 0)
                {
                    _playerShip.shipHealth -= collisionDataShipProjectile[1];
                    projectileList.RemoveAt(collisionDataShipProjectile[0]);

                }

                int[] collisionDataShipAsteroid = GameLogic.CollisionShipAsteroid(_playerShip, asteroidList);
                if (collisionDataShipAsteroid[1] > 0)
                {
                    _playerShip.shipHealth -= collisionDataShipAsteroid[1];
                    _playerShip.totalDestroyedAsteroids += 1;
                    asteroidList.RemoveAt(collisionDataShipAsteroid[0]);

                }

                int[] collisionDataShipPirate = GameLogic.CollisionShipPirate(_playerShip, pirateList);
                if (collisionDataShipPirate[1] > 0)
                {
                    _playerShip.shipHealth -= collisionDataShipPirate[1];
                    pirateList.RemoveAt(collisionDataShipPirate[0]);
                    _playerShip.totalKilledPirateAmnt += 1;
                    _playerShip.killedPirateAmnt += 1;
                    _playerShip.Money += 1;

                }

                int[] collisionDataPirateProjectile = GameLogic.CollisionPirateProjectile(projectileList, pirateList);
                if (collisionDataPirateProjectile[1] > 0)
                {
                    pirateList[collisionDataPirateProjectile[2]].health -= collisionDataPirateProjectile[1];
                    if(pirateList[collisionDataPirateProjectile[2]].health <= 0)
                    {
                        _playerShip.Money += 1;
                        _playerShip.killedPirateAmnt += 1;
                        _playerShip.totalKilledPirateAmnt += 1;
                        pirateList.RemoveAt(collisionDataPirateProjectile[2]);
                    }
                    projectileList.RemoveAt(collisionDataPirateProjectile[0]);
                }

                int[] collisionDataAsteroidProjectile = GameLogic.CollisionAsteroidProjectile(projectileList, asteroidList);
                if (collisionDataAsteroidProjectile[1] > 0)
                {
                    asteroidList.RemoveAt(collisionDataAsteroidProjectile[2]);
                    projectileList.RemoveAt(collisionDataPirateProjectile[0]);
                    _playerShip.totalDestroyedAsteroids += 1;
                    _playerShip.Money += 1;
                }
                if (_playerShip.shipHealth <= 0)
                {
                    return _playerShip;
                }
                if (_playerShip.killedPirateAmnt >= (_solarSystem.planetsList[planetIndex].hostility* _solarSystem.planetsList[planetIndex].hostility) +1)
                {
                    return _playerShip;
                }

                //RENDER IT OUT
                Render.OutputFrame(_playerShip, _render.renderGrid, asteroidList, projectileList, pirateList);
                Thread.Sleep(70);
                Console.Clear();
            }
        }
    }
}
