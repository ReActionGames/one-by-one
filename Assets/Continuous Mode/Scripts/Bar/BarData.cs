namespace Continuous
{
    public struct BarData
    {
        public float Speed { get; private set; }
        public float Size { get; private set; }
        public PickupType PowerupType { get; private set; }

        private static float defaultSpeed = 1;
        private static float defaultSize = 9;
        private static PickupType defaultPowerupType = PickupType.None;
        public static BarData Default { get; } = new BarData(defaultSpeed, defaultSize);

        public BarData(float speed = 1, float size = 9, PickupType powerupType = PickupType.None)
        {
            Speed = speed;
            Size = size;
            PowerupType = powerupType;
        }

        public BarData(float speed, float size)
        {
            Speed = speed;
            Size = size;
            PowerupType = defaultPowerupType;
        }

        public BarData(float size)
        {
            Speed = defaultSpeed;
            Size = size;
            PowerupType = defaultPowerupType;
        }
    }
}