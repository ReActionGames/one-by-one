using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    [Serializable]
    public struct ProceduralZone
    {
        public static ProceduralZone Default { get; } = new ProceduralZone()
        {
            startingScore = 0,
            endingScore = 10,
            startingSpeed = 1,
            endingSpeed = 3,
            averageSize = 5,
            sizeDistribution = 2,
            MinMaxBarSize = new Vector2(3, 10),
            barTypes = BarType.Normal
        };

        [VerticalGroup("Duration")]
        [BoxGroup("Duration/Score", Order = -2)]
        [TableColumnWidth(100)]
        [LabelWidth(90)]
        [SerializeField]
        private int startingScore;

        //[VerticalGroup("Duration")]
        [BoxGroup("Duration/Score")]
        [LabelWidth(90)]
        [SerializeField]
        private int endingScore;

        //[VerticalGroup("Duration")]
        [BoxGroup("Duration/Speed", Order = -1)]
        [LabelWidth(90)]
        [SerializeField]
        private float startingSpeed;

        //[VerticalGroup("Duration")]
        [BoxGroup("Duration/Speed")]
        [LabelWidth(90)]
        [SerializeField]
        private float endingSpeed;

        [VerticalGroup("Bars")]
        [BoxGroup("Bars/Bars")]
        [TableColumnWidth(120)]
        [LabelWidth(90)]
        [SerializeField]
        private float averageSize;

        //[VerticalGroup("Bars")]
        [BoxGroup("Bars/Bars")]
        [LabelWidth(90), LabelText("Distribution")]
        [SerializeField]
        private float sizeDistribution;

        //[VerticalGroup("Bars")]
        [BoxGroup("Bars/Size")]
        [MinMaxSlider(0, 20, showFields: true)]
        [HideLabel]
        [SerializeField] private Vector2 MinMaxBarSize;

        [EnumToggleButtons]
        [SerializeField]
        [TableColumnWidth(10)]
        private BarType barTypes;


        public int StartingScore => startingScore;
        public int EndingScore => endingScore;

        public float StartingSpeed => startingSpeed;
        public float EndingSpeed => endingSpeed;

        public float MaxSize { get => MinMaxBarSize.y; private set => MinMaxBarSize.y = value; }
        public float MinSize { get => MinMaxBarSize.x; private set => MinMaxBarSize.x = value; }
        public float SizeDistribution => sizeDistribution;
        public float AverageSize => averageSize;

        public BarType BarTypes => barTypes;
    }
}