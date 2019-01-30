using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Continuous
{
    public class AdvertisementUtility : MonoBehaviour
    {
        private const string VideoAdPlacementID = "video";
        private const string ShowAdsKey = "show-ads";

        [SerializeField] private string gameID = "3022194";
        [SerializeField] private bool testMode;

        public static bool ShowAds
        {
            get => RemoteSettings.GetBool(ShowAdsKey);
            private set => ShowAds = value;
        }

        private void Awake()
        {
            Advertisement.Initialize(gameID, testMode);
            ShowAds = RemoteSettings.GetBool(ShowAdsKey);
        }

        public static void ShowVideoAd()
        {
            if (ShowAds == false) return;

            if (Advertisement.isInitialized == false) return;
        
            if (Advertisement.IsReady(VideoAdPlacementID))
            {
                Advertisement.Show(VideoAdPlacementID);
            }
        }

        public static void ShowVideoAd(Action<ShowResult> onCompleted)
        {
            if (ShowAds == false) return;

            if (Advertisement.isInitialized == false) return;

            if (Advertisement.IsReady(VideoAdPlacementID))
            {
                ShowOptions options = new ShowOptions { resultCallback = onCompleted };
                Advertisement.Show(VideoAdPlacementID, options);
            }
        }
    }
}