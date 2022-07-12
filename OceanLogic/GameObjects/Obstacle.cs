using OceanLogic.GameObjects.AbstractObjects;

namespace OceanLogic.GameObjects
{
    public class Obstacle : Cell
    {
        public Obstacle()
        {
            image = GameSettings.defaultObstacleImage;
        }

        public override void Process()
        {
        }
    }
}
