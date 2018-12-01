using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private float startDelay;
        [SerializeField] private float startDuration;
        [SerializeField] private Ease startEasing;
        [SerializeField] private float stopDuration;
        [SerializeField] private Ease stopEasing;

        private IBackgroundElementMover[] backgroundElementMovers;
        private float originalTimeScale = 1;
        private float currentTimeScale = 1;
        private float topOfScreen;
        private PathController pathController;
        private bool moving = false;

        private static string startTweenID = "bg-start";

        private void Awake()
        {
            backgroundElementMovers = this.FindMonoBehavioursOfInterface<IBackgroundElementMover>();

            pathController = FindObjectOfType<PathController>();

            Camera cam = Camera.main;
            topOfScreen = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight)).y;
        }

        private void OnEnable()
        {
            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
            GameManager.GameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
            GameManager.GameEnd -= OnGameEnd;
        }

        private void OnGameStartOrRestart()
        {
            DOTween.Kill(startTweenID);
            moving = false;
            DOVirtual.DelayedCall(startDelay, SmoothStart)
                .SetId(startTweenID);
        }

        private void SmoothStart()
        {
            foreach (IBackgroundElementMover mover in backgroundElementMovers)
            {
                mover.StartMoving(speed);
            }

            DOVirtual.Float(0.1f, originalTimeScale, startDuration, UpdateTimeScale)
                .SetEase(startEasing)
                .OnComplete(StartMoving)
                .SetId(startTweenID);
            //UpdateTimeScale();
        }

        private void StartMoving()
        {
            moving = true;
        }

        private void UpdateTimeScale()
        {
            foreach (IBackgroundElementMover mover in backgroundElementMovers)
            {
                mover.UpdateTimeScale(currentTimeScale);
            }
        }

        private void UpdateTimeScale(float time)
        {
            currentTimeScale = time;
            UpdateTimeScale();
        }

        private void Update()
        {
            if (moving == false)
                return;

            if (pathController.CurrentBar.transform.position.y > topOfScreen)
            {
                currentTimeScale *= 1.1f;
                UpdateTimeScale();
                return;
            }

            if (currentTimeScale == originalTimeScale)
                return;

            if (currentTimeScale > originalTimeScale)
            {
                currentTimeScale *= 0.95f;
                UpdateTimeScale();
            }

            if (currentTimeScale < originalTimeScale)
            {
                currentTimeScale += 0.1f;
                UpdateTimeScale();
            }

            if (Mathf.Abs(1f - currentTimeScale) <= 0.1f)
            {
                currentTimeScale = originalTimeScale;
                UpdateTimeScale();
            }
        }

        private void OnGameEnd()
        {
            moving = false;
            DOTween.Kill(startTweenID);
            DOVirtual.Float(currentTimeScale, 0.1f, stopDuration, UpdateTimeScale)
                .SetEase(stopEasing)
                .OnComplete(StopMoving);
        }

        private void StopMoving()
        {
            foreach (IBackgroundElementMover mover in backgroundElementMovers)
            {
                mover.StopMoving();
            }
        }
    }

    public interface IBackgroundElementMover : IMover
    {
        void UpdateTimeScale(float time);
    }
}