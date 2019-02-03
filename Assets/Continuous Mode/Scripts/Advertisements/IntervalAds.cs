using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Continuous
{
    public class IntervalAds : MonoBehaviour
    {
        private const string GamesPerAdKey = "games-per-ad";
        private string NoAdsKey = "no-ads";
        private const string NoAds_IAP_ID = "com.reactiongames.onebyone.no_ads";

        [SerializeField] private int defaultGamesPerAd = 3;
        [SerializeField] private float delay = 0.5f;
        [ReadOnly]
        [SerializeField] private int gamesUntilNextAd;

        private UINoAdsButton[] noAdsButtons;
        private int gamesPerAd;

        private bool noAds
        {
            get => PlayerPrefs.GetInt(NoAdsKey, 0) == 1;
            set => PlayerPrefs.SetInt(NoAdsKey, value == true ? 1 : 0);
        }

        private void Awake()
        {
            LoadRemoteSettings();
            gamesUntilNextAd = gamesPerAd;
            noAdsButtons = FindObjectsOfType<UINoAdsButton>();
            UpdateNoAdsButtonActivation(noAds);
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

        private void UpdateNoAdsButtonActivation(bool noAds)
        {
            foreach (UINoAdsButton button in noAdsButtons)
            {
                button.UpdateActivation(noAds);
            }
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
            if (noAds == false && AdvertisementUtility.ShowAds)
            {
                yield return new WaitForSeconds(delay);
                AdvertisementUtility.ShowVideoAd();
            }
        }

        public void NoAdsPurchaseComplete(Product product)
        {
            if (product.definition.id.Equals(NoAds_IAP_ID) == false) return;

            noAds = true;
            UpdateNoAdsButtonActivation(noAds);
        }
    }
}