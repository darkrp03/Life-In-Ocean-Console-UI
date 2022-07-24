using OceanLogic.GameObjects;
using OceanLogic.Interfaces;
using System;
using System.Threading;

namespace OceanLogic.Controllers
{
    public class OceanController
    {
        #region Fields
        private readonly Ocean _ocean;
        private readonly IGameViewer _gameView;
        private bool _isPausedGame = false;
        #endregion

        #region Ctor
        public OceanController(Ocean ocean, IGameViewer consoleView)
        {
            _ocean = ocean;
            _gameView = consoleView;
        }
        #endregion

        #region Methods
        private void AddObstacle() //Add obstacles in ocean
        {
            for (int i = 0; i < _ocean.NumObstacles; i++)
            {
                var coordinate = _ocean.Direction.GetNullPosition();

                _ocean[coordinate.X, coordinate.Y] = new Obstacle();
            }
        }

        private void AddPredator() //Add predators in ocean
        {
            for (int i = 0; i < _ocean.NumPredators; i++)
            {
                var coordinate = _ocean.Direction.GetNullPosition();

                _ocean[coordinate.X, coordinate.Y] = new Predator(new Coordinate
                {
                    X = coordinate.X,
                    Y = coordinate.Y
                }, _ocean);
            }
        }

        private void AddKillerWhales() //Add killer whales in ocean
        {
            for (int i = 0; i < _ocean.NumKillerWhales; i++)
            {
                var coordinate = _ocean.Direction.GetNullPosition();

                _ocean[coordinate.X, coordinate.Y] = new KillerWhale(new Coordinate
                {
                    X = coordinate.X,
                    Y = coordinate.Y
                }, _ocean);
            }
        }

        private void AddPrey() //Add prey in ocean
        {
            for (int i = 0; i < _ocean.NumPrey; i++)
            {
                var coordinate = _ocean.Direction.GetNullPosition();

                _ocean[coordinate.X, coordinate.Y] = new Prey(new Coordinate
                {
                    X = coordinate.X,
                    Y = coordinate.Y
                }, _ocean);
            }
        }

        private void GenerateObjects()
        {
            AddPrey();
            AddPredator();
            AddKillerWhales();
            AddObstacle();
        }

        private void DetectKey() //Detects the key that the user enters
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.P)
                {
                    PauseOrResumeGame();
                }
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void PauseOrResumeGame()
        {
            if (_isPausedGame)
            {
                _isPausedGame = false;
            }
            else
            {
                _isPausedGame = true;
            }
        }

        public void SetSettings(int numPrey, int numPredators, int numObstacles, int numIterations)
        {
            _ocean.NumPrey = numPrey;
            _ocean.NumPredators = numPredators;
            _ocean.NumObstacles = numObstacles;
            _ocean.NumIterations = numIterations;
        }

        public void RunGame()
        {
            GenerateObjects();

            for (int i = 0; i < _ocean.NumIterations; i++)
            {
                DetectKey();

                while (_isPausedGame)
                {
                    DetectKey();

                    if (!_isPausedGame)
                    {
                        break;
                    }

                    Thread.Sleep(50);
                }

                if (_ocean.NumPrey <= 0 || _ocean.NumPredators <= 0)
                {
                    break;
                }

                _gameView.DisplayField(i);
                _ocean.Run();
            }

            _gameView.DisplayGameOver();

            Thread.Sleep(2000);
            _ocean.ResetSettings();
        }
        #endregion
    }
}
