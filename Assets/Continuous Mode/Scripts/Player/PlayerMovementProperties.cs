using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(fileName = "Player Movement Properties", menuName = "Scriptable Objects/Continuous/Player Movement Properties")]
    public class PlayerMovementProperties : ScriptableObject
    {
        [SerializeField] private float startMovementDuration;
        [SerializeField] private float restartMovementDuration;
        [SerializeField] private Ease easing;

        public float StartMovementDuration => startMovementDuration;
        public float RestartMovementDuration => restartMovementDuration;
        public Ease Easing => easing;
    }
}