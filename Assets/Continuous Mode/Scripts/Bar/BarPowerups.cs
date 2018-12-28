using UnityEngine;

namespace Continuous
{
    public class BarPowerups : MonoBehaviour
    {
        [SerializeField] private PowerupPickup[] powerupPickups;
        [SerializeField] private Transform pickupPosition;
        [SerializeField] private float padding;

        public void SetupPowerup(PowerupType powerupType, float size)
        {
            pickupPosition.Clear();

            if (powerupType == PowerupType.None)
                return;

            Transform powerup = Instantiate(powerupPickups[(int)powerupType], pickupPosition).transform;
            RandomizePosition(powerup, size);
        }

        private void RandomizePosition(Transform powerup, float size)
        {
            float maxDistanceFromCenter = (size / 2) - padding;

            float xPos = Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter);

            powerup.localPosition = powerup.localPosition.With(x: xPos);
        }
    }
}