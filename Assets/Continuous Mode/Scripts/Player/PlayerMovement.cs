using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class PlayerMovement : IMover
    {
        private Transform player;
        private float activeYPosition;
        private PlayerMovementProperties properties;

        public PlayerMovement(Transform player, Transform activePosition, PlayerMovementProperties movementProperties)
        {
            this.player = player;
            activeYPosition = activePosition.position.y;
            properties = movementProperties;
        }

        public void StartMoving(float speed)
        {
            player.DOMoveY(activeYPosition, properties.StartMovementDuration)
                .SetEase(properties.Easing);
        }

        public void StopMoving()
        {
            
        }
    }
}