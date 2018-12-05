using DG.Tweening;
using System;
using UnityEngine;

namespace Continuous
{
    [Serializable]
    public class BarPoolMover : MonoBehaviour, IBackgroundElementMover
    {
        [SerializeField] private Transform barParent;

        private Vector3 originalPosition;
        private float originalTimeScale = 1;
        private float topOfScreen;
        private Tween movementTween;
        private float distance = 100;
        private PathController pathController;

        private void Awake()
        {
            originalPosition = barParent.position;
        }

        private void OnEnable()
        {
            GameManager.GameStart += ResetPosition;
            GameManager.GameRestart += ResetPosition;
        }

        private void OnDisable()
        {
            GameManager.GameStart -= ResetPosition;
            GameManager.GameRestart -= ResetPosition;
        }

        private void ResetPosition()
        {
            movementTween.Pause();
            barParent.position = originalPosition;
        }

        public void StartMoving(float speed)
        {
            movementTween = barParent.DOMoveY(barParent.position.y - distance, speed * Math.Abs(distance))
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear)
                .OnStepComplete(OnTweenCompletedLoop);

            movementTween.Play();
        }

        private void OnTweenCompletedLoop()
        {
            foreach (Transform bar in barParent)
            {
                bar.localPosition = bar.localPosition.Shift(y: -distance);
            }
        }
        
        public void UpdateTimeScale(float time)
        {
            movementTween.timeScale = time;
        }

        public void StopMoving()
        {
            movementTween.Pause();
        }
    }
}