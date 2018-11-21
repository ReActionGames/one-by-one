using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Transform bar;
        [SerializeField] private BarMovementProperties movementProperties;

        private IMover mover;
        private BarData currentData;
        private BarVisibility visibility;

        private void Awake()
        {
            mover = new BarMover(bar, movementProperties);
            visibility = GetComponent<BarVisibility>();
        }

        public void Prepare(float yPos, BarData data)
        {
            currentData = data;
            transform.localPosition = new Vector3(0, yPos);
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