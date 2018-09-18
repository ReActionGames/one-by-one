using DG.Tweening;
using System;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float delay = 0f;
    [SerializeField] private PlayerRotateData playerRotateData;
    
    private Tween rotationTween;

    private void OnEnable()
    {
        Player player = GetComponentInParent<Player>();
        if (player)
        {
            player.OnStartIdle += StartRotating;
            player.OnStopIdle += StopRotating;
        }
    }

    private void OnDisable()
    {
        Player player = GetComponentInParent<Player>();
        if (player)
        {
            player.OnStartIdle -= StartRotating;
            player.OnStopIdle -= StopRotating;
        }
    }

    private void StartRotating()
    {
        rotationTween = transform.DORotate(playerRotateData.RotationInterval, playerRotateData.RotationSpeed)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(playerRotateData.RotationSmoothingEase)
            .SetDelay(delay * playerRotateData.RotationSpeed);
    }

    private void StopRotating()
    {
        rotationTween.Pause();
        transform.DORotate(Vector3.zero, playerRotateData.StopRotationSpeed)
            .SetEase(playerRotateData.StopingRotationEase)
            .SetDelay(delay * playerRotateData.StopRotationSpeed);
    }
}