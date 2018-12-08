﻿using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    public class PlayerLookAhead : MonoBehaviour
    {
        [SerializeField] private float diameter, distance;
        [SerializeField] private LayerMask mask;

        public static event Action<Collider2D> LookAheadCollision;
        public static event Action ScorePoint;

        //private void OnTriggerEnter2D(Collider2D collider)
        //{
        //    if (collider.CompareTag("EdgeCollider") == false)
        //    {
        //        return;
        //    }
        //    LookAheadCollision?.Invoke(collider);
        //}

        private bool hitBar = false;

        private void OnEnable()
        {
            PathController.BarPlaced += LookAhead;
        }

        private void OnDisable()
        {
            PathController.BarPlaced -= LookAhead;
        }

        private void LookAhead()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, diameter/2, Vector2.up, distance, mask);
            //if (hit.collider == null)
            //    return;
            if(hit.collider == null)
            {
                ScorePoint?.Invoke();
                hitBar = false;
                return;
            }
            hitBar = true;
            LookAheadCollision?.Invoke(hit.collider);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (hitBar)
                Gizmos.color = Color.red;

            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + (distance / 4)), new Vector3(diameter, distance / 2));
        }

#if UNITY_EDITOR
        [Button]
        private void Check()
        {
            PathController.InvokeBarPlacedEvent();
        }
#endif
    }
}