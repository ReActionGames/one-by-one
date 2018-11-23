using System;
using UnityEngine;

namespace Continuous
{
    public static class ProceduralPathGenerator
    {
        private static float averageSize = 6;
        private static float sizeDistribution = 2;
        private static float minSize = 3;

        public static BarData GetBarData()
        {
            float size = RandomExtensions.RandomGaussian(averageSize, sizeDistribution);
            size = Mathf.Clamp(size, minSize, int.MaxValue);

            var data = new BarData(size);
            return data;
        }
    }
}