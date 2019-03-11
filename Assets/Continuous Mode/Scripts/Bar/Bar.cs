using UnityEngine;

namespace Continuous
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] protected Transform left, right;

        private SpriteVisibility visibility;
        private ObjectVisibility objectVisibility;
        private BarPickups pickups;

        public virtual BarType Type => BarType.Normal;

        protected virtual IScaler Scaler
        {
            get; set;
        }

        protected virtual IPickupPositioner PickupPositioner { get; set; }

        private void Awake()
        {
            visibility = GetComponent<SpriteVisibility>();
            objectVisibility = GetComponent<ObjectVisibility>();
            pickups = GetComponent<BarPickups>();

            OnAwake();

            if (Scaler == null)
            {
                Scaler = new NormalBarScaler(left, right);
            }
            if (PickupPositioner == null)
            {
                PickupPositioner = new NormalBarPickupPositioner();
            }
        }

        protected virtual void OnAwake()
        {
        }

        public void Prepare(BarData data)
        {
            Scaler.Scale(data.Size);
            pickups.SetupPickup(data.PickupType, data.Size, PickupPositioner);
        }

        public void Show()
        {
            visibility.Show();
            objectVisibility.Show();
        }

        public void Hide()
        {
            visibility.Hide();
            objectVisibility.Hide();
        }

        public void HideInstantly()
        {
            visibility.HideInstantly();
            objectVisibility.Hide();
        }
    }
}