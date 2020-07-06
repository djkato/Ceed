using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CeedMain;

namespace SpaceShooter
{
    class Render
    {
        private static StringBuilder sb = new StringBuilder();
        private static readonly Random _random = new Random();
        public static int xGridSize = 40;
        public static int yGridSize = 30;
        public string[,] renderGrid = new string[yGridSize, xGridSize];


        //Actual code lol
        public static string[,] AddBackgroundStars(string[,] _renderGrid)
        {
            
            int starAmnt = _random.Next(0, 500);
            for (int i = 0; i < starAmnt; i++)
            {
                _renderGrid[_random.Next(0, yGridSize), _random.Next(0, xGridSize)] = ",";
            }
            return _renderGrid;
        }

        public static string[,] AddPositionedElement(string[,] Model, int elementSizeX, int elementSizeY, int elementPosX, int elementPosY)
        {

            string[,] renderGrid = new string[yGridSize, xGridSize];
            for (int i = 0; i < elementSizeY; i++)
            {
                for (int t = 0; t < elementSizeX; t++)
                {
                    
                    int normalizedElementPosX = Math.Min(xGridSize-((int)Math.Ceiling((float)elementSizeX/2)), Math.Max(((int)Math.Ceiling((float)elementSizeY / 2)), elementPosX));
                    int normalizedElementPosY = Math.Min(yGridSize - ((int)Math.Ceiling((float)elementSizeY / 2)), Math.Max(((int)Math.Ceiling((float)elementSizeY / 2)), elementPosY));
                    /**
                    int normalizedElementPosX = Math.Min(xGridSize - elementSizeX / 2, Math.Max(elementSizeX / 2, elementPosX));
                    int normalizedElementPosY = Math.Min(yGridSize - elementSizeY / 2, Math.Max(elementSizeY / 2, elementPosY));
                    **/
                    renderGrid[i + normalizedElementPosY - elementSizeY / 2, t + normalizedElementPosX- elementSizeX / 2] = Model[i, t];
                    
                 }

            }
            return renderGrid;
        }

