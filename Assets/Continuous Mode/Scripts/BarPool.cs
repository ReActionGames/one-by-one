using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class BarPool : MonoBehaviour
    {
        [SerializeField] private int numberOfBars = 20;
        [SerializeField] private int minimumNumberOfPreparedBars = 5;
        [SerializeField] private Bar prefab;
        [SerializeField] private Transform barParent;

        private Queue<Bar> preparedBars;
        private Queue<Bar> activeBars;
        private Bar[] allBars;

        private void Awake()
        {
            allBars = new Bar[numberOfBars];
            preparedBars = new Queue<Bar>(numberOfBars);
            activeBars = new Queue<Bar>(numberOfBars);
        }

        public void PreWarm()
        {
            float yPos = 0;
            for (int i = 0; i < numberOfBars; i++)
            {
                Bar bar = Instantiate(prefab, barParent);
                bar.Prepare(yPos, ProceduralPathGenerator.GetBarData());
                preparedBars.Enqueue(bar);
                allBars[i] = bar;
                yPos += 2;
            }
        }


    }
}