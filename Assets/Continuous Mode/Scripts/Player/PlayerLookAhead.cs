using System;
using UnityEngine;

namespace Continuous
{
    public class PlayerLookAhead : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            //Debug.Log("look ahead");
            if(collider.CompareTag("EdgeCollider") == false)
            {
                return;
            }
            EventManager.TriggerEvent(EventNames.LookAheadCollision, new Message(collider));
        }
    }
}