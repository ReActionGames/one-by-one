using DG.Tweening;
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
            bar.position = bar.position.With(x: movementProperties.LeftXPosition);
            bar.DOMoveX(movementProperties.RightXPosition, speed)
                .SetEase(movementProperties.Easing)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void StopMoving()
        {
            throw new System.NotImplementedException();
        }
    }
}