using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private Ease easing;
    [SerializeField] private Transform white;
    [SerializeField] private Transform start, end;

    public event Action OnStoppedMoving;

    private float direction = 1;
    private float positionPercentage = 0;
    private bool isMoving = true;
    
    [Button]
    public void StartMoving()
    {
        white.position = start.position;
        white.DOMove(end.position, speed)
            .SetEase(easing)
            .SetLoops(-1, LoopType.Yoyo);
    }
    
    [Button]
    public void StopMoving()
    {
        white.DOPause();
        OnStoppedMoving?.Invoke();
    }
}
