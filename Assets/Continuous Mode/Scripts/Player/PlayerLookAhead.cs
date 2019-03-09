using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    public class PlayerLookAhead : MonoBehaviour
    {
        [SerializeField] private float diameter, distance;
        [SerializeField] private LayerMask mask;

        public static event Action<RaycastHit2D> LookAheadCollision;

        public static event Action ScorePoint;

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
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, diameter / 2, Vector2.up, distance, mask);
        
            CheckForCollectables(hits);

            RaycastHit2D barHit = CheckForBars(hits);
            if (barHit.collider == null)
            {
                hitBar = false;
                ScorePoint?.Invoke();
            }
            else
            {
                hitBar = true;
                LookAheadCollision?.Invoke(barHit);
            }
        }

        private void CheckForCollectables(RaycastHit2D[] hits)
        {
            foreach (RaycastHit2D hit in hits)
            {
                ICollectible collectable = hit.collider.GetComponent<ICollectible>();
                if (collectable != null)
                {
                    collectable.Collect();
                    continue;
                }
            }
        }

        private RaycastHit2D CheckForBars(RaycastHit2D[] hits)
        {
            foreach (RaycastHit2D hit in hits)
            {
                BarController bar = hit.collider.GetComponentInParent<BarController>();
                if (bar == null) continue;

                if (bar.state == BarController.State.Active)
                {
                    return hit;
                }
            }
            return default;
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