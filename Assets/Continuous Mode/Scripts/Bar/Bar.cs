using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private float maxXStartDistanceFromCenter = 2f;
        [SerializeField] private Transform bar, left, right;
        [SerializeField] private BoxCollider2D center;
        [SerializeField] private BarMovementProperties movementProperties;
        [SerializeField] private string activeLayer;
        //[SerializeField] private string movingLayer = "MovingBar";
        [SerializeField] private string inactiveLayer;

        private IMover mover;
        private BarData currentData;
        private SpriteVisibility visibility;
        private ObjectVisibility objectVisibility;
        private BarScaler scaler;
        private BarPickups powerups;

        private void Awake()
        {
            mover = new BarMover(bar, movementProperties);
            scaler = new BarScaler(left, right, center);
            visibility = GetComponent<SpriteVisibility>();
            objectVisibility = GetComponent<ObjectVisibility>();
            powerups = GetComponent<BarPickups>();
            SetLayers(false);
        }

        public void SetYPosition(float position)
        {
            transform.localPosition = new Vector3(0, position);
        }

        public void Prepare(BarData data)
        {
            float xPos = Random.Range(-maxXStartDistanceFromCenter, maxXStartDistanceFromCenter);
            transform.localPosition = transform.localPosition.With(x: xPos);

            currentData = data;
            scaler.Scale(data.Size);
            powerups.SetupPickup(data.PowerupType, data.Size);
            HideInstantly();
        }
        
        public void Show()
        {
            mover.StartMoving(currentData.Speed);
            visibility.Show();
            objectVisibility.Show();
            SetLayers(false);
        }

        public void Stop()
        {
            mover.StopMoving();
            SetLayers(true);
        }

        public void Hide()
        {
            mover.StopMoving();
            visibility.Hide();
            objectVisibility.Hide();
            SetLayers(false);
        }

        public void HideInstantly()
        {
            mover.StopMoving();
            visibility.HideInstantly();
            objectVisibility.Hide();
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