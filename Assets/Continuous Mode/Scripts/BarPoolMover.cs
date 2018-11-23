using DG.Tweening;
using System;
using UnityEngine;

namespace Continuous
{
    public class BarPoolMover : IMover
    {
        private Transform barParent;

        private float originalTimeScale = 1;
        private float topOfScreen;
        private Tween movementTween;
        private float distance = 100;
        private PathController pathController;

        public BarPoolMover(Transform barParent, PathController pathController)
        {
            this.barParent = barParent;
            this.pathController = pathController;

            Camera cam = Camera.main;
            topOfScreen = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight)).y;
        }

        public void StartMoving(float speed)
        {
            movementTween = barParent.DOMoveY(barParent.position.y - distance, speed * Math.Abs(distance))
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear)
                .OnStepComplete(OnTweenCompletedLoop)
                .OnUpdate(OnTweenUpdate);

            movementTween.Play();
        }

        private void OnTweenCompletedLoop()
        {
            Debug.Log("Shifting bars...");
            foreach (Transform bar in barParent)
            {
                bar.localPosition = bar.localPosition.Shift(y: -distance);
            }
        }

        private void OnTweenUpdate()
        {
            if (pathController.CurrentBar.transform.position.y > topOfScreen)
            {
                movementTween.timeScale *= 1.1f;
                return;
            }

            if (movementTween.timeScale > originalTimeScale)
            {
                movementTween.timeScale *= 0.95f;
                return;
            }

            if (movementTween.timeScale < originalTimeScale)
            {
                movementTween.timeScale += 0.1f;
                return;
            }

            if(Mathf.Approximately(movementTween.timeScale, originalTimeScale))
            {
                movementTween.timeScale = originalTimeScale;
            }
        }

        public void StopMoving()
        {
            movementTween.Pause();
        }
    }
}