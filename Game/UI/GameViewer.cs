using OceanLogic;
using OceanLogic.GameObjects;
using OceanLogic.GameObjects.AbstractObjects;
using OceanLogic.Interfaces;
using System;

namespace Game.UI
{
    public class GameViewer : IGameViewer
    {
        #region Fields
        private readonly IOceanViewer _ocean;
        #endregion

        #region Ctor
        public GameViewer(IOceanViewer ocean)
        {
            _ocean = ocean;
        }
        #endregion

        #region Methods
        private void ColorCell(Cell cell) //Colors the cell
        {
            if (cell is Prey)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (cell is Predator)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (cell is Obstacle)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void DisplayExplanation() //Display information about game object in game
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nf - Prey\t");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("S - Predator\t");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("# - Obstacle\n");
        }

        private void DisplayBorder() //Displays field boundaries
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < _ocean.NumColumns; i++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }

        private void DisplayCells() //Displays array
        {
            for (int i = 0; i < _ocean.NumRows; i++)
            {
                for (int j = 0; j < _ocean.NumColumns; j++)
                {
                    Cell cell = _ocean.GetCellAt(i, j);

                    if (cell == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(GameSettings.defaultEmptyImage);
                    }
                    else
                    {
                        ColorCell(cell);
                        Console.Write(cell.Image);
                    }
                }

                Console.WriteLine();
            }
        }

        private void DisplayStats(int iteration) //Displays information about prey, obstacles, predators and iteration
        {
            int numObstacles = 0, numPrey = 0, numPredators = 0;

            for (int i = 0; i < _ocean.NumRows; i++)
            {
                for (int j = 0; j < _ocean.NumColumns; j++)
                {
                    Cell cell = _ocean.GetCellAt(i, j);

                    if (cell != null)
                    {
                        switch (cell.Image)
                        {
                            case GameSettings.defaultPreyImage:
                                numPrey++;
                                break;
                            case GameSettings.defaultPredatorImage:
                                numPredators++;
                                break;
                            case GameSettings.defaultObstacleImage:
                                numObstacles++;
                                break;
                        }
                    }
                }
            }

            _ocean.NumPrey = numPrey;
            _ocean.NumPredators = numPredators;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Iteration number: {0}", ++iteration);
            Console.WriteLine("Obstacles: {0}", numObstacles);
            Console.WriteLine("Prey: {0}     ", numPrey);
            Console.WriteLine("Predators: {0}     ", numPredators);
        }

        public void DisplayGameOver() //Displays message when game over
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n**********");
            Console.WriteLine("Game over!");
            Console.WriteLine("**********");
        }

        public void DisplayField(int iteration) //Displays field
        {
            Console.SetCursorPosition(0, 0);
            DisplayStats(iteration);
            DisplayExplanation();
            DisplayBorder();
            DisplayCells();
            DisplayBorder();
        }
    }
    #endregion
}
