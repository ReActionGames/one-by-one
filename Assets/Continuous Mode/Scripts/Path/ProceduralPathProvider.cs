using Sirenix.OdinInspector;
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
        [MinMaxSlider(0, 100, ShowFields = true)]
        [SerializeField] private Vector2 easyScoreRange;

        [MinMaxSlider(0, 100, ShowFields = true)]
        [SerializeField] private Vector2 meduimScoreRange;

        [MinMaxSlider(0, 100, ShowFields = true)]
        [SerializeField] private Vector2 hardScoreRange;

        private Queue<BarData> currentDeck = new Queue<BarData>();

        private void OnValidate()
        {
            easyScoreRange = easyScoreRange.FloorComponents();
            meduimScoreRange = meduimScoreRange.FloorComponents();
            hardScoreRange = hardScoreRange.FloorComponents();
        }

        public void Initialize()
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