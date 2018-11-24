using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Continuous
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerProperties properties;
        [SerializeField] private Transform activePosition;
        [SerializeField] private PlayerVisibility visibility;
        [SerializeField] private Explodable exploder;
        [SerializeField] private ExplosionForce explosionForce;
        [SerializeField] private bool collide = true;

        private IMover movement;

        private void Awake()
        {
            movement = new PlayerMovement(transform, activePosition, properties.MovementProperties);
            collide = false;
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.GameStart, OnGameStart);
            EventManager.StartListening(EventNames.LookAheadCollision, LookAheadCollision);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventNames.GameStart, OnGameStart);
            EventManager.StopListening(EventNames.LookAheadCollision, LookAheadCollision);
        }

        private void OnGameStart(Message message)
        {
            collide = true;
            exploder.fragmentInEditor();
            DOVirtual.DelayedCall(properties.StartDelay, () => movement.StartMoving(properties.Speed));
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collide == false || collider.CompareTag("EdgeCollider") == false)
                return;

            EndGame();
        }

        private void EndGame()
        {
            movement.StopMoving();
            Explode();
            Hide();
            GameManager.Instance.EndGame();
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
            explosionForce.doExplosion(transform.position + (Vector3)Random.insideUnitCircle);
        }

        public void LookAheadCollision(Message message)
        {
            if (collide == false)
                return;

            Collider2D bar = (Collider2D)message.Data;

            transform.DOMoveY(bar.transform.position.y, 1f)
                .SetEase(Ease.InOutSine);
        }
    }
}