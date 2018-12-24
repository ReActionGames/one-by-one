using System;
using System.Linq;
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
            PowerupType powerup = GetPowerupType();

            BarData data = new BarData(size: size, powerupType: powerup);
            return data;
        }

        private static PowerupType GetPowerupType()
        {
            //int index = Random
            return PowerupType.Shield;
            //return (PowerupType)UnityEngine.Random.Range(-1, (int)Enum.GetValues(typeof(PowerupType)).Cast<PowerupType>().Max());
        }
    }
}