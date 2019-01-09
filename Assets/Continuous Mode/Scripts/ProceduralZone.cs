using Sirenix.OdinInspector;
using UnityEngine;

namespace Continuous
{
    public class ProceduralZone : ScriptableObject
    {
        [ShowInInspector]
        public float AverageSize { get; set; } = 6;
        [ShowInInspector]
        public float SizeDistribution { get; set; } = 2;
        [ShowInInspector]
        public float MinSize { get; set; } = 3;
    }
}