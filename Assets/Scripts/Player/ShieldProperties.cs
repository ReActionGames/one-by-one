using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Properties", menuName = "Scriptable Objects/Shield Properties")]
public class ShieldProperties : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private Ease fadeEase;

    public float Duration => duration;
    public Ease ScaleEase => scaleEase;
    public Ease FadeEase => fadeEase;
}