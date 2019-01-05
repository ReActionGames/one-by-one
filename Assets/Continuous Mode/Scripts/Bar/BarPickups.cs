using UnityEngine;

namespace Continuous
{
    public class BarPickups : MonoBehaviour
    {
        [SerializeField] private Pickups pickups;
        [SerializeField] private Transform pickupPosition;
        [SerializeField] private float padding;

        public void SetupPickup(PickupType pickupType, float barSize)
        {
            pickupPosition.DestroyChildren();

            if (pickupType == PickupType.None)
                return;
            if (pickupType == PickupType.Shield && FindObjectOfType<ProjectileManager>().NumberOfProjectiles >= RemoteSettingsValues.MaxShields)
            {
                Debug.Log("Max shields!");
                return;
            }

            Transform powerup = pickups.GetPickup(pickupType);
            powerup.SetParent(pickupPosition);
            RandomizePosition(powerup, barSize);
        }

        private void RandomizePosition(Transform powerup, float size)
        {
            float maxDistanceFromCenter = (size / 2) - padding;

            float xPos = Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter);

            powerup.localPosition = Vector3.zero.With(x: xPos);
        }
    }
}