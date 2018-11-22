using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerProperties properties;
        [SerializeField] private Transform activePosition;

        private IMover movement;

        private void Awake()
        {
            movement = new PlayerMovement(transform, activePosition, properties.MovementProperties);
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
            DOVirtual.DelayedCall(properties.StartDelay, () => movement.StartMoving(properties.Speed));
        }
    }
}