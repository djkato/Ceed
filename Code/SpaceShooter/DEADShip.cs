using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class DEADShip
    {
        public int killedPirateAmnt = 0;
        public bool isFiring = false;
        public static int speed;
        public int health = 20;
        public int shipPosY = Render.yGridSize*2/3 ;
        public int shipPosX = Render.xGridSize/2;
        public string[,] shipModel = new string[4, 10];
        public static string[,] SetShipModel()
        {
            string[,] shipGraphic= new string[4, 10];
            shipGraphic[0, 0] = " ";
            shipGraphic[0, 1] = " ";
            shipGraphic[0, 2] = " ";    
            shipGraphic[0, 3] = " ";
            shipGraphic[0, 4] = "_";
            shipGraphic[0, 5] = "_";
            shipGraphic[0, 6] = " ";
            shipGraphic[0, 7] = " ";
            shipGraphic[0, 8] = " ";
            shipGraphic[0, 9] = " ";

            shipGraphic[1, 0] = " ";
            shipGraphic[1, 1] = " ";
            shipGraphic[1, 2] = " ";
            shipGraphic[1, 3] = "/";
            shipGraphic[1, 4] = " ";
            shipGraphic[1, 5] = " ";
            shipGraphic[1, 6] = @"\";
            shipGraphic[1, 7] = " ";
            shipGraphic[1, 8] = " ";
            shipGraphic[1, 9] = " ";

            shipGraphic[2, 0] = "|";
            shipGraphic[2, 1] = " ";
            shipGraphic[2, 2] = "_";
            shipGraphic[2, 3] = "|";
            shipGraphic[2, 4] = " ";
            shipGraphic[2, 5] = " ";
            shipGraphic[2, 6] = "|";
            shipGraphic[2, 7] = "_";
            shipGraphic[2, 8] = " ";
            shipGraphic[2, 9] = "|";

            shipGraphic[3, 0] = "|";
            shipGraphic[3, 1] = "=";
            shipGraphic[3, 2] = "|";
            shipGraphic[3, 3] = "_";
            shipGraphic[3, 4] = "|";
            shipGraphic[3, 5] = "|";
            shipGraphic[3, 6] = "_";
            shipGraphic[3, 7] = "|";
            shipGraphic[3, 8] = "=";
            shipGraphic[3, 9] = "|";
            return shipGraphic;
        }
        

    }
}
