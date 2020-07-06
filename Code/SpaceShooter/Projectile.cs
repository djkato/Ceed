using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Projectile
    {
        public string[,] positionGrid = new string[Render.yGridSize, Render.xGridSize];
        public static int speed = 2;
        public int[] position = new int[2];
        public bool shotByPlayer;
        public bool isOutsideBounds = false;
        public static Projectile CreateProjectile(int[] position, bool shotByPlayer)
        {
            
            Projectile _projectile = new Projectile();
            _projectile.position[0] = position[0];
            _projectile.position[1] = position[1];
            _projectile.shotByPlayer = shotByPlayer;
            _projectile.positionGrid[position[0] - 1, position[1] - 5] = "|";
            _projectile.positionGrid[position[0] - 1, position[1] + 4] = "|";
            return _projectile;
        }
        public static Projectile MoveProjectie(Projectile _projectile)
        {
            if (_projectile.shotByPlayer)
            {
                _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] - 5] = null;
                _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] + 4] = null;
                if (_projectile.position[0] < 3 || _projectile.position[0] >= Render.yGridSize-2)
                {
                    _projectile.isOutsideBounds = true;
                }
                else
                {
                    _projectile.position[0] -= Projectile.speed;
                    _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] - 5] = "|";
                    _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] + 4] = "|";
                    
                }
                
            }
            else
            {
                _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] - 5] = null;
                _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] + 4] = null;
                if (_projectile.position[0] < 3 || _projectile.position[0] >= Render.yGridSize - 3)
                {
                    _projectile.isOutsideBounds = true;
                }
                else
                {
                    _projectile.position[0] += Projectile.speed;
                    _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] - 5] = "*";
                    _projectile.positionGrid[_projectile.position[0] - 1, _projectile.position[1] + 4] = "*";

                }

            }
            return _projectile;
        }
    }
}
