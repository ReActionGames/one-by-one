using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMovement : MonoBehaviour {

    //[SerializeField] private float speed;
    [SerializeField] private Ease easing;
    [SerializeField] private Transform freeSpace;
    [SerializeField] private Transform start, end;

    public event Action OnStoppedMoving;

    private float direction = 1;
    private float positionPercentage = 0;
    private bool isMoving = true;
    
    [Button]
    public void StartMoving(BarData data)
    {
        freeSpace.localScale = new Vector3(data.GetPsuedoRandomSize(), freeSpace.localScale.y);
        freeSpace.position = start.position;
        freeSpace.gameObject.SetActive(true);
        freeSpace.DOMove(end.position, data.GetPsuedoRandomSpeed())
            .SetEase(easing)
            .SetLoops(-1, LoopType.Yoyo);
    }
    
    [Button]
    public void StopMoving()
    {
        freeSpace.DOPause();
        OnStoppedMoving?.Invoke();
    }

    public void Reset()
    {
        freeSpace.gameObject.SetActive(false);
    }
}
