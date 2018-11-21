using UnityEngine;

namespace Continuous
{
    public class PlayerMovement : IMover
    {
        private Rigidbody2D rigidbody2D;

        public PlayerMovement(Rigidbody2D rigidbody2D)
        {
            this.rigidbody2D = rigidbody2D;
        }

        public void StartMoving(float speed)
        {
            rigidbody2D.velocity = Vector2.up * speed;
        }

        public void StopMoving()
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}