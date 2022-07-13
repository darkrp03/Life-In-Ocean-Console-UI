using OceanLogic.Interfaces;
using System;

namespace OceanLogic.GameObjects
{
    public class Predator : Prey
    {
        #region Fields
        private int _timeToFeed;
        #endregion

        #region Ctor
        public Predator(Coordinate offset, IOcean ocean) : base(offset, ocean)
        {
            _timeToFeed = GameSettings.defaultTimeToFeed;
            _timeToReproduce = GameSettings.defaultTimeToReproduce;
            image = GameSettings.defaultPredatorImage;
        }
        #endregion

        #region Methods
        private void Die()
        {
            try
            {
                ocean.Direction.AssignCellAt(Offset, null);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace: {0}", e.StackTrace);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private void Eat(Coordinate preyNeighbourPosition)
        {
            Move(Offset, preyNeighbourPosition);
        }

        public override void Reproduce(Coordinate position) //Creates new Predators in specific coordinate
        {
            try
            {
                ocean.Direction.AssignCellAt(position, new Predator(position, ocean));
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace: {0}", e.StackTrace);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public override void Move(Coordinate oldPosition, Coordinate newPosition) //Creates new Cell in the old coordinate and creates new Predator in the new coordinate
        {
            Offset = newPosition;

            try
            {
                ocean.Direction.AssignCellAt(oldPosition, null);
                ocean.Direction.AssignCellAt(Offset, this);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace: {0}", e.StackTrace);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public override void Process()
        {
            Coordinate newPosition = ocean.Direction.GetEmptyNeighborCoord(Offset);

            if (_timeToFeed-- > 0)
            {
                if (_timeToReproduce-- > 0)
                {
                    Coordinate preyNeighbourPosition = ocean.Direction.GetPreyNeighborCoord(GameSettings.defaultPreyImage, Offset);

                    if (preyNeighbourPosition.X != Offset.X || preyNeighbourPosition.Y != Offset.Y)
                    {
                        Eat(preyNeighbourPosition);
                        _timeToFeed = GameSettings.defaultTimeToFeed;
                    }
                    else if (newPosition.X != Offset.X || newPosition.Y != Offset.Y)
                    {
                        Move(Offset, newPosition);
                    }
                }
                else
                {
                    if (newPosition.X != Offset.X || newPosition.Y != Offset.Y)
                    {
                        Reproduce(newPosition);
                    }
                }
            }
            else
            {
                Die();
            }
        }
        #endregion
    }
}
