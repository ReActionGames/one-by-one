using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public static class ProceduralPathGenerator
    {
        private static ProceduralPathSettings settings;

        public static BarData GetBarData()
        {
            float size = RandomExtensions.RandomGaussian(settings[0].AverageSize, settings[0].SizeDistribution);
            size = Mathf.Clamp(size, settings[0].MinSize, int.MaxValue);
            PickupType pickup = GetPickupType();

            BarData data = new BarData(size: size, powerupType: pickup);
            return data;
        }

        private static PickupType GetPickupType()
        {
            PickupType type = PickupType.None;
            float random = Random.Range(0f, 1f);

            if (random < settings.ShieldProbability)
                type = PickupType.Shield;
            //if (ScoreKeeper.IsNextPointHighScore())
            //{
            //    type = PickupType.HighScore;
            //}

            return type;
        }
    }

    [GlobalConfig("Continuous Mode/Resources", UseAsset = true)]
    public class ProceduralPathSettings : GlobalConfig<ProceduralPathSettings>
    {
        public float ShieldProbability => RemoteSettingsValues.ShieldProbability;

        public List<ProceduralZone> Zones { get; private set; }

        public ProceduralZone this[int index]
        {
            get { return Zones[index]; }
        }

        //[Button(ButtonSizes.Medium)]
        //private void AddZone()
        //{

        //}
    }
}