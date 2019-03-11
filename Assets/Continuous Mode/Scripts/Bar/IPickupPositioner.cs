using UnityEngine;

namespace Continuous
{
    public interface IPickupPositioner
    {
        void PositionPickup(Transform powerup, float size, float padding);
    }
}