using Game.Enums;
using Game.UI;
using OceanLogic;
using OceanLogic.Controllers;
using OceanLogic.Exceptions;
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

            int numPrey = 0, numPredators = 0, numObstacles = 0, numIterations = 0;

            try
            {
                Console.Write("Input a number of prey: ");
                while (!Int32.TryParse(Console.ReadLine(), out numPrey))
                {
                    Console.Write("Incorrect value! Reinput: ");
                }

                Console.Write("Input a number of predators: ");
                while (!Int32.TryParse(Console.ReadLine(), out numPredators))
                {
                    Console.Write("Incorrect value! Reinput: ");
                }

                Console.Write("Input a number of obstacles: ");
                while (!Int32.TryParse(Console.ReadLine(), out numObstacles))
                {
                    Console.Write("Incorrect value! Reinput: ");
                }

                Console.Write("Input a number of iterations: ");
                while (!Int32.TryParse(Console.ReadLine(), out numIterations))
                {
                    Console.Write("Incorrect value! Reinput: ");
                }

                while (numIterations < 0)
                {
                    Console.Write("Number of iterations cannot be less than 0! Reinput: ");
                    numIterations = Int32.Parse(Console.ReadLine());
                }

                int numberGameObject = numPredators + numPrey + numObstacles;
                int fieldSize = _ocean.NumColumns * _ocean.NumRows;

                if (numberGameObject > fieldSize)
                {
                    throw new GameFieldOverfillingException();
                }
                
            }
            catch (GameFieldOverfillingException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace: {0}", e.StackTrace);
                Console.ReadKey();
                Environment.Exit(0);
            }

            return (numPrey, numPredators, numObstacles, numIterations);
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
