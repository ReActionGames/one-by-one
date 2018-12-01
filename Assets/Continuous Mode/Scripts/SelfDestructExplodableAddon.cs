using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class SelfDestructExplodableAddon : ExplodableAddon
    {
        [SerializeField] private float delay = 5;

        public override void OnFragmentsGenerated(List<GameObject> fragments)
        {
            foreach (GameObject fragment in fragments)
            {
                var selfDestruct = fragment.AddComponent<SelfDestruct>();
                selfDestruct.delay = delay;
            }
        }
    }
}