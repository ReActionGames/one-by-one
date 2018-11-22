using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(fileName = "Player Movement Properties", menuName = "Scriptable Objects/Continuous/Player Movement Properties")]
    public class PlayerMovementProperties : ScriptableObject
    {
        [SerializeField] private float startMovementDuration;
        [SerializeField] private Ease easing;

        public float StartMovementDuration => startMovementDuration;
        public Ease Easing => easing;

    }
}