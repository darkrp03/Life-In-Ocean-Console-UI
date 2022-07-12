using OceanLogic.Interfaces;

namespace OceanLogic.GameObjects.AbstractObjects
{
    public abstract class Cell
    {
        #region Fields
        private Coordinate offset;
        protected readonly IOcean ocean;
        protected char image;
        #endregion

        #region Ctors
        public Cell() { }

        public Cell(Coordinate offset, IOcean ocean)
        {
            this.offset = offset;
            this.ocean = ocean;
        }
        #endregion

        #region Properties
        public Coordinate Offset
        {
            get => offset;
            set => offset = value;
        }

        public char Image
        {
            get => image;
        }
        #endregion

        #region Methods
        public abstract void Process();
        #endregion
    }
}
