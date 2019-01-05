using UnityEngine;

namespace Continuous
{
    public static class ProceduralPathGenerator
    {
        private static float averageSize = 6;
        private static float sizeDistribution = 2;
        private static float minSize = 3;

        private static float ShieldProbability => RemoteSettingsValues.ShieldProbability;

        public static BarData GetBarData()
        {
            float size = RandomExtensions.RandomGaussian(averageSize, sizeDistribution);
            size = Mathf.Clamp(size, minSize, int.MaxValue);
            PickupType pickup = GetPickupType();

            BarData data = new BarData(size: size, powerupType: pickup);
            return data;
        }

        private static PickupType GetPickupType()
        {
            PickupType type = PickupType.None;
            float random = Random.Range(0f, 1f);

            if (random < ShieldProbability)
                type = PickupType.Shield;

            return type;
        }
    }
}