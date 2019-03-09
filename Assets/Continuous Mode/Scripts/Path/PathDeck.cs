using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Continuous/Procedural Path Deck")]
    public class PathDeck : ScriptableObject
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

            public BarTypeCard(BarType type = BarType.Normal)
            {
                this.type = type;
                this.count = 1;
            }
        }

        [Serializable]
        public class PickupCard : Card
        {
            [SerializeField] private PickupType type = PickupType.None;
            public PickupType Type => type;

            public PickupCard(PickupType type = PickupType.None)
            {
                this.type = type;
                this.count = 1;
            }
        }

        [Serializable]
        public class SizeCard : Card
        {
            [InlineProperty]
            [SerializeField] private SizeRange size = new SizeRange();
            public SizeRange Size => size;

            public float Value => UnityEngine.Random.Range(size.Min, size.Max);

            public SizeCard(SizeRange size)
            {
                this.size = size;
                this.count = 1;
            }

            public SizeCard()
            {
                this.size = SizeRange.Default;
                this.count = 1;
            }
        }

        public const int STANDARD_DECK_SIZE = 10;
        public const int MAX_DECK_SIZE = 100;

        [OnValueChanged("ResizeLists")]
        [Range(1, MAX_DECK_SIZE)]
        [SerializeField] private int deckSize = STANDARD_DECK_SIZE;
        [ToggleLeft]
        [SerializeField] private bool randomizeOrder = true;

        [Space]
        [SerializeField, ListDrawerSettings(Expanded = true)] private BarTypeCard[] barTypes = new BarTypeCard[2] { new BarTypeCard(BarType.Normal), new BarTypeCard(BarType.Double) };
        [SerializeField, ListDrawerSettings(Expanded = true)] private PickupCard[] pickups = new PickupCard[1] { new PickupCard() };
        [SerializeField, ListDrawerSettings(Expanded = true)] private SizeCard[] sizes = new SizeCard[1] { new SizeCard() };

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

        public Queue<BarData> GetDeck()
        {
            Queue<BarData> deck = new Queue<BarData>(deckSize);

            List<BarType> barTypesL = new List<BarType>(deckSize);
            foreach (BarTypeCard type in barTypes)
            {
                for (int i = 0; i < type.Count; i++)
                {
                    barTypesL.Add(type.Type);
                }
            }

            List<PickupType> pickupsL = new List<PickupType>(deckSize);
            foreach (PickupCard type in pickups)
            {
                for (int i = 0; i < type.Count; i++)
                {
                    pickupsL.Add(type.Type);
                }
            }

            List<float> sizesL = new List<float>(deckSize);
            foreach (var size in sizes)
            {
                for (int i = 0; i < size.Count; i++)
                {
                    sizesL.Add(size.Value);
                }
            }

            barTypesL.Shuffle();
            pickupsL.Shuffle();
            sizesL.Shuffle();

            for (int i = 0; i < deckSize; i++)
            {
                BarData data = new BarData(size: sizesL[i], powerupType: pickupsL[i], type: barTypesL[i]);
                deck.Enqueue(data);
            }

            return deck;
        }

        private static int GetTotalNumber(Card[] layer)
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