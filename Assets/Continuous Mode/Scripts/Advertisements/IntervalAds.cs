using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Continuous
{
    public class IntervalAds : MonoBehaviour
    {
        private const string GamesPerAdKey = "games-per-ad";


        [SerializeField] private int defaultGamesPerAd = 3;
        [SerializeField] private float delay = 0.5f;

        [ReadOnly]
        private int gamesUntilNextAd;

        private int gamesPerAd;

        private void Awake()
        {
            LoadRemoteSettings();
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

        private void LoadRemoteSettings()
        {
            gamesPerAd = RemoteSettings.GetInt(GamesPerAdKey, defaultGamesPerAd);
            RemoteSettings.Updated += UpdateRemoteSettings;
        }

        private void UpdateRemoteSettings()
        {
            gamesPerAd = RemoteSettings.GetInt(GamesPerAdKey, defaultGamesPerAd);
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