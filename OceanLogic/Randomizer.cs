using System;

namespace OceanLogic
{
    static class Randomizer
    {
        #region Fields
        private static Random random = new Random();
        #endregion

        #region Methods
        public static int GetRandomNumber(int minValue, int maxValue) => random.Next(minValue, maxValue);
        #endregion
    }
}
