using System;

namespace OceanLogic.Exceptions
{
    public class OceanLogicException : Exception
    {
        public OceanLogicException() : base("Ocean business logic exception")
        {
        }

        public OceanLogicException(string message) : base(message)
        {
        }
    }
}
