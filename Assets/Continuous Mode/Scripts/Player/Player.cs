using System;
using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerProperties properties;
        [SerializeField] private Transform activePosition;
        [SerializeField] private PlayerVisibility visibility;
        [SerializeField] private Explodable exploder;
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
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventNames.GameStart, OnGameStart);
        }

        private void OnGameStart(Message message)
        {
            collide = true;
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

        private void Explode()
        {
            throw new NotImplementedException();
        }
    }
}