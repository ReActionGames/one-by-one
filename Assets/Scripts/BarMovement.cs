using DG.Tweening;
using System;
using UnityEngine;

public class BarMovement : MonoBehaviour
{
    //[SerializeField] private float speed;
    [SerializeField] private Ease easing;

    [SerializeField] private Transform freeSpace;
    [SerializeField] private Transform right, left, rightStart, leftStart;

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
        bool startOnRightSide = RandomExtensions.RandomBoolean();
        if (startOnRightSide)
        {
            StartOnRigthSide(data);
            return;
        }
        StartOnLeftSide(data);
    }

    private void StartOnLeftSide(BarData data)
    {
        freeSpace.position = leftStart.position;
        freeSpace.gameObject.SetActive(true);
        float speed = data.GetPsuedoRandomSpeed();

        freeSpace.DOMove(right.position, speed)
            .SetEase(easing)
            .OnComplete(() => freeSpace.DOMove(left.position, speed)
                                .SetEase(easing)
                                .SetLoops(-1, LoopType.Yoyo)
        );
    }

    private void StartOnRigthSide(BarData data)
    {
        freeSpace.position = rightStart.position;
        freeSpace.gameObject.SetActive(true);
        float speed = data.GetPsuedoRandomSpeed();

        freeSpace.DOMove(left.position, speed)
            .SetEase(easing)
            .OnComplete(() => freeSpace.DOMove(right.position, speed)
                                .SetEase(easing)
                                .SetLoops(-1, LoopType.Yoyo)
        );
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