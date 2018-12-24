using System;
using UnityEngine;

namespace Continuous
{
    public enum PowerupType
    {
        None = -1,
        Shield = 0
    }

    public class PowerupPickup : MonoBehaviour
    {
        public static event Action<PowerupType> PowerupCollected;

        [SerializeField] private PowerupType powerupType;

        public void Collect()
        {
            PowerupCollected?.Invoke(powerupType);
            Destroy(gameObject);
        }
    }
}