using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class GradientMover : MonoBehaviour, IBackgroundElementMover
    {
        [SerializeField] private float speedMultiplier = 0.9f;
        [SerializeField] private float height;
        [SerializeField] private float maxStartHeight;
        [SerializeField] private bool randomizeStartPosition = true;

        private Tween movementTween;

        private void Start()
        {
            if (randomizeStartPosition)
            {
                float randomAmount = Random.Range(0, maxStartHeight);
                transform.position += Vector3.down * randomAmount;
            }
        }

        public void StartMoving(float speed)
        {
            if (movementTween == null)
                InitializeTween(speed);
            movementTween.Play();
        }

        private void InitializeTween(float speed)
        {
            movementTween = transform.DOMoveY(transform.position.y - height, (1/speed) * speedMultiplier * height)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        public void StopMoving()
        {
            movementTween.Pause();
        }

        public void UpdateTimeScale(float time)
        {
            movementTween.timeScale = time;
        }
    }
}