﻿namespace Continuous
{
    public interface IPathProvider
    {
        BarData GetNextBar();
        void Initialize();
    }
}