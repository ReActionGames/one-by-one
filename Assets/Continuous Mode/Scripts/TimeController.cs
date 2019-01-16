using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class TimeController : MonoBehaviour
    {
        private float currentTime = 1;
        private Tween smoothStop;

        private void Awake()
        {
            SetTime(1);
        }

        private void OnEnable()
        {
            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
            GameManager.GameEnding += OnGameEnding;
            GameManager.GameEnd += OnGameEnd;

            PathController.BarPlaced += OnBarPlaced;
        }

        private void OnDisable()
        {
            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
            GameManager.GameEnding -= OnGameEnding;
            GameManager.GameEnd -= OnGameEnd;

            PathController.BarPlaced -= OnBarPlaced;
        }

        private void OnGameStartOrRestart()
        {
            SetTime(1);
        }

        private void OnBarPlaced()
        {
            float time = ProceduralPathGenerator.GetCurrentTimeScale(ScoreKeeper.Score);
            SetTime(time);
        }

        private void OnGameEnding()
        {
            Debug.Log($"Time scale on game end:{currentTime}");
            smoothStop = DOVirtual.Float(currentTime, 1, 0.5f, SetTime);
        }

        private void OnGameEnd()
        {
            smoothStop.Kill();
            SetTime(1);
        }

        private void SetTime(float time)
        {
            currentTime = time;
            //DOTween.timeScale = time;
            Time.timeScale = time;
        }
    }
}