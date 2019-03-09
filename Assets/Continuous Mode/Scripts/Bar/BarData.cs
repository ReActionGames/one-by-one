namespace Continuous
{
    public struct BarData
    {
        public float Speed { get; private set; }
        public float Size { get; private set; }
        public BarType Type { get; private set; }
        public PickupType PowerupType { get; private set; }

        private static float defaultSpeed = 1;
        private static float defaultSize = 9;
        private static PickupType defaultPowerupType = PickupType.None;
        private static BarType defaultType = BarType.Normal;
        public static BarData Default { get; } = new BarData(defaultSpeed, defaultSize, defaultPowerupType, defaultType);

        public BarData(float speed = 1, float size = 9, PickupType powerupType = PickupType.None, BarType type = BarType.Normal)
        {
            Speed = speed;
            Size = size;
            PowerupType = powerupType;
            Type = type;
        }
    }
}