using UnityEngine;

namespace Continuous
{
    public class DoubleBarPickupPositioner : IPickupPositioner
    {
        public void PositionPickup(Transform powerup, float size, float padding)
        {
            // TODO: Position double bar pickup
            float maxDistanceFromCenter = (size / 2) - padding;

            float xPos = Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter);

            powerup.localPosition = Vector3.zero.With(x: xPos);
        }
    }
}