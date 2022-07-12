namespace OceanLogic.Exceptions
{
    public class IndexOfGameFieldAbroadException : OceanLogicException
    {
        public IndexOfGameFieldAbroadException() : base("The index went beyond the boundaries of the playing field!")
        {
        }

        public IndexOfGameFieldAbroadException(string message) : base(message)
        {
        }
    }
}
