using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Pirate
    {
        public bool isFiring;
        public int isFiringNum = 0;
        public int health = 3;
        public int shipPosX = Render.xGridSize/2;
        public int shipPosY = 2;
        public int targetX;
        public int speed = 1;
        public string[,] shipModel = new string[4, 6];
        public bool isOutsideBounds = false;
        public static string[,] SetShipModel()
        {

            string[,] result = new string[3, 10];

            result[0, 0] = " ";
            result[0, 1] = " ";
            result[0, 2] = "_";
            result[0, 3] = "_";
            result[0, 4] = "|";
            result[0, 5] = "|";
            result[0, 6] = "_";
            result[0, 7] = "_";
            result[0, 8] = " ";
            result[0, 9] = " ";

            result[1, 0] = "|";
            result[1, 1] = "=";
            result[1, 2] = @"\";
            result[1, 3] = " ";
            result[1, 4] = "|";
            result[1, 5] = "|";
            result[1, 6] = " ";
            result[1, 7] = "/";
            result[1, 8] = "=";
            result[1, 9] = "|";

            result[2, 0] = " ";
            result[2, 1] = " ";
            result[2, 2] = " ";
            result[2, 3] = @"\";
            result[2, 4] = "_";
            result[2, 5] = "_";
            result[2, 6] = "/";
            result[2, 7] = " ";
            result[2, 8] = " ";
            result[2, 9] = " ";
            return result;
        }
    
    }
}
