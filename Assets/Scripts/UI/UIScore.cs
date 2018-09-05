using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;

    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnEnable()
    {
        scoreKeeper.OnScoreChanged += UpdateScoreUI;
    }

    private void OnDisable()
    {
        scoreKeeper.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = scoreKeeper.Score.ToString();
    }
}
