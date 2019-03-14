using UnityEngine;

namespace Continuous
{
    public class DoubleBar : Bar
    {
        [SerializeField] private Transform center;

        public override BarType Type => BarType.Double;

        protected override void OnAwake()
        {
            Scaler = new DoubleBarScaler(left, right, center);
            PickupPositioner = new DoubleBarPickupPositioner();
        }
    }
}