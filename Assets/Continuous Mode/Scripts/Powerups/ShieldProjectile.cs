using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class ShieldProjectile : MonoBehaviour
    {
        public static event Action HitBar;

        [SerializeField] private float speed;

        private Transform parent;
        private Vector2 localPosition;
        private new Collider2D collider;
        private Collider2D targetCollider;

        private void Awake()
        {
            parent = transform.parent;
            localPosition = transform.localPosition;
            collider = GetComponent<CircleCollider2D>();
            collider.enabled = false;
        }

        private void OnEnable()
        {
            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
        }

        private void OnDisable()
        {
            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
        }

        private void OnGameStartOrRestart()
        {
            ResetSelf();
        }

        public void Fire(Vector2 target, Collider2D targetCollider)
        {
            this.targetCollider = targetCollider;
            collider.enabled = true;
            transform.parent = null;
            transform.DOMove(target, Vector2.Distance(transform.position, target) * speed)
                .SetEase(Ease.Linear);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.Equals(targetCollider) == false)
                return;

            BarController bar = collider.GetComponentInParent<BarController>();
            if (bar == null)
                return;

            transform.DOKill();
            bar.Hide();
            ResetSelf();
            HitBar?.Invoke();
        }

        public void ResetSelf()
        {
            collider.enabled = false;
            transform.parent = parent;
            transform.localPosition = localPosition;
        }
    }
}