using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Continuous
{
    [CreateAssetMenu(fileName = "Pickups", menuName = "Scriptable Objects/Continuous/Pickups")]
    public class Pickups : ScriptableObject
    {
        [SerializeField] private Transform[] pickups;

        public Transform GetPickup(PickupType type)
        {
            switch (type)
            {
                case PickupType.Shield:
                    return Instantiate(pickups[0]);
                case PickupType.HighScore:
                    return Instantiate(pickups[1]);
                default:
                    return null;
            }
        }
    }
}