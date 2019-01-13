using Sirenix.OdinInspector;
using UnityEngine;

namespace Continuous
{
    public class ProceduralPathGenerator : MonoBehaviourSingleton<ProceduralPathGenerator>
    {
        private static ProceduralPathSettings settings;

        [InlineEditor]
        [SerializeField] private ProceduralPathSettings _settings;

        private void Awake()
        {
            settings = _settings;
        }

        public static BarData GetBarData(int score)
        {
            ProceduralZone zone = settings.GetCurrentZone(score);
            float size = RandomExtensions.RandomGaussian(zone.AverageSize, zone.SizeDistribution);
            size = Mathf.Clamp(size, zone.MinSize, zone.MaxSize);
            PickupType pickup = GetPickupType();

            BarData data = new BarData(size: size, powerupType: pickup);
            return data;
        }

        public static float GetCurrentTimeScale(int score)
        {
            return settings.GetCurrentTimeScale(score);
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
}