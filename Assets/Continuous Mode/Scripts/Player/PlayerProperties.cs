using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(fileName = "Player Properties", menuName = "Scriptable Objects/Continuous/Player Properties")]
    public class PlayerProperties : ScriptableObject
    {
        [SerializeField] private float startDelay;
        [SerializeField] private float speed;
        [SerializeField] private PlayerMovementProperties movementProperties;

        public float StartDelay => startDelay;
        public float Speed => speed;
        public PlayerMovementProperties MovementProperties => movementProperties;
    }
}