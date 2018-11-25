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
            Debug.Log("Shifting bars...");
            foreach (Transform bar in barParent)
            {
                bar.localPosition = bar.localPosition.Shift(y: -distance);
            }
        }

        //private void OnTweenUpdate()
        //{
        //    if (pathController.CurrentBar.transform.position.y > topOfScreen)
        //    {
        //        movementTween.timeScale *= 1.1f;
        //        return;
        //    }

        //    if (movementTween.timeScale > originalTimeScale)
        //    {
        //        movementTween.timeScale *= 0.95f;
        //        return;
        //    }

        //    if (movementTween.timeScale < originalTimeScale)
        //    {
        //        movementTween.timeScale += 0.1f;
        //        return;
        //    }

        //    if (Mathf.Approximately(movementTween.timeScale, originalTimeScale))
        //    {
        //        movementTween.timeScale = originalTimeScale;
        //    }
        //}

        public void UpdateTimeScale(float time)
        {
            movementTween.timeScale = time;
            Debug.Log($"Updating time ({time})");
        }

        public void StopMoving()
        {
            movementTween.Pause();
        }
    }
}