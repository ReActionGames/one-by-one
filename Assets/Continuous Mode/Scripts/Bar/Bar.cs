using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Transform bar;
        [SerializeField] private BarMovementProperties movementProperties;
        [SerializeField] private SpriteRenderer[] spriteRenderers;

        private IMover mover;
        private BarData currentData;

        private void Awake()
        {
            mover = new BarMover(bar, movementProperties);
        }

        public void Prepare(float yPos, BarData data)
        {
            currentData = data;
        }

        public void Show()
        {
            mover.StartMoving(currentData.Speed);
        }

        public void Stop()
        {
            mover.StopMoving();
        }

        public void Hide()
        {
            mover.StopMoving();
        }
    }
}