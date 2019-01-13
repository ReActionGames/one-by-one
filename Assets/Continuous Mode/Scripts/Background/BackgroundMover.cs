using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] private float mainMenuSpeed;
        [SerializeField] private float startDelay;
        [SerializeField] private float startDuration;
        [SerializeField] private Ease startEasing;
        [SerializeField] private float stopDuration;
        [SerializeField] private Ease stopEasing;

        private float speed => RemoteSettingsValues.BackgroundSpeed;
        private IBackgroundElementMover[] backgroundElementMovers;
        private float startingTimeScale = 1;
        private float currentTimeScale = 1;
        private float timeScaleFactor = 1;
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
        
        private void Start()
        {
            SmoothStart(mainMenuSpeed);
        }

        private void OnEnable()
        {
            GameManager.GameStart += OnGameStart;
            GameManager.GameRestart += OnGameRestart;
            GameManager.GameEnd += OnGameEndOrEnding;
            GameManager.GameEnding += OnGameEndOrEnding;
        }

        private void OnDisable()
        {
            GameManager.GameStart -= OnGameStart;
            GameManager.GameRestart -= OnGameRestart;
            GameManager.GameEnd -= OnGameEndOrEnding;
            GameManager.GameEnding -= OnGameEndOrEnding;
        }

        private void OnGameStart()
        {
            DOTween.Kill(startTweenID);
            SmoothStop();

            DOVirtual.DelayedCall(startDelay, () => SmoothStart(speed))
                .SetId(startTweenID);
        }

        private void OnGameRestart()
        {
            DOTween.Kill(startTweenID);
            moving = false;
            DOVirtual.DelayedCall(startDelay, () => SmoothStart(speed))
                .SetId(startTweenID);
        }

        private void SmoothStart(float speed)
        {
            foreach (IBackgroundElementMover mover in backgroundElementMovers)
            {
                mover.StartMoving(speed);
            }

            DOVirtual.Float(0.1f, startingTimeScale, startDuration, UpdateTimeScale)
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
                mover.UpdateTimeScale(currentTimeScale * timeScaleFactor);
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

            //UpdateTimeScale(ProceduralPathGenerator.GetCurrentTimeScale(ScoreKeeper.Score));

            if (CurrentBarOutOfView())
            {
                timeScaleFactor *= 1.1f;
                //currentTimeScale *= 1.1f;
                UpdateTimeScale();
                return;
            }

            if (timeScaleFactor == 1)
                return;

            if (timeScaleFactor > 1)
            {
                timeScaleFactor *= 0.95f;
                UpdateTimeScale();
            }

            if (timeScaleFactor < 1)
            {
                timeScaleFactor += 0.1f;
                UpdateTimeScale();
            }

            if (Mathf.Abs(1f - timeScaleFactor) <= 0.1f)
            {
                timeScaleFactor = 1;
                UpdateTimeScale();
            }
        }

        private bool CurrentBarOutOfView()
        {
            if (pathController.CurrentBar == null)
                return false;

            return pathController.CurrentBar.transform.position.y > topOfScreen;
        }

        private void OnGameEndOrEnding()
        {
            DOTween.Kill(startTweenID);
            SmoothStop();
        }

        private void SmoothStop()
        {
            moving = false;
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
}