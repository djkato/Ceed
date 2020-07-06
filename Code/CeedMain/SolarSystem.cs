using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CeedMain
{
    class SolarSystem
    {
        private static readonly Random _random = new Random();
        public List<Planet> planetsList = new List<Planet>();
        public string name;
        public int hostility; //0 = peaceful ... 5 = deadly
        public int planetAmount;
        public int occupiedByLiving; //0 == no;1==primitive;2==intelligent
        public bool hasAsteroids;
        public bool shop;
        public bool hasElectricClouds;
        
        public static string GenerateName()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            

            int letterLength = _random.Next(1, 5);
            int numberLength = _random.Next(1, 10);
            string name = "";
            int i;
            for (int f = 0; f < letterLength; f++)
            {
                i = _random.Next(chars.Length);
                name += Char.ToString(chars[i]);
            }
            name += "-";
            for (int f = 0; f < numberLength; f++)
            {
                name += _random.Next(0, 9).ToString();
            }

            return (name);
        }
        public static int SetPlanetAmount()
        {
            

            return (_random.Next(2, 13));
        }
        public static int SetMoonAmount()
        {
            

            return (_random.Next(0, 6));
        }
        public static bool GetRandomBool()
        {
            
            var temp = _random.Next(0, 2);
            if (temp == 0)
            {
                return false;
            }
            if (temp == 1)
            {
                return true;
            }
            else { return false; }
        }
    }
}
