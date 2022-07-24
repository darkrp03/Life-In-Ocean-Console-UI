using OceanLogic.GameObjects.AbstractObjects;
using OceanLogic.Interfaces;
using System;

namespace OceanLogic.GameObjects
{
    public class KillerWhale : Cell
    {

        public KillerWhale(Coordinate offset, IOcean ocean) : base(offset, ocean)
        {
            image = GameSettings.defaultKillerWhaleImage;
        }

        private void Eat(Coordinate preyNeighbourPosition)
        {
            Move(Offset, preyNeighbourPosition);
        }

        private void Move(Coordinate oldPosition, Coordinate newPosition) //Creates new Cell in the old coordinate and creates new Prey in the new coordinate
        {
            Offset = newPosition;

            try
            {
                ocean.Direction.AssignCellAt(oldPosition, null);
                ocean.Direction.AssignCellAt(newPosition, this);
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
            var newPosition = ocean.Direction.GetEmptyNeighborCoord(Offset);

            var preyNeighbourPosition = ocean.Direction.GetEntityNeighborCoord(GameSettings.defaultPreyImage, Offset);

            if (preyNeighbourPosition.X != Offset.X || preyNeighbourPosition.Y != Offset.Y)
            {
                Eat(preyNeighbourPosition);
            }

            var predatorNeighbourPosition = ocean.Direction.GetEntityNeighborCoord(GameSettings.defaultPredatorImage, Offset);

            if (predatorNeighbourPosition.X != Offset.X || predatorNeighbourPosition.Y != Offset.Y)
            {
                Eat(predatorNeighbourPosition);
            }

            if (newPosition.X != Offset.X || newPosition.Y != Offset.Y)
            {
                Move(Offset, newPosition);
            }
        }
    }
}