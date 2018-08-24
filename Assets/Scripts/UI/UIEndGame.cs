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
    [SerializeField] private float showScreenDelay;

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
            player.OnEdgeColliderHit += HandleEdgeColliderHit;
        }
    }

    private void OnDisable()
    {
        var player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnEdgeColliderHit -= HandleEdgeColliderHit;
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
    }

    private void PrepareEndGameScreen()
    {
        scoreText.text = FindObjectOfType<ScoreKeeper>().Score.ToString();
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
