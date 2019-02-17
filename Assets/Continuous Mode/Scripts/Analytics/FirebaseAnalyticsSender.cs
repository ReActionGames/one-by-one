using Firebase.Analytics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class FirebaseAnalyticsSender : MonoBehaviour
    {
        [SerializeField] private bool sendAnalytics = true;
        
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
        
        private void OnApplicationQuit()
        {
            SendApplicationQuitData();
        }
        
        private void SendProjectileFiredData(bool success)
        {
            if (sendAnalytics == false || success == false) return;

            FirebaseAnalytics.LogEvent(AnalyticsManager.EventProjectileFired);
        }

        private void SendOnGameEndData()
        {
            if (sendAnalytics == false) return;


            string gotHighScore = ScoreKeeper.GotHighScoreThisRound == true ? "true" : "false";
            Parameter paramGotHighScore = new Parameter(AnalyticsManager.ParamHighScore, gotHighScore);

            int score = ScoreKeeper.Score;
            Parameter paramScore = new Parameter(AnalyticsManager.ParamScore, score);

            double timeScaleOnEnd = FindObjectOfType<TimeController>().CurrentTime;
            Parameter paramTimeScale = new Parameter(AnalyticsManager.ParamTimeScale, timeScaleOnEnd);

            double averageBarTime = AnalyticsManager.BarTimesAverage;
            Parameter paramAverageBarTime = new Parameter(AnalyticsManager.ParamAverageBarTime, averageBarTime);

            FirebaseAnalytics.LogEvent(AnalyticsManager.EventGameEnd, paramGotHighScore, paramScore, paramTimeScale, paramAverageBarTime);
            FirebaseAnalytics.SetUserProperty(AnalyticsManager.UserPropGamesPlayed, AnalyticsManager.GamesPlayedInLifetime.ToString());
        }

        private void SendApplicationQuitData()
        {
            if (sendAnalytics == false) return;
            FirebaseAnalytics.LogEvent(AnalyticsManager.EventSessionEnd, AnalyticsManager.ParamGamesPlayed, AnalyticsManager.GamesPlayedThisSession);
        }

        private void AddBarTimeToList(float time)
        {
            if (sendAnalytics == false) return;
            FirebaseAnalytics.LogEvent(AnalyticsManager.EventBarPlaced, AnalyticsManager.ParamBarTime, time);
        }
    }
}