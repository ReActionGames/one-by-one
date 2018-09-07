using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundMulti : MonoBehaviour {

    [SerializeField] private float distance;
    [SerializeField] private float variation = 5;
    [SerializeField] private Ease ease;
    [SerializeField] private float height;

    private void OnEnable()
    {
        var levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBackgroundMove += ScrollBackground;
        }
    }

    private void OnDisable()
    {
        var levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBackgroundMove -= ScrollBackground;
        }
    }

    private void ScrollBackground(float duration, Ease easing)
    {
        float randomDistance = RandomExtensions.RandomGaussian(distance, variation);
        transform.DOMoveY(transform.position.y - randomDistance, duration, false).SetEase(ease).OnComplete(ResetPosition);
    }

    private void ResetPosition()
    {
        if (transform.position.y < -height)
        {
            transform.position += Vector3.up * height;
        }
    }
}
