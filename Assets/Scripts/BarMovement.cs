using DG.Tweening;
using System;
using UnityEngine;

public class BarMovement : MonoBehaviour
{
    //[SerializeField] private float speed;
    [SerializeField] private Ease easing;

    [SerializeField] private Transform freeSpace;
    [SerializeField] private Transform start, end;

    public event Action OnStoppedMoving;

    private float direction = 1;
    private float positionPercentage = 0;
    private bool isMoving = true;
    private BarScaler scaler;

    private void Awake()
    {
        scaler = GetComponent<BarScaler>();
    }

    public void StartMoving(BarData data)
    {
        scaler.Scale(data.GetPsuedoRandomSize());
        freeSpace.position = start.position;
        freeSpace.gameObject.SetActive(true);
        freeSpace.DOMove(end.position, data.GetPsuedoRandomSpeed())
            .SetEase(easing)
            .SetLoops(-1, LoopType.Yoyo);
    }

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