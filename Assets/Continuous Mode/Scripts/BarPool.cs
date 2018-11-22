using System;
using System.Collections.Generic;
using System.Linq;
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

        public Bar GetNextBar()
        {
            Bar nextBar = preparedBars.Dequeue();
            activeBars.Enqueue(nextBar);
            return nextBar;
        }

        public void RecycleBars()
        {
            if (preparedBars.Count >= minimumNumberOfPreparedBars)
                return;

            float yPos = preparedBars.Last().transform.localPosition.y + 2;

            Bar bottomBar = activeBars.Dequeue();
            bottomBar.Prepare(yPos, ProceduralPathGenerator.GetBarData());
            preparedBars.Enqueue(bottomBar);
        }
    }
}