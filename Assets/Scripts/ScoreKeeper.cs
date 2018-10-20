using DG.Tweening;
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
    }

    private void ResetScore()
    {
        Score = 0;
    }
}