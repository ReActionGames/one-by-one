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
        GameManager.Instance.OnEnterState += OnEnterState;
    }

    private void OnDisable()
    {

        if (GameManager.Instance != null)
            GameManager.Instance.OnEnterState -= OnEnterState;
    }

    private void OnEnterState(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.End:
                OnEndGame();
                break;
        }
    }

    private void OnEndGame()
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
        GameManager.Instance.AttemptChangeState(GameManager.GameState.Active);
    }
}
