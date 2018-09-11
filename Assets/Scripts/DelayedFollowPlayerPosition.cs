using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedFollowPlayerPosition : MonoBehaviour {
    [SerializeField] private float delay;

    private void OnEnable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnStartMoving += StartMoving;
        }
    }

    private void OnDisable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnStartMoving -= StartMoving;
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
