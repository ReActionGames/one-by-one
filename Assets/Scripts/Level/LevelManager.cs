﻿using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level currentLevel, nextLevel;
    [SerializeField] private Transform topOfScreen, bottomOfScreen;
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;
    [SerializeField] private bool playOnStart = false;

    public event Action<float, Ease> OnBackgroundMove;

    public event Action OnBarsSet;

    public event Action OnLevelStart;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        player.OnLevelCompleted += HandleLevelCompleted;
        currentLevel.OnBarsSet += HandleBarsSet;
        nextLevel.OnBarsSet += HandleBarsSet;

        UIPlay.OnPlayButtonClicked += HandlePlayButtonClicked;
    }

    private void HandlePlayButtonClicked()
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
        }

        StartGame();
    }

    private void OnDisable()
    {
        player.OnLevelCompleted -= HandleLevelCompleted;
        currentLevel.OnBarsSet -= HandleBarsSet;
        nextLevel.OnBarsSet -= HandleBarsSet;

        UIPlay.OnPlayButtonClicked -= HandlePlayButtonClicked;
    }

    private void Start()
    {
        if (playOnStart)
        {
            StartGame();
        }
    }

    [Button]
    public void StartGame()
    {
        StartCurrentLevel();
    }

    private void HandleBarsSet()
    {
        OnBarsSet?.Invoke();
    }

    private void HandleLevelCompleted()
    {
        NextLevel();
    }

    private void NextLevel()
    {
        transform.position = topOfScreen.position;

        nextLevel.gameObject.SetActive(true);

        currentLevel.transform.SetParent(transform, true);
        nextLevel.transform.SetParent(transform, true);
        player.transform.SetParent(transform, true);

        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(delay)
            .AppendCallback(InvokeOnBackgroundMove)
            .Append(transform.DOMove(bottomOfScreen.position, duration).SetEase(easing))
            .OnComplete(OnMovementCompleted);
    }

    private void InvokeOnBackgroundMove()
    {
        OnBackgroundMove?.Invoke(duration, easing);
    }

    private void OnMovementCompleted()
    {
        currentLevel.transform.parent = null;
        nextLevel.transform.parent = null;
        player.transform.parent = null;

        SwapLevelReferences();
        nextLevel.transform.position = topOfScreen.position;
        nextLevel.Reset();
        StartCurrentLevel();
    }

    private void StartCurrentLevel()
    {
        currentLevel.gameObject.SetActive(true);
        nextLevel.gameObject.SetActive(false);

        currentLevel.transform.position = bottomOfScreen.position;
        nextLevel.transform.position = topOfScreen.position;

        currentLevel.StartBarsMoving();
        OnLevelStart?.Invoke();
    }

    private void SwapLevelReferences()
    {
        Level temp = currentLevel;
        currentLevel = nextLevel;
        nextLevel = temp;
    }
}