using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {

        [SerializeField] private Transform left, right;
        [SerializeField] private BarType type = BarType.Normal;

        private SpriteVisibility visibility;
        private ObjectVisibility objectVisibility;
        private BarScaler scaler;
        private BarPickups powerups;

        public BarType Type => type;

        private void Awake()
        {
            scaler = new BarScaler(left, right);
            visibility = GetComponent<SpriteVisibility>();
            objectVisibility = GetComponent<ObjectVisibility>();
            powerups = GetComponent<BarPickups>();
        }

        public void Prepare(BarData data)
        {
            scaler.Scale(data.Size);
            powerups.SetupPickup(data.PowerupType, data.Size);
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