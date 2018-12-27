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
        [SerializeField] private Explodable exploder;
        [SerializeField] private ExplosionForce explosionForce;
        [SerializeField] private bool collide = true;

        private IMover movement;
        private ShieldPowerupComponent shield;
        private Vector3 startingPosition;
        private Tween startTween;

        private void Awake()
        {
            movement = new PlayerMovement(transform, activePosition, properties.MovementProperties);
            shield = GetComponent<ShieldPowerupComponent>();
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
            collide = false;
            exploder.fragmentInEditor();
            startTween = DOVirtual.DelayedCall(properties.StartDelay, () => { movement.StartMoving(properties.Speed); collide = true; });
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collide == false || collider.gameObject.layer != LayerMask.NameToLayer("EdgeCollider"))
                return;

            EndGame();
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collide == false || collider.CompareTag("CenterCollider") == false)
                return;

            //ScorePoint?.Invoke();
        }

        private void EndGame()
        {
            collide = false;
            startTween.Kill();
            movement.StopMoving();
            Explode();
            Hide();
            Die?.Invoke();
            GameManager.EndGame();
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
            if (collide == false)
                return;

            if (shield.Active)
            {
                shield.Use(hit);
                return;
            }

            transform.DOMoveY(hit.collider.transform.position.y, 1f)
                .SetEase(Ease.InOutSine);
        }
    }
}