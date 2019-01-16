using DG.Tweening;
using System;
using UnityEngine;

namespace Continuous
{
    public class Player : MonoBehaviour
    {
        private enum CollisionState
        {
            Disabled,
            Enabled,
            MovingToCollision
        }

        public static event Action ScorePoint;

        public static event Action Die;

        [SerializeField] private PlayerProperties properties;
        [SerializeField] private Transform activePosition;
        [SerializeField] private Transform underCameraPosition;
        [SerializeField] private ObjectVisibility visibility;
        [SerializeField] private ProjectileManager projectileManager;
        [SerializeField] private Explodable exploder;
        [SerializeField] private ExplosionForce explosionForce;

        private IMover movement;

        private Vector3 startingPosition;
        private CollisionState collisionState = CollisionState.Disabled;
        private Tween startTween;

        private void Awake()
        {
            movement = new PlayerMovement(transform, activePosition, properties.MovementProperties);
            collisionState = CollisionState.Disabled;
            startingPosition = transform.position;
        }

        private void OnEnable()
        {
            PlayerLookAhead.LookAheadCollision += LookAheadCollision;

            GameManager.GameStart += OnGameStart;
            GameManager.GameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            PlayerLookAhead.LookAheadCollision -= LookAheadCollision;

            GameManager.GameStart -= OnGameStart;
            GameManager.GameRestart -= OnGameRestart;
        }

        private void OnGameStart()
        {
            StartGame();
        }

        private void OnGameRestart()
        {
            collisionState = CollisionState.Disabled;
            transform.position = underCameraPosition.position;
            visibility.Show();
            transform.DOMoveY(startingPosition.y, properties.MovementProperties.RestartMovementDuration)
                .SetEase(properties.MovementProperties.Easing);
            StartGame();
        }

        private void StartGame()
        {
            collisionState = CollisionState.Enabled;
            exploder.fragmentInEditor();
            startTween = DOVirtual.DelayedCall(properties.StartDelay, () => { movement.StartMoving(properties.Speed);});
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collisionState == CollisionState.Disabled) return;

            if (collisionState == CollisionState.Enabled)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("MovingBar")) EndGame();
            }
            if (collisionState == CollisionState.MovingToCollision)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("EdgeCollider")) EndGame();
            }
        }

        private void EndGame()
        {
            collisionState = CollisionState.Disabled;
            startTween.Kill();
            movement.StopMoving();
            Explode();
            Hide();
            Die?.Invoke();
            GameManager.FinishEndGame();
        }

        private void Hide()
        {
            collisionState = CollisionState.Disabled;
            visibility.Hide();
        }

        private void Explode()
        {
            exploder.explode();
            explosionForce.doExplosion(transform.position + (Vector3)UnityEngine.Random.insideUnitCircle);
        }

        public void LookAheadCollision(RaycastHit2D hit)
        {
            if (collisionState == CollisionState.MovingToCollision)
                return;

            if (projectileManager.ShootProjectile(hit))
            {
                return;
            }

            collisionState = CollisionState.MovingToCollision;
            startTween.Kill();
            GameManager.StartEndGame();
            transform.DOMoveY(hit.collider.transform.position.y, 1f)
                .SetEase(Ease.InOutSine);
        }
    }
}