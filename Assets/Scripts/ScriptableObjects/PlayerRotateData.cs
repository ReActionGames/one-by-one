using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRotateData", menuName = "Scriptable Objects/Player Rotate Data")]
public class PlayerRotateData : ScriptableObject
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float stopRotationSpeed;
    [SerializeField] private Ease stopingRotationEase;
    [SerializeField] private AnimationCurve rotationSmoothingCurve;
    [SerializeField] private Ease rotationSmoothingEase;
    [SerializeField] private Vector3 rotationInterval;

    public float RotationSpeed => rotationSpeed;

    public Ease StopingRotationEase => stopingRotationEase;

    public AnimationCurve RotationSmoothingCurve => rotationSmoothingCurve;

    public Ease RotationSmoothingEase => rotationSmoothingEase;

    public Vector3 RotationInterval => rotationInterval;

    public float StopRotationSpeed => stopRotationSpeed;
}