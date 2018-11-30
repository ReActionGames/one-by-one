using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    public class ScoreKeeper : MonoBehaviour
    {
        private const string HighScorePlayerPrefsKey = "highscore";

        public static event Action<int> OnScoreChanged;
        public static event Action<int> OnNewHighScore;

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
                OnScoreChanged?.Invoke(score);
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
                    OnNewHighScore?.Invoke(highScore);
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
            Player.ScorePoint += ScorePoint;

            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
        }

        private void OnDisable()
        {
            Player.ScorePoint -= ScorePoint;

            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
        }

        private void OnGameStartOrRestart()
        {
            gotHighScoreThisRound = false;
            ResetScore();
        }

        private void Start()
        {
            ResetScore();
        }

        private void ScorePoint()
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