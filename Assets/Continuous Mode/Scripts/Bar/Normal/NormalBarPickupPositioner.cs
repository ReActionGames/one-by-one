using UnityEngine;

namespace Continuous
{
    public class NormalBarPickupPositioner : IPickupPositioner
    {
        public void PositionPickup(Transform powerup, float size, float padding)
        {
            float maxDistanceFromCenter = (size / 2) - padding;

            float xPos = Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter);

            powerup.localPosition = Vector3.zero.With(x: xPos);
        }
    }
}