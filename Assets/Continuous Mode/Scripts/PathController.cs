﻿using System;
using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class PathController : MonoBehaviour
    {
        [SerializeField] private BarPool barPool;
        [SerializeField] private Transform barPoolParent;
        [SerializeField] private float barMovementSpeed = 1;
        [SerializeField] private float movementDelay = 2;

        private IMover barPoolMover = null;
        public Bar CurrentBar { get; private set; }

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.GameStart, OnGameStart);
            EventManager.StartListening(EventNames.GameEnd, OnGameEnd);
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventNames.GameStart, OnGameStart);
            EventManager.StopListening(EventNames.GameEnd, OnGameEnd);
        }
        private void OnGameStart(Message message)
        {
            barPool.PreWarm(barPoolParent);
            ActivateNextBar();

        }

        private void OnGameEnd(Message message)
        {
            StopCurrentBar();
        }

        private void ActivateNextBar()
        {
            Bar nextBar = barPool.GetNextBar();
            nextBar.Show();
            CurrentBar = nextBar;
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing)
                return;

            if (Input.GetButtonDown("Fire1"))
                OnClick();
        }

        private void OnClick()
        {
            StopCurrentBar();
            ActivateNextBar();
            barPool.RecycleBars();
            EventManager.TriggerEvent(EventNames.BarPlaced);
        }

        private void StopCurrentBar()
        {
            CurrentBar.Stop();
        }
    }
}