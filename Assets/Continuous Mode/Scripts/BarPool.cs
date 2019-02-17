using Sirenix.OdinInspector;
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
        [ReadOnly]
        [SerializeField] private Bar[] allBars = null;

        private Queue<Bar> preparedBars;
        private Queue<Bar> activeBars;
        private IMover mover;

        private void Awake()
        {
            preparedBars = new Queue<Bar>(numberOfBars);
            activeBars = new Queue<Bar>(numberOfBars);
        }

        private void Start()
        {
            HideAllBars(true);
        }

        public void PreWarm(Transform parent)
        {
            if (allBars == null || allBars.Length <= 0)
            {
                InstantiateBars(numberOfBars, parent);
            }

            float yPos = 0;

            preparedBars.Clear();
            activeBars.Clear();

            foreach (Bar bar in allBars)
            {
                bar.transform.SetParent(parent);
                bar.SetYPosition(yPos);
                bar.HideInstantly();
                preparedBars.Enqueue(bar);
                yPos += 2;
            }
        }

        private void InstantiateBars(int numberOfBars, Transform parent)
        {
            allBars = new Bar[numberOfBars];
            for (int i = 0; i < numberOfBars; i++)
            {
                Bar bar = Instantiate(prefab, parent);
                allBars[i] = bar;
            }
        }

        public void HideAllBars(bool instant = false)
        {
            foreach (Bar bar in allBars)
            {
                if (instant)
                    bar.HideInstantly();
                else
                    bar.Hide();
            }
        }

        public Bar GetNextBar()
        {
            Bar nextBar = preparedBars.Dequeue();
            nextBar.Prepare(ProceduralPathGenerator.GetBarData(ScoreKeeper.Score));
            activeBars.Enqueue(nextBar);
            return nextBar;
        }

        public void RecycleBars()
        {
            if (preparedBars.Count >= minimumNumberOfPreparedBars)
                return;

            float yPos = preparedBars.Last().transform.localPosition.y + 2;

            Bar bottomBar = activeBars.Dequeue();
            bottomBar.SetYPosition(yPos);
            bottomBar.Prepare(ProceduralPathGenerator.GetBarData(ScoreKeeper.Score));
            preparedBars.Enqueue(bottomBar);
        }

#if UNITY_EDITOR

        public void PreWarmInEditor(Transform barPoolParent)
        {
            DeleteBars();
            InstantiateBars(numberOfBars, barPoolParent);
        }

        private void DeleteBars()
        {
            if (allBars == null) return;
            for (int i = 0; i < allBars.Length; i++)
            {
                Bar bar = allBars[i];
                allBars[i] = null;
                if (bar != null)
                    DestroyImmediate(bar.gameObject);
            }

            allBars = null;
        }

#endif
    }
}