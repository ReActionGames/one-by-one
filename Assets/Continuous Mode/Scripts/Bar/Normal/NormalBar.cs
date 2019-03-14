namespace Continuous
{
    public class NormalBar : Bar
    {
        public override BarType Type => BarType.Normal;

        protected override void OnAwake()
        {
            Scaler = new NormalBarScaler(left, right);
            PickupPositioner = new NormalBarPickupPositioner();
        }
    }
}