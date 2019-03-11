using UnityEngine;

namespace Continuous
{
    public class BarPickups : MonoBehaviour
    {
        [SerializeField] private Pickups pickups;
        [SerializeField] private Transform pickupParent;
        [SerializeField] private float padding;
        
        public void SetupPickup(PickupType pickupType, float barSize, IPickupPositioner positioner)
        {
            pickupParent.DestroyChildren();

            if (pickupType == PickupType.None)
                return;
            if (pickupType == PickupType.Shield && HasMaxShields())
            {
                Debug.Log("Max shields!");
                return;
            }

            Transform pickup = pickups.GetPickup(pickupType);
            pickup.SetParent(pickupParent);
            positioner.PositionPickup(pickup, barSize, padding);
        }

        private static bool HasMaxShields()
        {
            return FindObjectOfType<ProjectileManager>().NumberOfProjectiles >= RemoteSettingsValues.MaxShields;
        }
    }
}