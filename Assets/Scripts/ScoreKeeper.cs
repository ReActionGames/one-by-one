using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private const string HighScorePlayerPrefsKey = "highscore";


    public Action OnScoreChanged;
    public Action OnNewHighScore;

    [ReadOnly]
    [SerializeField] private int score;

    [ReadOnly]
    [SerializeField] private int highScore = -1;

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
            OnNewHighScore?.Invoke();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HighScorePlayerPrefsKey, HighScore);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnEnterState += OnEnterState;
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnCenterColliderEnter += HandleCenterColliderEnter;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnEnterState -= OnEnterState;
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnCenterColliderEnter -= HandleCenterColliderEnter;
        }
    }

    private void OnEnterState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Active)
        {
            DOVirtual.DelayedCall(0.1f, ResetScore);
        }
    }

    private void Start()
    {
        ResetScore();
    }

    private void HandleCenterColliderEnter()
    {
        IncrementScore();
    }

    private void IncrementScore()
    {
        Score++;
        if(Score > HighScore)
        {
            HighScore = Score;
        }
    }

    private void ResetScore()
    {
        Score = 0;
    }
}