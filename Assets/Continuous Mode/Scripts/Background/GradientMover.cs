using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class GradientMover : MonoBehaviour, IBackgroundElementMover
    {
        [SerializeField] private float speedMultiplier = 0.9f;
        [SerializeField] private float height;
        //[SerializeField] private bool randomizeStartPosition = true;
        // TODO: implement random starting position

        private Tween movementTween;

        public void StartMoving(float speed)
        {
            if (movementTween == null)
                InitializeTween(speed);
            movementTween.Play();
        }

        private void InitializeTween(float speed)
        {
            movementTween = transform.DOMoveY(transform.position.y - height, speed * speedMultiplier * height)
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
        private void Start()
        {
            //if (randomizeStartPosition)
            //{
            //    float randomAmount = UnityEngine.Random.Range(0, height);
            //    transform.position += Vector3.down * randomAmount;
            //    ResetPosition();
            //}
        }
    }
}