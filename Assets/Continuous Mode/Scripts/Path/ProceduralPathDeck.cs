using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Continuous/Procedural Path Deck")]
    public class ProceduralPathDeck : ScriptableObject
    {
        [Serializable]
        public class Card
        {
            [SerializeField] protected int count;
            public int Count { get => count; set => count = value; }
        }

        [Serializable]
        public struct SizeRange
        {
            [MinMaxSlider(0, 15, showFields: true)]
            [HideLabel]
            [SerializeField] private Vector2 size;

            public static SizeRange Default { get; } = new SizeRange(7, 9);

            public SizeRange(int min = 6, int max = 8)
            {
                size.x = min;
                size.y = max;
            }

            public float Min => size.x;
            public float Max => size.y;
        }

        [Serializable]
        public class BarTypeCard : Card
        {
            [SerializeField] private BarType type = BarType.Normal;
            public BarType Type => type;
        }

        [Serializable]
        public class PickupCard : Card
        {
            [SerializeField] private PickupType type = PickupType.None;
            public PickupType Type => type;
        }

        [Serializable]
        public class SizeCard : Card
        {
            [InlineProperty]
            [SerializeField] private SizeRange size = new SizeRange();
            public SizeRange Size => size;
        }

        public const int STANDARD_DECK_SIZE = 10;
        public const int MAX_DECK_SIZE = 100;

        [OnValueChanged("ResizeLists")]
        [Range(1, MAX_DECK_SIZE)]
        [SerializeField] private int deckSize = STANDARD_DECK_SIZE;

        [Space]
        [SerializeField] private BarTypeCard[] barTypes = new BarTypeCard[2];
        [SerializeField] private PickupCard[] pickups = new PickupCard[1];
        [SerializeField] private SizeCard[] sizes = new SizeCard[1];

        private void ResizeLists()
        {
            deckSize = Mathf.Clamp(deckSize, 1, MAX_DECK_SIZE);
            UpdateLayers();
        }

        [Button(ButtonSizes.Large)]
        private void UpdateLayers()
        {
            UpdateLayer(barTypes);
            UpdateLayer(pickups);
            UpdateLayer(sizes);
        }

        private void UpdateLayer(Card[] layer)
        {
            int count = GetTotalNumber(layer);
            if (count == deckSize) return;

            foreach (Card card in layer)
            {
                while (count < deckSize)
                {
                    card.Count++;
                    count = GetTotalNumber(layer);
                }
                if (count == deckSize) return;

                while (count > deckSize && card.Count > 0)
                {
                    card.Count--;
                    count = GetTotalNumber(layer);
                }
                if (count == deckSize) return;
            }
        }

        private int GetTotalNumber(Card[] layer)
        {
            int count = 0;
            foreach (Card card in layer)
            {
                count += card.Count;
            }
            return count;
        }
    }
}