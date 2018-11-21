using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerProperties properties;

        private IMover movement;

        private void Awake()
        {
            movement = new PlayerMovement(GetComponent<Rigidbody2D>());
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