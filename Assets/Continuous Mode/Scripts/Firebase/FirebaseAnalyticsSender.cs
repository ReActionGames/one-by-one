using Firebase.Analytics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class FirebaseAnalyticsSender : MonoBehaviour
    {
        [SerializeField] private bool sendAnalytics = true;

        private const string EventBarPlaced = "bar-placed";
        private const string EventGameEnd = "game-end";
        private const string EventProjectileFired = "projectile-fired";
        private const string EventSessionEnd = "session-end";

        private const string ParamAverageBarTime = "average-bar-time";
        private const string ParamBarTime = "bar-time";
        private const string ParamGamesPlayed = "games-played";
        private const string ParamHighScore = "got-high-score";
        private const string ParamScore = "score";
        private const string ParamTimeScale = "time-scale-on-end";

        private const string UserPropGamesPlayed = "lifetime-games-played";

        private List<float> barTimes = new List<float>();
        private int gamesPlayedThisSession;

        private int gamesPlayedInLifetime
        {
            get => PlayerPrefs.GetInt("total-games-played", 0);
            set => PlayerPrefs.SetInt("total-games-played", value);
        }

        private void OnEnable()
        {
            GameManager.GameEnd += SendOnGameEndData;
            GameManager.GameStartOrRestart += OnGameStartOrRestart;
            PathController.BarPlacedWithTime += AddBarTimeToList;
            Player.OnProjectileFired += SendProjectileFiredData;
        }

        private void OnDisable()
        {
            GameManager.GameEnd -= SendOnGameEndData;
            GameManager.GameStartOrRestart -= OnGameStartOrRestart;
            PathController.BarPlacedWithTime -= AddBarTimeToList;
            Player.OnProjectileFired -= SendProjectileFiredData;
        }

        private void Start()
        {
            gamesPlayedThisSession = 0;
        }

        private void OnApplicationQuit()
        {
            SendApplicationQuitData();
        }

        private void OnGameStartOrRestart()
        {
            barTimes.Clear();
        }

        private void SendProjectileFiredData(bool success)
        {
            if (sendAnalytics == false || success == false) return;

            FirebaseAnalytics.LogEvent(EventProjectileFired);
        }

        private void SendOnGameEndData()
        {
            if (sendAnalytics == false) return;

            gamesPlayedThisSession++;
            gamesPlayedInLifetime += 1;

            string gotHighScore = ScoreKeeper.GotHighScoreThisRound == true ? "true" : "false";
            Parameter paramGotHighScore = new Parameter(ParamHighScore, gotHighScore);

            int score = ScoreKeeper.Score;
            Parameter paramScore = new Parameter(ParamScore, score);

            double timeScaleOnEnd = FindObjectOfType<TimeController>().CurrentTime;
            Parameter paramTimeScale = new Parameter(ParamTimeScale, timeScaleOnEnd);

            double averageBarTime = barTimes.GetAverage();
            Parameter paramAverageBarTime = new Parameter(ParamAverageBarTime, averageBarTime);

            FirebaseAnalytics.LogEvent(EventGameEnd, paramGotHighScore, paramScore, paramTimeScale, paramAverageBarTime);
            FirebaseAnalytics.SetUserProperty(UserPropGamesPlayed, gamesPlayedInLifetime.ToString());
        }

        private void SendApplicationQuitData()
        {
            if (sendAnalytics == false) return;
            FirebaseAnalytics.LogEvent(EventSessionEnd, ParamGamesPlayed, gamesPlayedThisSession);
        }

        private void AddBarTimeToList(float time)
        {
            barTimes.Add(time);
            if (sendAnalytics == false) return;
            FirebaseAnalytics.LogEvent(EventBarPlaced, ParamBarTime, time);
        }
    }
}