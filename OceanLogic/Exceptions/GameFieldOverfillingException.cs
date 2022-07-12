namespace OceanLogic.Exceptions
{
     public class GameFieldOverfillingException : OceanLogicException
     {
        public GameFieldOverfillingException() : base("The number of game objects is greater than the playing field!")
        {

        }

        public GameFieldOverfillingException(string message) : base(message)
        {

        }
    }
}
