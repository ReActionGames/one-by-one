using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CameraShakeData cameraShakeData;

    //private Camera cam;

    //private void Awake()
    //{
    //    cam = GetComponent<Camera>();
    //}

    private void OnEnable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnEdgeColliderHit += Shake;
        }
    }

    private void OnDisable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnEdgeColliderHit -= Shake;
        }
    }

    private void Shake()
    {
        Debug.Log("Shaking...");
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
