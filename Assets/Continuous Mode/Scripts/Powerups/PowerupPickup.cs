using System;
using UnityEngine;

namespace Continuous
{
    public class PowerupPickup : MonoBehaviour, ICollectible
    {
        public static event Action<PickupType> PowerupCollected;

        [SerializeField] private PickupType powerupType;

        public void Collect()
        {
            PowerupCollected?.Invoke(powerupType);
            Destroy(gameObject);
        }
    }
}