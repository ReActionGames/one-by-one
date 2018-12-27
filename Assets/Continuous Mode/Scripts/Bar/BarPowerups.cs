using UnityEngine;

namespace Continuous
{
    public class BarPowerups : MonoBehaviour
    {
        [SerializeField] private PowerupPickup[] powerupPickups;
        [SerializeField] private Transform pickupPosition;

        public void SetupPowerup(PowerupType powerupType)
        {
            pickupPosition.Clear();

            if (powerupType == PowerupType.None)
                return;

            Instantiate(powerupPickups[(int)powerupType], pickupPosition);
        }
    }
}