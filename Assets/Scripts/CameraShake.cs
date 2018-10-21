using EZCameraShake;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CameraShakeData cameraShakeData;

    private void OnEnable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnDie += Shake;
        }
    }

    private void OnDisable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnDie -= Shake;
        }
    }

    [Button]
    private void Shake()
    {
        var shakeInstance = cameraShakeData.ShakeInstance;
        CameraShaker.Instance.ShakeOnce(shakeInstance.Magnitude, shakeInstance.Roughness,
            cameraShakeData.Duration / 2, cameraShakeData.Duration / 2,
            shakeInstance.PositionInfluence, shakeInstance.RotationInfluence);
    }
}