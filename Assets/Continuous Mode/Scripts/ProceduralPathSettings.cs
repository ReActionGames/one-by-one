using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(fileName = "Procedural Path Settings", menuName = "Scriptable Objects/Continuous/Procedural Path Settings")]
    public class ProceduralPathSettings : ScriptableObject
    {
        [BoxGroup("Pickup Probabilities")]
        [SerializeField]
        private float shieldProbability = 0.05f;

        [PropertySpace(SpaceBefore = 25)]
        [LabelText("Zones")]
        [TableList(AlwaysExpanded = true, HideToolbar = false, ShowIndexLabels = true, CellPadding = 10, DrawScrollView = false, ShowPaging = true)]
        [SerializeField]
        private List<ProceduralZone> zones;

        public float ShieldProbability => shieldProbability;
        public List<ProceduralZone> Zones => zones;

        public ProceduralZone this[int index] => Zones[index];

        public ProceduralZone GetCurrentZone(int score)
        {
            var zone = zones.Where((z) => z.StartingScore <= score && z.EndingScore > score).First();
            Debug.Log(zone.name);
            return zone;
        }

        public void AddZone(ProceduralZone zone)
        {
            zones.Add(zone);
        }

        public void RemoveZone(ProceduralZone zone)
        {
            zones.Remove(zone);
        }
    }
}