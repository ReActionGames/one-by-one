using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class ProceduralPathProvider : MonoBehaviour, IPathProvider
    {
        [SerializeField] private PathDeck startingDeck;
        [SerializeField] private PathDeck[] easyDecks;
        [SerializeField] private PathDeck[] mediumDecks;
        [SerializeField] private PathDeck[] hardDecks;

        [Space]
        [MinMaxSlider(0, 100)]
        [SerializeField] private Vector2 easyScoreRange;
        [MinMaxSlider(0, 100)]
        [SerializeField] private Vector2 meduimScoreRange;
        [MinMaxSlider(0, 100)]
        [SerializeField] private Vector2 hardScoreRange;

        private int barIndex = 0;
        private Queue<BarData> currentDeck;

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
            currentDeck = startingDeck.GetDeck();
        }

        public BarData GetNextBar()
        {
            if (currentDeck.Count <= 0)
            {
                currentDeck = GetNextDeck();
            }

            BarData data = currentDeck.Dequeue();
            return data;
        }

        private Queue<BarData> GetNextDeck()
        {
            Queue<BarData> nextDeck;

            if (ScoreKeeper.Score < easyScoreRange.y) nextDeck = easyDecks.PickRandom().GetDeck();
            else if (ScoreKeeper.Score < meduimScoreRange.y) nextDeck = mediumDecks.PickRandom().GetDeck();
            else nextDeck = hardDecks.PickRandom().GetDeck();

            return nextDeck;
        }
    }
}