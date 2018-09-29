using UnityEngine;

[CreateAssetMenu(fileName = "CameraShakeData", menuName = "Scriptable Objects/Camera Shake Data")]
public class CameraShakeData : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private Vector3 strength;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private float randomness = 90;
    [SerializeField] private bool fadeOut = true;
    [SerializeField] private bool shakeRotation;
    [SerializeField] private bool shakePosition;

    public float Duration => duration;
    public Vector3 Strength => strength;
    public int Vibrato => vibrato;
    public float Randomness => randomness;
    public bool FadeOut => fadeOut;
    public bool ShakeRotation => shakeRotation;
    public bool ShakePosition => shakePosition;
}