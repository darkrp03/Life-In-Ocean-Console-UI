using OceanLogic.GameObjects.AbstractObjects;

namespace OceanLogic.Interfaces
{
    public interface IOceanViewer
    {
        int NumPrey { get; set; }

        int NumPredators { get; set; }

        int NumObstacles { get; set; }

        int NumRows { get; }

        int NumColumns { get; }

        int NumIterations { get; set; }

        Cell GetCellAt(int x, int y);
    }
}
