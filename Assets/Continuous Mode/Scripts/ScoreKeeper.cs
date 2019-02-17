using System;
using UnityEngine;

namespace Continuous
{
    public class ScoreKeeper : MonoBehaviourSingleton<ScoreKeeper>
    {
        private const string HighScorePlayerPrefsKey = "highscore";

        public static event Action<int> OnScoreChanged;

        public static event Action<int> OnNewHighScore;

        public static bool GotHighScoreThisRound { get; private set; } = false;
        private static int score;
        private static int? highScore = null;

        public static int Score
        {
            get => score;
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
                if (highScore.HasValue == false)
                {
                    highScore = LoadHighScore();
                }
                return highScore.Value;
            }
            private set
            {
                highScore = value;
                SaveHighScore();
                if (!GotHighScoreThisRound)
                {
                    //Debug.Log("New High Score This Round!");
                    GotHighScoreThisRound = true;
                    OnNewHighScore?.Invoke(highScore.Value);
                }
            }
        }

        public static bool IsNextPointHighScore()
        {
            if (GotHighScoreThisRound || highScore <= 0)
                return false;

            return score == highScore;
        }

        private static int LoadHighScore()
        {
            return PlayerPrefs.GetInt(HighScorePlayerPrefsKey, 0);
        }

        private static void SaveHighScore()
        {
            PlayerPrefs.SetInt(HighScorePlayerPrefsKey, HighScore);
            PlayerPrefs.Save();
        }

        private void Awake()
        {
            highScore = LoadHighScore();
            ResetScore();
        }

        private void OnEnable()
        {
            PlayerLookAhead.ScorePoint += ScorePoint;

            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
        }

        private void OnDisable()
        {
            PlayerLookAhead.ScorePoint -= ScorePoint;

            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
        }

        private static void OnGameStartOrRestart()
        {
            GotHighScoreThisRound = false;
            ResetScore();
        }

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