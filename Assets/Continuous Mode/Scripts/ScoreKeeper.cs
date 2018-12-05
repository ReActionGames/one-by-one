using System;
using UnityEngine;

namespace Continuous
{
    public static class ScoreKeeper
    {
        private const string HighScorePlayerPrefsKey = "highscore";

        public static event Action<int> OnScoreChanged;

        public static event Action<int> OnNewHighScore;

        //public static int HighScore { get; private set; }
        //public static int Score { get; private set; }

        //[ReadOnly]
        //[SerializeField] private int score;

        ////[ReadOnly]
        //[OnValueChanged("ChangeHighScore")]
        //[SerializeField] private int highScore = -1;

        private static bool gotHighScoreThisRound = false;

        //private void ChangeHighScore()
        //{
        //    PlayerPrefs.SetInt(HighScorePlayerPrefsKey, highScore);
        //}

        private static int score, highScore = -1;

        public static int Score
        {
            get
            {
                return score;
            }
            private set
            {
                score = value;
                OnScoreChanged?.Invoke(score);
            }
        }

        public static int HighScore
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
                    //Debug.Log("New High Score This Round!");
                    gotHighScoreThisRound = true;
                    OnNewHighScore?.Invoke(highScore);
                }
            }
        }

        private static void SaveHighScore()
        {
            PlayerPrefs.SetInt(HighScorePlayerPrefsKey, HighScore);
            PlayerPrefs.Save();
        }

        static ScoreKeeper()
        {
            Player.ScorePoint += ScorePoint;

            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
        }

        //private void OnEnable()
        //{
        //}

        //private void OnDisable()
        //{
        //    Player.ScorePoint -= ScorePoint;

        //    GameManager.GameStart -= OnGameStartOrRestart;
        //    GameManager.GameRestart -= OnGameStartOrRestart;
        //}

        private static void OnGameStartOrRestart()
        {
            gotHighScoreThisRound = false;
            ResetScore();
        }

        //private void Start()
        //{
        //    ResetScore();
        //}

        private static void ScorePoint()
        {
            IncrementScore();
        }

        private static void IncrementScore()
        {
            Score++;
            if (Score > HighScore)
            {
                HighScore = Score;
            }
        }

        private static void ResetScore()
        {
            Score = 0;
        }
    }
}