        public static void OutputFrame(PlayerShip _ship, string[,] renderGrid, List<Asteroid> asteroidList,List<Projectile> projectileList, List<Pirate> pirateList)
        {
            //resets main renderLayer to 0
            for (int i = 0; i < Render.yGridSize; i++)
            {
                for (int t = 0; t < Render.xGridSize; t++)
                {
                    renderGrid[i, t] = null;
                }
            }
            //Adds all elements to the render frame buffer
            string[,] renderGridBackground = AddBackgroundStars(renderGrid);
            string[,] renderGridShip = AddPositionedElement(_ship.shipModel, 10, 4, _ship.shipPosX, _ship.shipPosY);
            string[,] renderGridAsteroids = new string[yGridSize, xGridSize];
            string[,] renderGridProjectiles = new string[yGridSize, xGridSize];
            string[,] renderGridPirates = new string[yGridSize, xGridSize];
            
            //Merges all asteroids into one layer
            foreach (Asteroid a in asteroidList) 
            {
                string[,] tempRenderGridAsteroids = AddPositionedElement(a.AsteroidModel, 7, 5, a.asteroidPosX, a.asteroidPosY);
                for (int i = 0; i < Render.yGridSize; i++)
                {
                    for (int t = 0; t < Render.xGridSize; t++)
                    {
                        if (renderGridAsteroids[i, t] == "" || renderGridAsteroids[i, t] == null)
                        {
                            renderGridAsteroids[i, t] = tempRenderGridAsteroids[i, t];
                        }
                    }
                }
            }
            
            //Merges all projectiles into one layer
            foreach (Projectile a in projectileList)
            {
                
                for (int i = 0; i < Render.yGridSize; i++)
                {
                    for (int t = 0; t < Render.xGridSize; t++)
                    {
                        if (renderGridProjectiles[i, t] == null || renderGridProjectiles[i, t] == "")
                        {
                            renderGridProjectiles[i, t] = a.positionGrid[i, t];
                        }
                    }
                }
            }
            
            //Merges all pirates into one layer
            foreach(Pirate a in pirateList)
            {
                string[,] tempRenderGridPirates = AddPositionedElement(a.shipModel, 10, 3, a.shipPosX, a.shipPosY);
                for (int i = 0; i < Render.yGridSize; i++)
                {
                    for (int t = 0; t < Render.xGridSize; t++)
                    {
                        if (renderGridPirates[i, t] == "" || renderGridPirates[i, t] == null)
                        {
                            renderGridPirates[i, t] = tempRenderGridPirates[i, t];
                        }
                    }
                }
            }
        
            //Merges all layers into final renderGrid layer
            for (int i = 0; i < Render.yGridSize; i++)
            {
                for (int t = 0; t < Render.xGridSize; t++)
                {
                    if (renderGrid[i, t] == "" || renderGrid[i, t] == null || renderGrid[i, t] == ",")
                    {
                        if (renderGrid[i, t] == "" || renderGrid[i, t] == null || renderGrid[i, t] == ",")
                        {
                            renderGrid[i, t] = renderGridShip[i, t];
                        }
                    }
                }
            }
            for (int i = 0; i < Render.yGridSize; i++)
            {
                for (int t = 0; t < Render.xGridSize; t++)
                {
                        if (renderGrid[i, t] == "" || renderGrid[i, t] == null || renderGrid[i, t] == ",")
                        {
                            renderGrid[i, t] = renderGridPirates[i, t];
                        }
                    
                }
            }
            for (int i = 0; i < Render.yGridSize; i++)
            {
                for (int t = 0; t < Render.xGridSize; t++)
                {
                    if (renderGrid[i, t] == "" || renderGrid[i, t] == null)
                    {
                        
                            renderGrid[i, t] = renderGridProjectiles[i, t];
                        
                    }
                }
            }
            for (int i = 0; i < Render.yGridSize; i++)
            {
                for (int t = 0; t < Render.xGridSize; t++)
                {
                    if (renderGrid[i, t] == "" || renderGrid[i, t] == null)
                    {
                        if (renderGrid[i, t] == "" || renderGrid[i, t] == null)
                        {
                            renderGrid[i, t] = renderGridAsteroids[i, t];
                        }
                    }
                }
            }
            for (int i = 0; i < Render.yGridSize; i++)
            {
                for (int t = 0; t < Render.xGridSize; t++)
                {
                    if (renderGrid[i, t] == "" || renderGrid[i, t] == null)
                    {
                        renderGrid[i, t] = renderGridBackground[i, t];
                    }
                }
            }

            //ADDS HUD INFO

            //hp
            renderGrid[yGridSize - 2, xGridSize - 5] = "H";
            renderGrid[yGridSize - 2, xGridSize - 4] = "P";
            renderGrid[yGridSize - 2, xGridSize - 3] = ":";
            renderGrid[yGridSize - 2, xGridSize - 2] = _ship.shipHealth.ToString();

            //_ship.killedPirateAmnt
            renderGrid[yGridSize - 1, xGridSize - 8] = "K";
            renderGrid[yGridSize - 1, xGridSize - 7] = "I";
            renderGrid[yGridSize - 1, xGridSize - 6] = "L";
            renderGrid[yGridSize - 1, xGridSize - 5] = "L";
            renderGrid[yGridSize - 1, xGridSize - 4] = "S";
            renderGrid[yGridSize - 1, xGridSize - 3] = ":";
            renderGrid[yGridSize - 1, xGridSize - 2] = Convert.ToString(_ship.killedPirateAmnt);


            //replaces all nulls with spaces
            for (int i = 0; i < yGridSize; i++)
            {
                for (int t = 0; t < xGridSize; t++)
                {
                    if (renderGrid[i, t] == "" || renderGrid[i, t] == null)
                    {
                        renderGrid[i, t] = " ";
                    }
                }
            }

            //Sets the window to be the size the game needs & centers position
            Console.SetWindowSize(xGridSize+3, yGridSize+4);

            //Sets up console
            sb.Clear();
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            
            //Renders with frame to Console
            sb.AppendLine("");
            for(int i = 0; i < xGridSize;i++)
            {
                sb.Append("_");
            }

            for (int i = 0; i < yGridSize; i++)
            {
                sb.Append("|");
                for (int t = 0; t < xGridSize; t++)
                {
                    sb.Append(renderGrid[i, t]);
                }
                sb.AppendLine("|");
            }

            sb.AppendLine("");
            for (int i = 0; i < xGridSize; i++)
            {
                sb.Append("_");
            }
            Console.WriteLine(sb);

        }
    }
}
