using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public Action OnScoreChanged;

    [SerializeField] private int score;

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

    private void OnEnable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnCenterColliderEnter += HandleCenterColliderEnter;
        }
    }

    private void OnDisable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnCenterColliderEnter -= HandleCenterColliderEnter;
        }
    }

    private void HandleCenterColliderEnter()
    {
        IncrementScore();
    }

    private void IncrementScore()
    {
        Score++;
    }
}