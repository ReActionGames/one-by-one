using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class AnalyticsManager : MonoBehaviourSingleton<AnalyticsManager>
    {
        //public static event Action OnSendGameEndData;
        //public static event Action OnGameStartOrRestart;
        //public static event Action OnAddBarTimeToList;
        //public static event Action OnSendProjectileFiredData;

        public static int GamesPlayedInLifetime
        {
            get => PlayerPrefs.GetInt("total-games-played", 0);
            private set => PlayerPrefs.SetInt("total-games-played", value);
        }

        public static float BarTimesAverage { get => BarTimes.GetAverage(); }
        private static List<float> BarTimes = new List<float>();
        public static int GamesPlayedThisSession { get; private set; }

        public const string EventBarPlaced = "bar-placed";
        public const string EventGameEnd = "game-end";
        public const string EventProjectileFired = "projectile-fired";
        public const string EventSessionEnd = "session-end";

        public const string ParamAverageBarTime = "average-bar-time";
        public const string ParamBarTime = "bar-time";
        public const string ParamGamesPlayed = "games-played";
        public const string ParamHighScore = "got-high-score";
        public const string ParamScore = "score";
        public const string ParamTimeScale = "time-scale-on-end";

        public const string UserPropGamesPlayed = "lifetime-games-played";

        private void OnEnable()
        {
            GameManager.GameEnd += SendOnGameEndData;
            GameManager.GameStartOrRestart += GameStartOrRestart;
            PathController.BarPlacedWithTime += AddBarTimeToList;
            Player.OnProjectileFired += SendProjectileFiredData;
        }
        private void OnDisable()
        {
            GameManager.GameEnd -= SendOnGameEndData;
            GameManager.GameStartOrRestart -= GameStartOrRestart;
            PathController.BarPlacedWithTime -= AddBarTimeToList;
            Player.OnProjectileFired -= SendProjectileFiredData;
        }


        private void Start()
        {
            GamesPlayedThisSession = 0;
        }

        private void SendOnGameEndData()
        {
            GamesPlayedThisSession++;
            GamesPlayedInLifetime += 1;
        }

        private void SendProjectileFiredData(bool obj)
        {
            //throw new NotImplementedException();
        }

        private void AddBarTimeToList(float time)
        {
            BarTimes.Add(time);
        }

        private void GameStartOrRestart()
        {
            BarTimes.Clear();
        }
    }
}