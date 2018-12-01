namespace Continuous
{
    public struct BarData
    {
        public float Speed { get; private set; }
        public float Size { get; private set; }

        private static float defaultSpeed = 1;
        private static float defaultSize = 9;
        public static BarData Default { get; } = new BarData(defaultSpeed, defaultSize);


        public BarData(float speed, float size)
        {
            Speed = speed;
            Size = size;
        }

        public BarData(float size)
        {
            Speed = defaultSpeed;
            Size = size;
        }
    }
}