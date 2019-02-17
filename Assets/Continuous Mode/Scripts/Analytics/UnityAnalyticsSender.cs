using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Continuous
{
    public class UnityAnalyticsSender : MonoBehaviour
    {
        [SerializeField] private bool sendAnalytics = true;
        [SerializeField] private bool debugMode = false;

        private void OnEnable()
        {
            GameManager.GameEnd += SendOnGameEndData;
            PathController.BarPlacedWithTime += AddBarTimeToList;
            Player.OnProjectileFired += SendProjectileFiredData;
        }

        private void OnDisable()
        {
            GameManager.GameEnd -= SendOnGameEndData;
            PathController.BarPlacedWithTime -= AddBarTimeToList;
            Player.OnProjectileFired -= SendProjectileFiredData;
        }

        private void Awake()
        {
            AnalyticsEvent.debugMode = debugMode;
        }

        private void OnApplicationQuit()
        {
            SendApplicationQuitData();
        }

        private void SendApplicationQuitData()
        {
            if (sendAnalytics == false) return;
            AnalyticsEvent.Custom(AnalyticsManager.EventSessionEnd, new Dictionary<string, object>()
            {
                { AnalyticsManager.ParamGamesPlayed, AnalyticsManager.GamesPlayedThisSession }
            });
        }

        private void SendProjectileFiredData(bool success)
        {
            if (sendAnalytics == false || success == false) return;
            AnalyticsEvent.Custom(AnalyticsManager.EventProjectileFired);
        }

        private void AddBarTimeToList(float time)
        {
            if (sendAnalytics == false) return;
            AnalyticsEvent.Custom(AnalyticsManager.EventBarPlaced, new Dictionary<string, object>()
            {
                {AnalyticsManager.ParamBarTime, time }
            });
        }

        private void SendOnGameEndData()
        {
            if (sendAnalytics == false) return;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {AnalyticsManager.ParamHighScore, ScoreKeeper.GotHighScoreThisRound },
                {AnalyticsManager.ParamScore, ScoreKeeper.Score },
                {AnalyticsManager.ParamTimeScale, FindObjectOfType<TimeController>().CurrentTime },
                {AnalyticsManager.ParamAverageBarTime, AnalyticsManager.BarTimesAverage },
                {AnalyticsManager.UserPropGamesPlayed, AnalyticsManager.GamesPlayedInLifetime }
            };

            AnalyticsEvent.GameOver(eventData: data);
        }
    }
}