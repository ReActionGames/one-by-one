using DoozyUI;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndGame : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private float showScreenDelay;
    [SerializeField] private UIElement HUDElement;

    public event Action OnRestartGame;

    private UIElement endGameElement;

    private void Awake()
    {
        endGameElement = GetComponent<UIElement>();
    }

    private void OnEnable()
    {
        var player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnDie += HandleEdgeColliderHit;
        }
    }

    private void OnDisable()
    {
        var player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnDie -= HandleEdgeColliderHit;
        }
    }

    private void HandleEdgeColliderHit()
    {
        PrepareEndGameScreen();
        ShowEndGameScreenAfterDelay(showScreenDelay);
    }

    private void ShowEndGameScreenAfterDelay(float delay)
    {
        var sequence = DOTween.Sequence();
        sequence.PrependInterval(delay)
            .OnComplete(ShowEndGameScreen);
        sequence.Play();
    }

    private void ShowEndGameScreen()
    {
        endGameElement.Show(false);
        HUDElement.Hide(false);
    }

    private void PrepareEndGameScreen()
    {
        scoreText.text = FindObjectOfType<ScoreKeeper>().Score.ToString();
        int highScore = FindObjectOfType<ScoreKeeper>().HighScore;
        highscoreText.text = "HI " + highScore;
    }

    public void OnRestartClick()
    {
        OnRestartGame?.Invoke();
    }
}
