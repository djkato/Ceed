using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Asteroid
    {
        public static readonly Random _random = new Random(); 
        public int asteroidPosX;
        public int asteroidPosY;
        public int speed;
        public string[,] AsteroidModel = new string[5,7];
        public static string[,] SetAsteroidModel()
        {
            string[,] asteroidGraphic = new string[5,7];

            asteroidGraphic[0, 0] = " ";
            asteroidGraphic[0, 1] = "_";
            asteroidGraphic[0, 2] = "_";
            asteroidGraphic[0, 3] = "_";
            asteroidGraphic[0, 4] = " ";
            asteroidGraphic[0, 5] = " ";
            asteroidGraphic[0, 6] = " ";

            asteroidGraphic[1, 0] = "/";
            asteroidGraphic[1, 1] = " ";
            asteroidGraphic[1, 2] = " ";
            asteroidGraphic[1, 3] = " ";
            asteroidGraphic[1, 4] = @"\";
            asteroidGraphic[1, 5] = "_";

            asteroidGraphic[2, 0] = "|";
            asteroidGraphic[2, 1] = " ";
            asteroidGraphic[2, 2] = " ";
            asteroidGraphic[2, 3] = " ";
            asteroidGraphic[2, 4] = " ";
            asteroidGraphic[2, 5] = " ";
            asteroidGraphic[2, 6] = @"\";

            asteroidGraphic[3, 0] = @"\";
            asteroidGraphic[3, 1] = " ";
            asteroidGraphic[3, 2] = " ";
            asteroidGraphic[3, 3] = " ";
            asteroidGraphic[3, 4] = " ";
            asteroidGraphic[3, 5] = " ";
            asteroidGraphic[3, 6] = "/";

            asteroidGraphic[4, 0] = " ";
            asteroidGraphic[4, 1] = @"\";
            asteroidGraphic[4, 2] = "_";
            asteroidGraphic[4, 3] = "_";
            asteroidGraphic[4, 4] = "_";
            asteroidGraphic[4, 5] = "/";
            asteroidGraphic[4, 6] = " ";

            return asteroidGraphic;
        }

    }
}
