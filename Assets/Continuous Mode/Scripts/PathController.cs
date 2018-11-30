using System;
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

        public Bar CurrentBar { get; private set; }

        private IMover barPoolMover = null;
        private Vector3 originalBarPoolPosition;

        private void Awake()
        {
            originalBarPoolPosition = barPoolParent.position;
        }

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
            //barPool.HideAllBars();
            barPoolParent.position = originalBarPoolPosition;
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