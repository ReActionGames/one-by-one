using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Transform bar, left, right;
        [SerializeField] private BoxCollider2D center;
        [SerializeField] private BarMovementProperties movementProperties;
        [SerializeField] private string activeLayer;
        //[SerializeField] private string movingLayer = "MovingBar";
        [SerializeField] private string inactiveLayer;

        private IMover mover;
        private BarData currentData;
        private SpriteVisibility visibility;
        private BarScaler scaler;

        private void Awake()
        {
            mover = new BarMover(bar, movementProperties);
            scaler = new BarScaler(left, right, center);
            visibility = GetComponent<SpriteVisibility>();
            SetLayers(false);
        }

        public void Prepare(float yPos, BarData data)
        {
            currentData = data;
            transform.localPosition = new Vector3(0, yPos);
            scaler.Scale(data.Size);
            visibility.HideInstantly();
            //visibility.Hide();
            //gameObject.SetLayer(inactiveLayer, true);
            SetLayers(false);
        }

        public void Show()
        {
            mover.StartMoving(currentData.Speed);
            visibility.Show();
            //gameObject.SetLayer(movingLayer, true);
            SetLayers(false);
        }

        public void Stop()
        {
            mover.StopMoving();
            //gameObject.SetLayer(placedLayer, true);
            SetLayers(true);
        }

        public void Hide()
        {
            mover.StopMoving();
            visibility.Hide();
            //gameObject.SetLayer(inactiveLayer, true);
            SetLayers(false);
        }

        private void SetLayers(bool active)
        {
            if (active)
            {
                left.gameObject.SetLayer(activeLayer);
                right.gameObject.SetLayer(activeLayer);
                return;
            }

            left.gameObject.SetLayer(inactiveLayer);
            right.gameObject.SetLayer(inactiveLayer);
        }

    }
}