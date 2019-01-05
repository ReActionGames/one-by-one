using DG.Tweening;
using System;
using UnityEngine;

namespace Continuous
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float repositionDuration;
        [SerializeField] private float fireSpeed;

        private new Collider2D collider;
        private Collider2D targetCollider;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        public void Reposition(Vector2 position)
        {
            transform.DOLocalMove(position, repositionDuration);
        }

        public void Fire(Vector2 point, Collider2D targetCollider)
        {
            this.targetCollider = targetCollider;
            collider.enabled = true;
            transform.parent = targetCollider.transform;

            var localPoint = targetCollider.transform.InverseTransformPoint(point);

            transform.DOLocalMove(localPoint, Vector2.Distance(transform.localPosition, localPoint) * fireSpeed)
                .SetEase(Ease.Linear);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.Equals(targetCollider) == false)
                return;

            Bar bar = collider.GetComponentInParent<Bar>();
            if (bar == null)
                return;

            transform.DOKill();
            bar.Hide();

            Destroy(gameObject);
        }
    }
}