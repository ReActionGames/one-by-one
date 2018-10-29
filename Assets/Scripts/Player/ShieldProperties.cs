using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Properties", menuName = "Scriptable Objects/Shield Properties")]
public class ShieldProperties : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private float activateDuration;
    [SerializeField] private float pulseDelay;
    [SerializeField] private Ease scaleEase;
    [SerializeField] private Ease fadeEase;
    [SerializeField] private Ease activateScaleEase;
    [SerializeField] private int lifeSpan;

    public float Duration => duration;
    public float ActivateDuration => activateDuration;
    public float PulseDelay => pulseDelay;
    public Ease ScaleEase => scaleEase;
    public Ease FadeEase => fadeEase;
    public Ease ActivateScaleEase => activateScaleEase;
    public int CurrentLifeSpan { get; private set; }

    public void ResetLifeSpan()
    {
        CurrentLifeSpan = lifeSpan;
    }

    public void DecrementLifeSpan()
    {
        CurrentLifeSpan--;
    }
}