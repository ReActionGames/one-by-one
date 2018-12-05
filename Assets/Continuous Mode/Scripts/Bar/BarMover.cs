﻿using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class BarMover : IMover
    {
        private Transform bar;
        private BarMovementProperties movementProperties;
    
        public BarMover(Transform bar, BarMovementProperties movementProperties)
        {
            this.bar = bar;
            this.movementProperties = movementProperties;
        }

        public void StartMoving(float speed)
        {
            float startPos = movementProperties.LeftXPosition;
            float endPos = movementProperties.RightXPosition;

            // Randomize starting side
            if (RandomExtensions.RandomBoolean())
            {
                startPos = movementProperties.RightXPosition;
                endPos = movementProperties.LeftXPosition;
            }

            bar.position = bar.position.With(x: startPos);

            bar.DOMoveX(endPos, speed)
                .SetEase(movementProperties.Easing)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void StopMoving()
        {
            bar.DOPause();
        }
    }
}