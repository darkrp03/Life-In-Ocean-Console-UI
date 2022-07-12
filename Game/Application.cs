using Game.Enums;
using Game.UI;
using OceanLogic;
using OceanLogic.Controllers;
using System;

namespace Game
{
    internal class Application
    {
        private readonly Ocean _ocean;
        private readonly OceanController _oceanController;
        private readonly GameViewer _gameViewer;

        public Application()
        {
            _ocean = new Ocean();
            _gameViewer = new GameViewer(_ocean);
            _oceanController = new OceanController(_ocean, _gameViewer);
        }

        private (int, int, int, int) ChangeSettings()
        {
            Console.Clear();
            Console.WriteLine("Current amount of prey (default is {0}): {1}", GameSettings.defaultNumPrey, _ocean.NumPrey);
            Console.WriteLine("Current amount of predators (default is {0}): {1}", GameSettings.defaultNumPredators, _ocean.NumPredators);
            Console.WriteLine("Current amount of obstacles (default is {0}): {1}", GameSettings.defaultNumObstacles, _ocean.NumObstacles);
            Console.WriteLine("Current amount of iterations (default is {0}): {1}\n", GameSettings.defaultNumIterations, _ocean.NumIterations);

            int numPrey, numPredators, numObstacles, numIterations;

            try
            {
                Console.Write("Input a number of prey: ");
                numPrey = Int32.Parse(Console.ReadLine());

                if (numPrey >= (_ocean.NumRows * _ocean.NumColumns))
                {
                    numPrey /= 4;
                }

                Console.Write("Input a number of predators: ");
                numPredators = Int32.Parse(Console.ReadLine());

                if (numPredators >= (_ocean.NumRows * _ocean.NumColumns - _ocean.NumPrey))
                {
                    numPredators /= 4;
                }

                Console.Write("Input a number of obstacles: ");
                numObstacles = Int32.Parse(Console.ReadLine());

                if (numObstacles >= (_ocean.NumRows * _ocean.NumColumns))
                {
                    numObstacles /= 4;
                }

                Console.Write("Input a number of iterations: ");
                numIterations = Int32.Parse(Console.ReadLine());

                return (numPrey, numPredators, numObstacles, numIterations);
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong input!");
            }

            return (0, 0, 0, 0);
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("****************");
            Console.WriteLine("1.Play");
            Console.WriteLine("2.Change settings");
            Console.WriteLine("3.Exit");
            Console.WriteLine("****************");

            Console.Write("\nInput number: ");
        }

        private int GetMenuItemNumber()
        {
            return Int32.Parse(Console.ReadLine());
        }

        public void Run()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                DisplayMenu();

                int menuItemNumber = GetMenuItemNumber();

                Console.Clear();

                MenuItem menuItem = (MenuItem)menuItemNumber;

                switch (menuItem)
                {
                    case MenuItem.Play:
                        _oceanController.RunGame();
                        break;
                    case MenuItem.Settings:
                        (int, int, int, int) settings = ChangeSettings();
                        _oceanController.SetSettings(settings.Item1, settings.Item2, settings.Item3, settings.Item4);
                        break;
                    case MenuItem.Exit:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
