using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerShootData", menuName = "Scriptable Objects/Player Shoot Data")]
public class PlayerShootData : ScriptableObject
{
    [SerializeField] private float waitTime;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;

    public float WaitTime => waitTime;

    public float Duration => duration;

    public Ease Easing => easing;
}