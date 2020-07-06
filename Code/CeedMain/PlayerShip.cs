using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeedMain
{
    class PlayerShip : ShipTemplate
    {
        
        private static readonly Random _random = new Random();
        public int scannerAmount;
        public int fuelAmount;
        public int exploredSystemsAmount;
        public int Money;

        //SpaceShooter vars
        public int totalKilledPirateAmnt = 0;
        public int totalDestroyedAsteroids = 0;
        public int killedPirateAmnt = 0;
        public bool isFiring = false;
        public int shipPosY = SpaceShooter.Render.yGridSize * 2 / 3;
        public int shipPosX = SpaceShooter.Render.xGridSize / 2;
        public string[,] shipModel = new string[4, 10];

        //OG methods
        public static int[] GetScanIndexes(SolarSystem newSolarSytem)
        {
            var scanAmnt = _random.Next(0, newSolarSytem.planetAmount);
            int[] scanIndexs = new int[scanAmnt];
            for (int i = 0; i < scanAmnt; i++)
            {
                scanIndexs[i] = _random.Next(0, newSolarSytem.planetAmount);
            }
            return scanIndexs;
            
        }
        public static int Wander(SolarSystem newSolarSystem)
        {
            return (_random.Next(0, newSolarSystem.planetAmount));
        }

        //SpaceShooter Methods
        public static string[,] SetShipModel()
        {
            string[,] shipGraphic = new string[4, 10];
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
