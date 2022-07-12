using OceanLogic.GameObjects.AbstractObjects;

namespace OceanLogic.Interfaces
{
    public interface IOcean
    {
        IOceanViewer OceanViewer { get; }

        Direction Direction { get; }

        Cell this[int x, int y] { get; set; }
    }
}
