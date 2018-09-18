using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedFollowPlayerRotation : MonoBehaviour {
    [SerializeField] private float delay;

    private void OnEnable()
    {
        PlayerRotate player = FindObjectOfType<PlayerRotate>();
        if (player)
        {
            //player.OnStartRotating += StartMoving;
        }
    }

    private void OnDisable()
    {
        PlayerRotate player = FindObjectOfType<PlayerRotate>();
        if (player)
        {
            //player.OnStartRotating -= StartMoving;
        }
    }

    private void StartMoving(float delay, Vector2 target, float duration, Ease ease)
    {
        transform.parent = null;
        transform.DOMove(target, duration)
            .SetDelay(delay + this.delay)
            .SetEase(ease)
            .OnComplete(() => transform.parent = FindObjectOfType<Player>().transform);
    }
}
