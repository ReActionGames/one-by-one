using Firebase.Database;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Continuous
{
    public class ProceduralPathGenerator : MonoBehaviourSingleton<ProceduralPathGenerator>
    {
        private const string firebaseNode = "path-settings";

        private static ProceduralPathSettings settings;

        [InlineEditor]
        [SerializeField] private ProceduralPathSettings _settings;

        private void Awake()
        {
            settings = _settings;
            LoadSettingsFromFirebase();
        }

        private void OnEnable()
        {
            FirebaseDatabaseUtility.SubscribeToValueChanged(firebaseNode, HandleValueChanged);
        }

        private void OnDisable()
        {
            FirebaseDatabaseUtility.UnsubscribeToValueChanged(firebaseNode, HandleValueChanged);
        }

        public static BarData GetBarData(int score)
        {
            ProceduralZone zone = settings.GetCurrentZone(score);
            float size = RandomExtensions.RandomGaussian(zone.AverageSize, zone.SizeDistribution);
            size = Mathf.Clamp(size, zone.MinSize, zone.MaxSize);
            PickupType pickup = GetPickupType();
            BarType type = GetBarType(zone.BarTypes);

            BarData data = new BarData(size: size, powerupType: pickup);
            return data;
        }

        private static BarType GetBarType(BarType types)
        {
            BarType type = BarType.Normal;

            float random = Random.Range(0f, 1f);
            //if(random > 0.5f && types.IsSet(BarType.Double))
            //{
            //    type = BarType.Double;
            //}

            return type;
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

        [Button]
        private static void LoadSettingsFromFirebase()
        {
            FirebaseDatabaseUtility.GetDataAsJson(firebaseNode, ConvertJsonToPathSettings);
        }

        private static void ConvertJsonToPathSettings(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("Retrieved data was null or empty! Aborting...");
                return;
            }

            JsonUtility.FromJsonOverwrite(json, settings);
        }

        private void HandleValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }

            string json = args.Snapshot.GetRawJsonValue();
            ConvertJsonToPathSettings(json);
            Debug.Log("Updated Settings");
        }

        [Button]
        private static void SaveSettingsToFirebase()
        {
            string json = JsonUtility.ToJson(settings);

            FirebaseDatabaseUtility.SaveData(firebaseNode, json);
        }
    }
}