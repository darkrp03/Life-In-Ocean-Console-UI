using OceanLogic.Exceptions;
using OceanLogic.GameObjects.AbstractObjects;
using OceanLogic.Interfaces;

namespace OceanLogic.GameObjects
{
    public class Prey : Cell
    {
        #region Fields
        protected int timeToReproduce;
        #endregion

        #region Ctor
        public Prey(Coordinate offset, IOcean ocean) : base(offset, ocean)
        {
            timeToReproduce = GameSettings.defaultTimeToReproduce;
            image = GameSettings.defaultPreyImage;
        }
        #endregion

        #region Methods
        public virtual void Reproduce(Coordinate position) //Creates new Prey in specific coordinate
        {
            try
            {
                ocean.Direction.AssignCellAt(position, new Prey(position, ocean));
            }
            catch (IndexOfGameFieldAbroadException e)
            {
                e.DisplayErrorAndExit();
            }
        }

        public virtual void Move(Coordinate oldPosition, Coordinate newPosition) //Creates new Cell in the old coordinate and creates new Prey in the new coordinate
        {
            Offset = newPosition;

            try
            {
                ocean.Direction.AssignCellAt(oldPosition, null);
                ocean.Direction.AssignCellAt(newPosition, this);
            }
            catch(IndexOfGameFieldAbroadException e)
            {
                e.DisplayErrorAndExit();
            }
        }

        public override void Process()
        {
            Coordinate newPosition = ocean.Direction.GetEmptyNeighborCoord(Offset);

            if (timeToReproduce-- > 0)
            {
                if (newPosition.X != Offset.X || newPosition.Y != Offset.Y)
                {
                    Move(Offset, newPosition);
                }
            }
            else
            {
                if (newPosition.X != Offset.X || newPosition.Y != Offset.Y)
                {
                    Reproduce(newPosition);
                    timeToReproduce = GameSettings.defaultTimeToFeed;
                }
            }
        }
        #endregion
    }
}
