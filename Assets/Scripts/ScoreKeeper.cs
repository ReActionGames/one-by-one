using DG.Tweening;
using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour, IResetable
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

    private void Start()
    {
        //Debug.Log("[" + Time.time + "] Start...");
        ResetScore();
    }

    private void HandleCenterColliderEnter()
    {
        //Debug.Log("[" + Time.time + "] Handling Center Collider Enter...");
        IncrementScore();
    }

    private void IncrementScore()
    {
        //Debug.Log("[" + Time.time + "] Incrementing Score...");
        Score++;
    }

    private void ResetScore()
    {
        //Debug.Log("[" + Time.time + "] Reseting Score... ");
        Score = 0;
    }

    public void ResetObject()
    {
        //Debug.Log("[" + Time.time + "] Reseting Object..." );
        DOVirtual.DelayedCall(0.1f, ResetScore);
        //ResetScore();
    }
}