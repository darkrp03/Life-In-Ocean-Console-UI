using OceanLogic.Interfaces;

namespace OceanLogic.GameObjects
{
    public class Predator : Prey
    {
        #region Fields
        private int timeToFeed;
        #endregion

        #region Ctor
        public Predator(Coordinate offset, IOcean ocean) : base(offset, ocean)
        {
            timeToFeed = GameSettings.defaultTimeToFeed;
            timeToReproduce = GameSettings.defaultTimeToReproduce;
            image = GameSettings.defaultPredatorImage;
        }
        #endregion

        #region Methods
        private void Eat(Coordinate preyNeighbourPosition)
        {
            Move(Offset, preyNeighbourPosition);
        }

        public override void Reproduce(Coordinate position) //Creates new Predators in specific coordinate
        {
            ocean.Direction.AssignCellAt(position, new Predator(position, ocean));
        }

        public override void Move(Coordinate oldPosition, Coordinate newPosition) //Creates new Cell in the old coordinate and creates new Predator in the new coordinate
        {
            Offset = newPosition;
            ocean.Direction.AssignCellAt(oldPosition, null);
            ocean.Direction.AssignCellAt(Offset, this);
        }

        public override void Process()
        {
            Coordinate newPosition = ocean.Direction.GetEmptyNeighborCoord(Offset);

            if (timeToFeed-- > 0)
            {
                if (timeToReproduce-- > 0)
                {
                    Coordinate preyNeighbourPosition = ocean.Direction.GetPreyNeighborCoord(GameSettings.defaultPreyImage, Offset);

                    if (preyNeighbourPosition.X != Offset.X || preyNeighbourPosition.Y != Offset.Y)
                    {
                        Eat(preyNeighbourPosition);
                        timeToFeed = GameSettings.defaultTimeToFeed;
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
                ocean.Direction.AssignCellAt(Offset, null);
            }
        }
        #endregion
    }
}
