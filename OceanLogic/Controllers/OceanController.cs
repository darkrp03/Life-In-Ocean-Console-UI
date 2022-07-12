using OceanLogic.GameObjects;
using OceanLogic.Interfaces;
using System.Threading;

namespace OceanLogic.Controllers
{
    public class OceanController
    {
        #region Fields
        private readonly Ocean _ocean;
        private readonly IGameViewer _gameView;
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
            AddObstacle();
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
