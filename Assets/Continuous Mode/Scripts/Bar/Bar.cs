using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {
        public enum State
        {
            Inactive,
            Moving,
            Active
        }

        [SerializeField] private float maxXStartDistanceFromCenter = 2f;
        [SerializeField] private Transform bar, left, right;
        [SerializeField] private BarMovementProperties movementProperties;

        private IMover mover;
        private BarData currentData;
        private SpriteVisibility visibility;
        private ObjectVisibility objectVisibility;
        private BarScaler scaler;
        private BarPickups powerups;

        public State state { get; private set; }

        private void Awake()
        {
            mover = new BarMover(bar, movementProperties);
            scaler = new BarScaler(left, right);
            visibility = GetComponent<SpriteVisibility>();
            objectVisibility = GetComponent<ObjectVisibility>();
            powerups = GetComponent<BarPickups>();
            state = State.Inactive;
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
            state = State.Inactive;
        }
        
        public void Show()
        {
            mover.StartMoving(currentData.Speed);
            visibility.Show();
            objectVisibility.Show();
            state = State.Moving;
        }

        public void Stop()
        {
            mover.StopMoving();
            state = State.Active;
        }

        public void Hide()
        {
            mover.StopMoving();
            visibility.Hide();
            objectVisibility.Hide();
            state = State.Inactive;
        }

        public void HideInstantly()
        {
            mover.StopMoving();
            visibility.HideInstantly();
            objectVisibility.Hide();
            state = State.Inactive;
        }

        //private void SetLayers(bool active)
        //{
        //    if (active)
        //    {
        //        left.gameObject.SetLayer(activeLayer);
        //        right.gameObject.SetLayer(activeLayer);
        //        return;
        //    }

        //    left.gameObject.SetLayer(inactiveLayer);
        //    right.gameObject.SetLayer(inactiveLayer);
        //}

    }
}