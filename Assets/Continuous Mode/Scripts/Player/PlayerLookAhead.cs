using System;
using UnityEngine;

namespace Continuous
{
    public class PlayerLookAhead : MonoBehaviour
    {
        public static event Action<Collider2D> LookAheadCollision;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("EdgeCollider") == false)
            {
                return;
            }
            LookAheadCollision?.Invoke(collider);
        }
    }
}