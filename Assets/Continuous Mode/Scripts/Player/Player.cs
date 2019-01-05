using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    public class Player : MonoBehaviour
    {
        public static event Action ScorePoint;

        public static event Action Die;

        [SerializeField] private PlayerProperties properties;
        [SerializeField] private Transform activePosition;
        [SerializeField] private Transform underCameraPosition;
        [SerializeField] private ObjectVisibility visibility;
        [SerializeField] private ProjectileManager projectileManager;
        [SerializeField] private Explodable exploder;
        [SerializeField] private ExplosionForce explosionForce;
        [SerializeField] private bool collide = true;

        private IMover movement;

        //private ShieldPowerupComponent shield;
        private Vector3 startingPosition;

        private Tween startTween;
        private bool movingToCollision = false;

        private void Awake()
        {
            movement = new PlayerMovement(transform, activePosition, properties.MovementProperties);
            //shield = GetComponent<ShieldPowerupComponent>();
            collide = false;
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
            collide = false;
            transform.position = underCameraPosition.position;
            visibility.Show();
            transform.DOMoveY(startingPosition.y, properties.MovementProperties.RestartMovementDuration)
                .SetEase(properties.MovementProperties.Easing);
            StartGame();
        }

        private void StartGame()
        {
            collide = true;
            movingToCollision = false;
            exploder.fragmentInEditor();
            startTween = DOVirtual.DelayedCall(properties.StartDelay, () => { movement.StartMoving(properties.Speed);/* collide = true;*/ });
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collide == false || !IsBar(collider))
                return;

            EndGame();
        }

        private bool IsBar(Collider2D collider)
        {
            return collider.gameObject.layer == LayerMask.NameToLayer("EdgeCollider") || collider.gameObject.layer == LayerMask.NameToLayer("MovingBar");
        }

        private void EndGame()
        {
            collide = false;
            startTween.Kill();
            movement.StopMoving();
            Explode();
            Hide();
            Die?.Invoke();
            GameManager.FinishEndGame();
        }

        private void Hide()
        {
            collide = false;
            visibility.Hide();
        }

        private void Show()
        {
            collide = true;
            visibility.Show();
        }

        [Button]
        private void Explode()
        {
            exploder.explode();
            explosionForce.doExplosion(transform.position + (Vector3)UnityEngine.Random.insideUnitCircle);
        }

        public void LookAheadCollision(RaycastHit2D hit)
        {
            if (collide == false || movingToCollision == true)
                return;

            if (projectileManager.ShootProjectile(hit))
            {
                return;
            }

            movingToCollision = true;
            startTween.Kill();
            GameManager.StartEndGame();
            transform.DOMoveY(hit.collider.transform.position.y, 1f)
                .SetEase(Ease.InOutSine);
        }
    }
}