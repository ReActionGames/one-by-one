using DG.Tweening;
using System;
using UnityEngine;

namespace Continuous
{
    [Serializable]
    public class BarPoolMover : MonoBehaviour, IBackgroundElementMover
    {
        [SerializeField] private Transform barParent;

        private float originalTimeScale = 1;
        private float topOfScreen;
        private Tween movementTween;
        private float distance = 100;
        private PathController pathController;

        public void StartMoving(float speed)
        {
            movementTween = barParent.DOMoveY(barParent.position.y - distance, speed * Math.Abs(distance))
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear)
                .OnStepComplete(OnTweenCompletedLoop);

            movementTween.Play();
            //movementTween.timeScale = 0.1f;
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
            Debug.Log("Stop Moving");
            movementTween.Pause();
        }
    }
}