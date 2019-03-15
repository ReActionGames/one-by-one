using UnityEngine;

namespace Continuous
{
    public class DoubleBarPickupPositioner : IPickupPositioner
    {
        public void PositionPickup(Transform powerup, float size, float padding)
        {
            float centerOffset = ((19 / 92f) * size) + (957 / 460f); // Predetermined formula (y = 19/92 * x + 957/460)
            if (RandomExtensions.RandomBoolean())
            {
                centerOffset = -centerOffset;
            }

            float adjustedSize = ((21 / 46f) * size) + (84 / 115f); // Predetermined formula (y = 21/46 * x + 84/115)
            float maxDistanceFromCenter = (adjustedSize / 2) - padding;

            float distanceFromCenter = Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter);

            powerup.localPosition = Vector3.zero.With(x: centerOffset + distanceFromCenter);
        }
    }
}