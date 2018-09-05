using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundMulti : MonoBehaviour {

    [SerializeField] private float distance;
    [SerializeField] private Ease ease;


    private void Awake()
    {
    }

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

        transform.DOMoveY(transform.position.y - distance, duration, false).SetEase(ease).OnComplete(ResetPosition);
    }

    private void ResetPosition()
    {
        //if (transform.position.y < -height)
        //{
        //    transform.position += Vector3.up * height;
        //}

    }
}
