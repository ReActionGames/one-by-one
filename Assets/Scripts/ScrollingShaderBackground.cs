using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingShaderBackground : MonoBehaviour {

    [SerializeField] private float distance;
    [SerializeField] private Ease ease;

    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
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

        float speed = material.GetFloat("_Speed");
        

        //transform.DOMoveY(transform.position.y - distance, duration, false).SetEase(ease).OnComplete(ResetPosition);
    }

}
