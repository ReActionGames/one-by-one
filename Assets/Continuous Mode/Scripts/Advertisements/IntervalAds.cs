using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Continuous
{
    public class IntervalAds : MonoBehaviour
    {
        private const string GamesPerAdKey = "games-per-ad";

        private static int gamesPerAd;

        [SerializeField] private int defaultGamesPerAd = 3;
        [SerializeField] private float delay = 0.5f;

        [ReadOnly]
        private int gamesUntilNextAd;

        private void Awake()
        {
            gamesUntilNextAd = gamesPerAd;
        }

        private void OnEnable()
        {
            GameManager.GameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.GameEnd -= OnGameEnd;
        }

        private static void LoadRemoteSettings()
        {
            gamesPerAd = RemoteSettings.GetInt(GamesPerAdKey);
            RemoteSettings.Updated += UpdateRemoteSettings;
        }

        private static void UpdateRemoteSettings()
        {
            gamesPerAd = RemoteSettings.GetInt(GamesPerAdKey);
        }

        private void OnGameEnd()
        {
            gamesUntilNextAd--;
            if (gamesUntilNextAd <= 0)
            {
                StartCoroutine(ShowAd());
                gamesUntilNextAd = gamesPerAd;
            }
        }

        private IEnumerator ShowAd()
        {
            if (AdvertisementUtility.ShowAds)
            {
                yield return new WaitForSeconds(delay);
                AdvertisementUtility.ShowVideoAd();
            }
        }
    }
}