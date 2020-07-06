using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CeedMain;

namespace SpaceShooter
{
    class GameLogic
    {
        private static int pirateY = 0;
        private static readonly int collisionDistShip = 5;
        
        private readonly static Random _random = new Random();

        //SHIP STUFF
        public static PlayerShip Controller(PlayerShip _ship)
        {
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {

                    case ConsoleKey.A:
                        if (_ship.shipPosX > 5)
                        { _ship.shipPosX--; }
                        break;
                    case ConsoleKey.D:
                        if (_ship.shipPosX < Render.xGridSize - 5)
                        { _ship.shipPosX++; }
                        break;
                    case ConsoleKey.W:
                        if (_ship.shipPosY > 3)
                        { _ship.shipPosY--; }
                        break;
                    case ConsoleKey.S:
                        if (_ship.shipPosY < Render.yGridSize - 3)
                        { _ship.shipPosY++; }
                        break;
                    case ConsoleKey.Spacebar:
                        _ship.isFiring = true;
                        break;
                }

                return _ship;

            }
        }

        //ASTEROID STUFF
        public static Asteroid AsteroidGenerator()
        {
            Asteroid _asteroid = new Asteroid
            {
                asteroidPosX = _random.Next(-5, Render.xGridSize + 5),
                asteroidPosY = 0,
                AsteroidModel = Asteroid.SetAsteroidModel(),
                speed = _random.Next(1, 3)
            };
            return _asteroid;

        }

        public static Asteroid MoveAsteroid(Asteroid _asteroid)
        {
            _asteroid.asteroidPosY += _asteroid.speed;
            return _asteroid;

        }

        //PIRATE STUFF
        public static Pirate PirateLogic(Pirate _pirate, PlayerShip _ship)
        {
            //FOLLOW PLAYERS X AT OWN SPEED; IF PIRATE.PosX = ship.posX, shoot!
            if (_pirate.shipPosY >= Render.yGridSize - 3)
            {
                _pirate.isOutsideBounds = true;
                return _pirate;
            }

            else
            {
                _pirate.targetX = _ship.shipPosX;
                pirateY++;
                
                if (_pirate.shipPosX > _pirate.targetX && _pirate.shipPosX < _pirate.targetX+3 || _pirate.shipPosX < _pirate.targetX && _pirate.shipPosX > _pirate.targetX - 3)
                {
                    _pirate.isFiringNum += 1;
                    if(_pirate.isFiringNum % 5 == 0)
                    {

                        _pirate.isFiring = true;
                    }
                    
                }

                else if (_pirate.shipPosX > _pirate.targetX)
                {
                    _pirate.isFiring = false;
                    if (pirateY % 5 == 0)
                    {
                        _pirate.shipPosX -= _pirate.speed;
                    }

                }
                else
                {
                    _pirate.isFiring = false;
                    if (pirateY % 5 == 0)
                    {
                        _pirate.shipPosX += _pirate.speed;
                    }
                }
                //MOVES the PIRAte SLOOWLY DOWNWARDS


                pirateY++;
                if (pirateY % 5 == 0)
                {
                    _pirate.shipPosY += 1;
                }

                return _pirate;
            }
        }

        //COLISSION STUFF
        public static int[] CollisionShipProjectile(PlayerShip _ship, List<Projectile> projectileList)
        {
            /**output[0] = index of collided element output[1] Damage ship takes **/
            int[] output = new int[2];
            output[0] = 0;
            output[1] = 0;
            foreach (Projectile a in projectileList)
            {
                if (a.shotByPlayer == false)
                {
                    if ((_ship.shipPosX - a.position[1]) * (_ship.shipPosX - a.position[1]) + (_ship.shipPosY - a.position[0]) * (_ship.shipPosY - a.position[0]) < collisionDistShip * collisionDistShip)
                    {
                        output[0] = projectileList.IndexOf(a);
                        output[1] = 1;
                        break;
                    }
                    else
                    {
                        output[1] = 0;
                    }
                }
            }
            return output;
        }

        public static int[] CollisionShipAsteroid(PlayerShip _ship, List<Asteroid> asteroidList)
        {
            /**output[0] = index of collided element output[1] Damage ship takes **/
            int[] output = new int[2];
            output[0] = 0;
            output[1] = 0;
            foreach (Asteroid a in asteroidList)
            {
                if ((_ship.shipPosX - a.asteroidPosX) * (_ship.shipPosX - a.asteroidPosX) + (_ship.shipPosY - a.asteroidPosY) * (_ship.shipPosY - a.asteroidPosY) < collisionDistShip * collisionDistShip)
                {
                    output[0] = asteroidList.IndexOf(a);
                    output[1] = 2;
                    break;
                }
                else
                {
                    output[1] = 0;
                }
            }
            return output;
        }

        public static int[] CollisionShipPirate(PlayerShip _ship, List<Pirate> pirateList)
        {
            /**output[0] = index of collided element output[1] Damage ship takes **/
            int[] output = new int[2];
            output[0] = 0;
            output[1] = 0;
            foreach (Pirate a in pirateList)
            {
                if ((_ship.shipPosX - a.shipPosX) * (_ship.shipPosX - a.shipPosX) + (_ship.shipPosY - a.shipPosY) * (_ship.shipPosY - a.shipPosY) < collisionDistShip * collisionDistShip)
                {
                    output[0] = pirateList.IndexOf(a);
                    output[1] = 2;
                    break;
                }
                else
                {
                    output[1] = 0;
                }
            }
            return output;
        }

        public static int[] CollisionPirateProjectile(List<Projectile> projectileList, List<Pirate> pirateList)
        {
            /**output[0] = index of collided projectile, output[1] Damage ship takes, output[2] Index of pirate **/
            int[] output = new int[3];
            foreach (Projectile a in projectileList)
            {
                foreach (Pirate b in pirateList)
                {
                    if (a.shotByPlayer == true)
                    {
                        if ((b.shipPosX - a.position[1]) * (b.shipPosX - a.position[1]) + (b.shipPosY - a.position[0]) * (b.shipPosY - a.position[0]) < collisionDistShip * collisionDistShip)
                        {
                            output[0] = projectileList.IndexOf(a);
                            output[1] = 1;
                            output[2] = pirateList.IndexOf(b);
                            break;
                        }
                    }
                    else
                    {
                        output[1] = 0;
                    }
                }
            }
            return output;
        }
        public static int[] CollisionAsteroidProjectile(List<Projectile> projectileList, List<Asteroid> asteroidList)
        {
            /**output[0] = index of collided projectile, output[1] Damage ship takes, output[2] Index of pirate **/
            int[] output = new int[3];
            foreach (Projectile a in projectileList)
            {
                foreach (Asteroid b in asteroidList)
                {
                    if (a.shotByPlayer == true)
                    {
                        if ((b.asteroidPosX - a.position[1]) * (b.asteroidPosX - a.position[1]) + (b.asteroidPosY - a.position[0]) * (b.asteroidPosY - a.position[0]) < collisionDistShip * collisionDistShip)
                        {
                            output[0] = projectileList.IndexOf(a);
                            output[1] = 1;
                            output[2] = asteroidList.IndexOf(b);
                            break;
                        }
                    }
                    else
                    {
                        output[1] = 0;
                    }
                }
            }
            return output;
        }
    }

}
