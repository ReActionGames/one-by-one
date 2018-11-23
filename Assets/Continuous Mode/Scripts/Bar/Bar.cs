using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Transform bar, left, right;
        [SerializeField] private BoxCollider2D center;
        [SerializeField] private BarMovementProperties movementProperties;

        private IMover mover;
        private BarData currentData;
        private BarVisibility visibility;
        private BarScaler scaler;

        private void Awake()
        {
            mover = new BarMover(bar, movementProperties);
            scaler = new BarScaler(left, right, center);
            visibility = GetComponent<BarVisibility>();
        }

        public void Prepare(float yPos, BarData data)
        {
            currentData = data;
            transform.localPosition = new Vector3(0, yPos);
            scaler.Scale(data.Size);
            visibility.HideInstantly();
        }

        public void Show()
        {
            mover.StartMoving(currentData.Speed);
            visibility.Show();
        }

        public void Stop()
        {
            mover.StopMoving();
        }

        public void Hide()
        {
            mover.StopMoving();
            visibility.Hide();
        }
    }
}