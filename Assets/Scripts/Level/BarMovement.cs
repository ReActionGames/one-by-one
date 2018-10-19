using DG.Tweening;
using System;
using UnityEngine;

public class BarMovement : MonoBehaviour
{
    [SerializeField] private Ease easing;

    [SerializeField] private Transform freeSpace;
    [SerializeField] private Transform right, left, rightStart, leftStart;

    public event Action OnStoppedMoving;

    private BarScaler scaler;
    private BarData data;
    private bool startOnRightSide;

    private void Awake()
    {
        scaler = GetComponent<BarScaler>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnExitState += OnExitState;
    }

    private void OnExitState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.End)
        {
            ResetObject();
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnExitState -= OnExitState;
    }

    public void StartMoving(BarData data)
    {
        this.data = data;
        scaler.Scale(data.GetPsuedoRandomSize());
        startOnRightSide = RandomExtensions.RandomBoolean();
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

    public void ResetObject()
    {
        Hide();
    }

    public void Hide(bool instant = false)
    {
        if (instant)
        {
            ResetBar();
            return;
        }

        Vector2 destination = rightStart.position;
        if (startOnRightSide == false)
        {
            destination = leftStart.position;
        }

        float speed = data.ExitSpeed;

        freeSpace.DOMove(destination, speed)
            .SetEase(easing)
            .OnComplete(ResetBar);
    }

    public void StopMoving()
    {
        freeSpace.DOPause();
        OnStoppedMoving?.Invoke();
    }

    private void ResetBar()
    {
        freeSpace.gameObject.SetActive(false);
    }
}