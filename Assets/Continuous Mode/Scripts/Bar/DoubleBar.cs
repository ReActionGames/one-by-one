namespace Continuous
{
    public class DoubleBar : Bar
    {
        public override BarType Type => BarType.Double;

        protected override void OnAwake()
        {
            Scaler = new DoubleBarScaler(left, right);
            PickupPositioner = new DoubleBarPickupPositioner();
        }
    }
}