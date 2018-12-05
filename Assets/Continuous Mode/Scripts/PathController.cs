﻿using System;
using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class PathController : MonoBehaviour
    {
        public static event Action BarPlaced;

        [SerializeField] private BarPool barPool;
        [SerializeField] private Transform barPoolParent;

        public Bar CurrentBar { get; private set; }
                
        private void OnEnable()
        {
            GameManager.GameStart += OnGameStart;
            GameManager.GameRestart += OnGameRestart;
            GameManager.GameEnd += OnGameEnd;
        }
        private void OnDisable()
        {
            GameManager.GameStart -= OnGameStart;
            GameManager.GameRestart -= OnGameRestart;
            GameManager.GameEnd -= OnGameEnd;
        }

        private void OnGameRestart()
        {
            StartGame();
        }

        private void OnGameStart()
        {
            StartGame();
        }

        private void OnGameEnd()
        {
            StopCurrentBar();
        }

        private void StartGame()
        {
            barPool.PreWarm(barPoolParent);
            ActivateNextBar();
        }

        private void ActivateNextBar()
        {
            Bar nextBar = barPool.GetNextBar();
            nextBar.Show();
            CurrentBar = nextBar;
        }

        private void Update()
        {
            if (GameManager.CurrentGameState != GameState.Playing)
                return;

            if (Input.GetButtonDown("Fire1"))
                OnClick();
        }

        private void OnClick()
        {
            StopCurrentBar();
            ActivateNextBar();
            barPool.RecycleBars();
            BarPlaced?.Invoke();
        }

        private void StopCurrentBar()
        {
            CurrentBar.Stop();
        }
    }
}