namespace Continuous
{
    public struct BarData
    {
        public float Size { get; private set; }
        public BarType Type { get; private set; }
        public PickupType PowerupType { get; private set; }

        private static float defaultSize = 9;
        private static PickupType defaultPowerupType = PickupType.None;
        private static BarType defaultType = BarType.Normal;
        public static BarData Default { get; } = new BarData(defaultSize, defaultPowerupType, defaultType);

        public BarData(float size = 9, PickupType powerupType = PickupType.None, BarType type = BarType.Normal)
        {
            Size = size;
            PowerupType = powerupType;
            Type = type;
        }
    }
}