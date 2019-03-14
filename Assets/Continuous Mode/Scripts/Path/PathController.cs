using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    public class PathController : SerializedMonoBehaviour
    {
        public static event Action BarPlaced;

        public static event Action<float> BarPlacedWithTime;

        [SerializeField] private BarPool barPool;
        [SerializeField] private Transform barPoolParent;
        [SerializeField] private IPathProvider pathProvider;

        public BarController CurrentBar { get; private set; }

        private float timeStampOfBarActivated;

        private void OnEnable()
        {
            GameManager.GameStartOrRestart += OnGameStartOrRestart;
            GameManager.GameEnd += OnGameEndOrEnding;
            GameManager.GameEnding += OnGameEndOrEnding;
        }

        private void OnDisable()
        {
            GameManager.GameStartOrRestart -= OnGameStartOrRestart;
            GameManager.GameEnd -= OnGameEndOrEnding;
            GameManager.GameEnding -= OnGameEndOrEnding;
        }
        
        private void OnGameStartOrRestart()
        {
            StartGame();
        }

        private void OnGameEndOrEnding()
        {
            StopCurrentBar();
        }

        private void StartGame()
        {
            barPool.PreWarm(barPoolParent);
            pathProvider.Initialize();
            ActivateNextBar();
        }

        private void ActivateNextBar()
        {
            timeStampOfBarActivated = Time.unscaledTime;

            BarData data = pathProvider.GetNextBar();
            BarController nextBar = barPool.GetNextBar(data);
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
            BarPlaced?.Invoke();
            BarPlacedWithTime?.Invoke(Time.unscaledTime - timeStampOfBarActivated);
            ActivateNextBar();
            barPool.RecycleBars();
        }

        private void StopCurrentBar()
        {
            CurrentBar.Stop();
        }

#if UNITY_EDITOR

        public static void InvokeBarPlacedEvent()
        {
            BarPlaced?.Invoke();
        }

        [Button("Prewarm Pool")]
        private void EditorPrewarmPool()
        {
            barPool.PreWarmInEditor(barPoolParent);
        }

#endif
    }
}