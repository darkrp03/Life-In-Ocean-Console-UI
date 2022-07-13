using OceanLogic.GameObjects.AbstractObjects;
using OceanLogic.Interfaces;
using System;
using System.Threading;

namespace OceanLogic
{
    public class Ocean : IOcean, IOceanViewer
    {
        #region Fields
        private readonly Direction _direction; //Object to search for a cell and coordinates
        private Cell[,] _cells; //Playing field
        private bool[,] _movedCells; //Array for avoid dublicate method Process
        private int _numRows = GameSettings.defaultNumRows; //Number of rows on he playing field
        private int _numColumns = GameSettings.defaultNumCols; //Number of columns on he playing field
        private int _numPrey = GameSettings.defaultNumPrey; //Number of prey in the ocean
        private int _numPredators = GameSettings.defaultNumPredators; //Number of predators in the ocean
        private int _numObstacles = GameSettings.defaultNumObstacles; //Number of obstacles in the ocean
        private int _numIterations = GameSettings.defaultNumIterations; //Number of iterations
        #endregion

        #region Ctor
        public Ocean()
        {
            _cells = new Cell[_numRows, _numColumns];
            _movedCells = new bool[_numRows, _numColumns];
            _direction = new Direction(this);
            OceanViewer = this;
        }
        #endregion

        #region Properties
        public int NumPrey
        {
            get => _numPrey;
            set => _numPrey = value;
        }

        public int NumPredators
        {
            get => _numPredators;
            set => _numPredators = value;
        }

        public int NumObstacles
        {
            get => _numObstacles;
            set => _numObstacles = value;
        }

        public int NumRows
        {
            get => _numRows;
        }

        public int NumColumns
        {
            get => _numColumns;
        }

        public int NumIterations
        {
            get => _numIterations;
            set => _numIterations = value;
        }

        public Direction Direction
        {
            get => _direction;
        }

        public IOceanViewer OceanViewer { get; }

        public Cell this[int x, int y]
        {
            get => _cells[x, y];
            set => _cells[x, y] = value;
        }

        #endregion

        #region Methods
        public Cell GetCellAt(int x, int y) //Returns cell by position
        {
            if (x < 0 || y < 0)
            {
                throw new IndexOutOfRangeException("The index x or y went beyond the boundaries of the playing field!");
            }

            return this[x, y];
        }

        public void ResetSettings()
        {
            for (int i = 0; i < _numRows; i++)
            {
                for (int j = 0; j < _numColumns; j++)
                {
                    _cells[i, j] = null;
                }
            }

            _numObstacles = GameSettings.defaultNumObstacles;
            _numPrey = GameSettings.defaultNumPrey;
            _numPredators = GameSettings.defaultNumPredators;
        }

        public void Run()
        {
            for (int i = 0; i < _numRows; i++)
            {
                for (int j = 0; j < _numColumns; j++)
                {
                    _movedCells[i, j] = false;
                }
            }

            foreach (var cell in _cells)
            {
                if (cell != null)
                {
                    if (!_movedCells[cell.Offset.X, cell.Offset.Y])
                    {
                        cell.Process();
                        _movedCells[cell.Offset.X, cell.Offset.Y] = true;
                    }
                }
            }

            GC.Collect();
            Thread.Sleep(500);
        }
        #endregion
    }
}
