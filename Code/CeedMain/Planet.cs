using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeedMain
{
    class Planet
    {
        private static readonly Random  _random = new Random();
        public string name;
        public bool isExplored;
        public int hostility; //0 = peaceful ... 5 = deadly
        public int occupiedByLiving; //0 == no;1==primitive;2==intelligent
        public bool isFuel;
        public bool areResources;
        public bool isDiscovered;

        public static int AddRandomFuelAmount()
        {
            return (_random.Next(1, 5));
        }
        public static int SetOccupation()
        {

            int tmp = _random.Next(0, 2);
            if (tmp == 1)
            {
                return _random.Next(1, 3);
            }
            else
            {
                return 0;
            }
        }

        public static bool SetFuel()
        {
            
            int tmp = _random.Next(0, 2);
            if (tmp == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool SetResources()
        {
            
            int tmp = _random.Next(0, 2);
            if (tmp == 1)
            {
                return true;
            }
            else
            { 
                return false; 
            }
        }

        public static int SetHostility()
        {
           
            int tmp = _random.Next(1, 6);
            return tmp;
        }

        public static string GenerateName()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            

            int letterLength = _random.Next(1, 10);

            string name = "";
            int i;
            for (int f = 0; f < letterLength; f++)
            {
                i = _random.Next(chars.Length);
                name += Char.ToString(chars[i]);
            }

            return name;
        }

    }
}
