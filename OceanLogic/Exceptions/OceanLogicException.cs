using System;

namespace OceanLogic.Exceptions
{
    public class OceanLogicException : Exception
    {
        public OceanLogicException()
        {
        }

        public OceanLogicException(string message) : base(message)
        {
        }

        public void DisplayErrorAndExit()
        {
            Console.WriteLine("Error: {0}", Message);
            Console.WriteLine("Stack trace: {0}", StackTrace);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
