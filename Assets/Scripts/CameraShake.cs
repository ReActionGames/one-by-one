using DG.Tweening;
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

    private void Shake()
    {
        if (cameraShakeData.ShakePosition)
        {
            Camera.main.DOShakePosition(cameraShakeData.Duration, cameraShakeData.Strength, cameraShakeData.Vibrato, cameraShakeData.Randomness, cameraShakeData.FadeOut);
        }
        if (cameraShakeData.ShakeRotation)
        {
            Camera.main.DOShakeRotation(cameraShakeData.Duration, cameraShakeData.Strength, cameraShakeData.Vibrato, cameraShakeData.Randomness, cameraShakeData.FadeOut);
        }
    }
}