using Sirenix.OdinInspector;
using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(fileName = "Procedural Path Zone", menuName = "Scriptable Objects/Continuous/Procedural Path Zone")]
    public class ProceduralZone : ScriptableObject
    {
        [BoxGroup("Duration")]
        [BoxGroup("Duration/Score", Order = -2)]
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

        [BoxGroup("Bars")]
        [BoxGroup("Bars/Bars")]
        [LabelWidth(90)]
        [SerializeField]
        private float averageSize = 6;

        //[VerticalGroup("Bars")]
        [BoxGroup("Bars/Bars")]
        [LabelWidth(90), LabelText("Distribution")]
        [SerializeField]
        private float sizeDistribution = 2;

        //[VerticalGroup("Bars")]
        [BoxGroup("Bars/Size")]
        [MinMaxSlider(0, 20, showFields: true)]
        [HideLabel]
        [SerializeField] private Vector2 MinMaxBarSize = new Vector2(3, 10);

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