using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    public class ScoreKeeper : MonoBehaviour
    {
        private const string HighScorePlayerPrefsKey = "highscore";

        public Action OnScoreChanged;
        public Action OnNewHighScore;

        [ReadOnly]
        [SerializeField] private int score;

        //[ReadOnly]
        [OnValueChanged("ChangeHighScore")]
        [SerializeField] private int highScore = -1;

        private bool gotHighScoreThisRound = false;

        private void ChangeHighScore()
        {
            PlayerPrefs.SetInt(HighScorePlayerPrefsKey, highScore);
        }

        public int Score
        {
            get
            {
                return score;
            }
            private set
            {
                score = value;
                OnScoreChanged?.Invoke();
            }
        }

        public int HighScore
        {
            get
            {
                if (highScore < 0)
                {
                    highScore = PlayerPrefs.GetInt(HighScorePlayerPrefsKey, 0);
                }
                return highScore;
            }
            private set
            {
                highScore = value;
                SaveHighScore();
                if (!gotHighScoreThisRound)
                {
                    DebugManager.Log("New High Score This Round!");
                    gotHighScoreThisRound = true;
                    OnNewHighScore?.Invoke();
                }
            }
        }

        private void SaveHighScore()
        {
            PlayerPrefs.SetInt(HighScorePlayerPrefsKey, HighScore);
            PlayerPrefs.Save();
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.ScorePoint, ScorePoint);
            EventManager.StartListening(EventNames.GameStart, OnGameStartOrRestart);
            EventManager.StartListening(EventNames.GameRestart, OnGameStartOrRestart);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventNames.ScorePoint, ScorePoint);
            EventManager.StopListening(EventNames.GameStart, OnGameStartOrRestart);
            EventManager.StopListening(EventNames.GameRestart, OnGameStartOrRestart);
        }

        private void OnGameStartOrRestart(Message message)
        {
            gotHighScoreThisRound = false;
            ResetScore();
        }


        private void Start()
        {
            ResetScore();
        }

        private void ScorePoint(Message message)
        {
            IncrementScore();
        }

        private void IncrementScore()
        {
            Score++;
            if (Score > HighScore)
            {
                HighScore = Score;
            }
        }

        private void ResetScore()
        {
            Score = 0;
        }
    }
}