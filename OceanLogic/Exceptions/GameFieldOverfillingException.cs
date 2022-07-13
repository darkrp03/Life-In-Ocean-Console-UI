using System;

namespace OceanLogic.Exceptions
{
    public class GameFieldOverfillingException : Exception
    {
        public GameFieldOverfillingException() : base("The number of game objects is greater than the playing field!")
        {
        }

        public GameFieldOverfillingException(string message) : base(message)
        {
        }

        public GameFieldOverfillingException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
