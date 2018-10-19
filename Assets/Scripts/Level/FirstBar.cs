using DG.Tweening;
using UnityEngine;

public class FirstBar : MonoBehaviour
{
    [SerializeField] private Ease easing;

    [SerializeField] private Transform freeSpace;
    [SerializeField] private Transform exitPosition;

    private BarData data;

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

    public void SetData(BarData data)
    {
        this.data = data;
    }

    public void SetUp()
    {
        freeSpace.localPosition = Vector3.zero;
        freeSpace.gameObject.SetActive(true);
    }

    public void ResetObject()
    {
        Hide(false);
    }

    public void Hide(bool instant = false)
    {
        if (instant)
        {
            ResetBar();
            return;
        }

        Vector2 destination = exitPosition.position;

        float speed = data.ExitSpeed;

        freeSpace.DOMove(destination, speed)
            .SetEase(easing)
            .OnComplete(ResetBar);
    }

    private void ResetBar()
    {
        freeSpace.gameObject.SetActive(false);
    }
}