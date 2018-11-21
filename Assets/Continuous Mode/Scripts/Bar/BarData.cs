namespace Continuous
{
    public struct BarData
    {
        public float Speed { get; private set; }
        public float Size { get; private set; }

        public static BarData Default { get; } = new BarData(1, 9);


        public BarData(float speed, float size)
        {
            Speed = speed;
            Size = size;
        }
    }
}