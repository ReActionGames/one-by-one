using DoozyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private UIElement highScoreEffect;

    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnEnable()
    {
        scoreKeeper.OnScoreChanged += UpdateScoreUI;
        scoreKeeper.OnNewHighScore += ShowHighScoreEffect;
    }

    private void OnDisable()
    {
        scoreKeeper.OnScoreChanged -= UpdateScoreUI;
        scoreKeeper.OnNewHighScore -= ShowHighScoreEffect;
    }

    private void ShowHighScoreEffect()
    {
        highScoreEffect.Show(false);
    }

    private void UpdateScoreUI()
    {
        scoreText.text = scoreKeeper.Score.ToString();
    }
}
