using OceanLogic.Enums;
using OceanLogic.GameObjects.AbstractObjects;
using OceanLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace OceanLogic
{
    public class Direction
    {
        #region Fields
        private readonly IOcean _ocean;
        #endregion

        #region Ctor
        public Direction(Ocean ocean)
        {
            _ocean = ocean;
        }
        #endregion

        #region Methods
        private Cell GetNorthCell(Coordinate position) //Returns western neighbour cell
        {
            if (position.X - 1 < 0)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X - 1, position.Y);
        }

        private Cell GetSouthCell(Coordinate position) //Returns east neighbour cell
        {
            if (position.X + 1 >= _ocean.OceanViewer.NumRows)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X + 1, position.Y);
        }

        private Cell GetWesternCell(Coordinate position) //Returns south neighbour cell
        {
            if (position.Y - 1 < 0)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X, position.Y - 1);
        }

        private Cell GetEastCell(Coordinate position) //Returns north neighbour cell
        {
            if (position.Y + 1 >= _ocean.OceanViewer.NumColumns)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X, position.Y + 1);
        }

        private Cell GetNorthEastCell(Coordinate position) //Returns north-east neighbour cell
        {
            if (position.X - 1 < 0 || position.Y + 1 >= _ocean.OceanViewer.NumColumns)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X - 1, position.Y + 1);
        }

        private Cell GetNorthWesternCell(Coordinate position) //Returns north-western neighbour cell
        {
            if (position.X - 1 < 0 || position.Y - 1 < 0)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X - 1, position.Y - 1);
        }

        private Cell GetSouthEastCell(Coordinate position) //Returns south-east neighbour cell
        {
            if (position.X + 1 >= _ocean.OceanViewer.NumRows || position.Y + 1 >= _ocean.OceanViewer.NumColumns)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X + 1, position.Y + 1);
        }

        private Cell GetSouthWesternCell(Coordinate position) //Returns south-western neighbout cell
        {
            if (position.X + 1 >= _ocean.OceanViewer.NumRows || position.Y - 1 < 0)
            {
                return _ocean.OceanViewer.GetCellAt(position.X, position.Y);
            }

            return _ocean.OceanViewer.GetCellAt(position.X + 1, position.Y - 1);
        }

        private List<Cell> GetNeighbourCells(Coordinate position) //Returns a list of neighboring cells
        {
            var cells = new List<Cell>();

            cells.Add(GetNorthCell(position));
            cells.Add(GetSouthCell(position));
            cells.Add(GetEastCell(position));
            cells.Add(GetWesternCell(position));
            cells.Add(GetNorthEastCell(position));
            cells.Add(GetNorthWesternCell(position));
            cells.Add(GetSouthEastCell(position));
            cells.Add(GetSouthWesternCell(position));

            return cells;
        }

        public Coordinate GetEmptyNeighborCoord(Coordinate position) //Returns empty neighbor coordinate 
        {
            var coordinates = new List<Coordinate>();
            var cells = GetNeighbourCells(position);

            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i] == null)
                {
                    switch (i)
                    {
                        case (int)Side.North:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X - 1,
                                Y = position.Y
                            });
                            break;
                        case (int)Side.South:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X + 1,
                                Y = position.Y
                            });
                            break;
                        case (int)Side.East:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X,
                                Y = position.Y + 1
                            });
                            break;
                        case (int)Side.Western:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X,
                                Y = position.Y - 1
                            });
                            break;
                        case (int)Side.NorthEast:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X - 1,
                                Y = position.Y + 1
                            });
                            break;
                        case (int)Side.NorthWestern:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X - 1,
                                Y = position.Y - 1
                            });
                            break;
                        case (int)Side.SouthEast:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X + 1,
                                Y = position.Y + 1
                            });
                            break;
                        case (int)Side.SouthWestern:
                            coordinates.Add(new Coordinate
                            {
                                X = position.X + 1,
                                Y = position.Y - 1
                            });
                            break;
                    }
                }
            }

            if (coordinates.Count == 0)
                return position;

            return coordinates[Randomizer.GetRandomNumber(0, coordinates.Count)];
        }

        public Coordinate GetEntityNeighborCoord(char image, Coordinate position) //Returns prey neighbor coordinate
        {
            var coordinates = new List<Coordinate>();
            var cells = GetNeighbourCells(position);

            foreach (var cell in cells)
            {
                if (cell != null && cell.Image == image)
                {
                    coordinates.Add(cell.Offset);
                }
            }

            if (coordinates.Count == 0)
                return position;

            return coordinates[Randomizer.GetRandomNumber(0, coordinates.Count)];
        }

        public Coordinate GetNullPosition() //Returns empty cell
        {
            bool isNull = false;
            Coordinate position = new Coordinate();

            while (!isNull)
            {
                int currentRowPosition = Randomizer.GetRandomNumber(0, _ocean.OceanViewer.NumRows - 1);
                int currentColPosition = Randomizer.GetRandomNumber(0, _ocean.OceanViewer.NumColumns - 1);

                if (_ocean.OceanViewer.GetCellAt(currentRowPosition, currentColPosition) == null)
                {
                    position.X = currentRowPosition;
                    position.Y = currentColPosition;

                    isNull = true;
                }
            }

            return position;
        }

        public void AssignCellAt(Coordinate position, Cell cell) //Changes the value of cell
        {
            if (position.X < 0 || position.Y < 0)
            {
                throw new IndexOutOfRangeException("The index x or y went beyond the boundaries of the playing field!");
            }

            _ocean[position.X, position.Y] = cell;
        }
        #endregion
    }
}
