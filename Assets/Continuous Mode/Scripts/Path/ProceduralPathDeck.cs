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
        public struct SizeRange
        {
            public int min;
            public int max;

            public static SizeRange Default { get; } = new SizeRange(7, 9);

            public SizeRange(int min = 6, int max = 8)
            {
                this.min = min;
                this.max = max;
            }
        }

        [Serializable]
        public class BarTypeLayer
        {
            [Serializable]
            public class Card
            {
                [SerializeField] protected BarType value;
                [SerializeField] protected int count;

                public BarType Value => value;
                public int Count { get => count; set => count = value; }

                public Card(BarType value = default, int count = 1)
                {
                    this.value = value;
                    this.count = count;
                }
            }

            //[ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
            [SerializeField] private List<Card> cards = new List<Card>();
            
            public int totalSize
            {
                get
                {
                    int count = 0;
                    foreach (Card card in cards)
                    {
                        count += card.Count;
                    }
                    return count;
                }
            }

            public BarTypeLayer(int size)
            {
                cards.Add(new Card(BarType.Normal));
                cards.Add(new Card(BarType.Double));
            }
            
            public void UpdateCards(int size)
            {
                if (totalSize == size) return;

                for (int i = 0; i < cards.Count; i++)
                {
                    while (totalSize < size)
                        cards[i].Count++;
                    while (totalSize < size && cards[i].Count > 0)
                        cards[i].Count--;
                    if (totalSize == size) return;
                }
            }
        }

        [Serializable]
        public class PickupLayer
        {
            [Serializable]
            public class Card
            {
                [SerializeField] protected PickupType value;
                [SerializeField] protected int count;

                public PickupType Value => value;
                public int Count { get => count; set => count = value; }

                public Card(PickupType value = default, int count = 1)
                {
                    this.value = value;
                    this.count = count;
                }
            }

            //[ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
            [SerializeField] private List<Card> cards = new List<Card>();


            public int totalSize
            {
                get
                {
                    int count = 0;
                    foreach (Card card in cards)
                    {
                        count += card.Count;
                    }
                    return count;
                }
            }

            public PickupLayer(int size)
            {
                cards.Add(new Card(PickupType.None));
                UpdateCards(size);
            }


            public void UpdateCards(int size)
            {
                if (totalSize == size) return;

                for (int i = 0; i < cards.Count; i++)
                {
                    while (totalSize < size)
                        cards[i].Count++;
                    while (totalSize < size && cards[i].Count > 0)
                        cards[i].Count--;
                    if (totalSize == size) return;
                }
            }
        }

        [Serializable]
        public class SizeLayer
        {
            [Serializable]
            public class Card
            {
                [SerializeField] protected SizeRange value;
                [SerializeField] protected int count;

                public SizeRange Value => value;
                public int Count { get => count; set => count = value; }

                public Card(SizeRange value = default, int count = 1)
                {
                    this.value = value;
                    this.count = count;
                }
            }

            //[ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
            [SerializeField] private List<Card> cards = new List<Card>();


            public int totalSize
            {
                get
                {
                    int count = 0;
                    foreach (Card card in cards)
                    {
                        count += card.Count;
                    }
                    return count;
                }
            }

            public SizeLayer(int size)
            {
                cards.Add(new Card(SizeRange.Default));
                UpdateCards(size);
            }


            public void UpdateCards(int size)
            {
                if (totalSize == size) return;

                for (int i = 0; i < cards.Count; i++)
                {
                    while (totalSize < size)
                        cards[i].Count++;
                    while (totalSize < size && cards[i].Count > 0)
                        cards[i].Count--;
                    if (totalSize == size) return;
                }
            }
        }

        public const int STANDARD_DECK_SIZE = 10;
        public const int MAX_DECK_SIZE = 100;

        [OnValueChanged("ResizeLists")]
        [Range(1, MAX_DECK_SIZE)]
        [SerializeField] private int deckSize = STANDARD_DECK_SIZE;

        [Space]
        //[SerializeField] private List<BarType> barTypes = new List<BarType>(STANDARD_DECK_SIZE);
        //[SerializeField] private BarType[] barTypes = new BarType[STANDARD_DECK_SIZE];
        [SerializeField] private BarTypeLayer barTypes = new BarTypeLayer(STANDARD_DECK_SIZE);
        [SerializeField] private PickupLayer pickups = new PickupLayer(STANDARD_DECK_SIZE);
        [SerializeField] private SizeLayer sizes = new SizeLayer(STANDARD_DECK_SIZE);

        private void ResizeLists()
        {
            deckSize = Mathf.Clamp(deckSize, 1, MAX_DECK_SIZE);
            UpdateLayers();
        }

        [Button(ButtonSizes.Large)]
        private void UpdateLayers()
        {
            barTypes.UpdateCards(deckSize);
            pickups.UpdateCards(deckSize);
            sizes.UpdateCards(deckSize);
        }
    }
}