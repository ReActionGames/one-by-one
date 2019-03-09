using System;
using UnityEngine;

namespace Continuous
{
    public class ProceduralPathProvider : MonoBehaviour, IPathProvider
    {
        [SerializeField] private ProceduralPathDeck[] decks;

        private int barIndex = 0;

        private void OnEnable()
        {
            GameManager.GameStartOrRestart += SetUp;
        }

        private void OnDisable()
        {
            GameManager.GameStartOrRestart -= SetUp;
        }

        private void SetUp()
        {
            barIndex = 0;
        }

        public BarData GetNextBar()
        {
            BarData data = new BarData();

            barIndex++;
            return data;
        }
    }
}