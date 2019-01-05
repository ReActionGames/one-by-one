using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    [CreateAssetMenu(fileName = "Bar Movement Properties", menuName = "Scriptable Objects/Continuous/Bar Movement Properties")]
    public class BarMovementProperties : ScriptableObject
    {
        [SerializeField] private float leftXPosition, rightXPosition;
        [SerializeField] private Ease easing;

        public float LeftXPosition => leftXPosition;
        public float RightXPosition => rightXPosition;
        public Ease Easing => easing;
        public float DistanceBetweenPositions => Mathf.Abs(leftXPosition - rightXPosition);
    }
}