using UnityEngine;

[CreateAssetMenu(fileName = "CameraShakeData", menuName = "Scriptable Objects/Camera Shake Data")]
public class CameraShakeData : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private EZCameraShake.CameraShakeInstance shakeInstance;

    public float Duration => duration;
    public EZCameraShake.CameraShakeInstance ShakeInstance => shakeInstance;
}