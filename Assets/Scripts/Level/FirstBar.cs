using DG.Tweening;
using UnityEngine;

public class FirstBar : MonoBehaviour, IResetable
{
    [SerializeField] private Ease easing;

    [SerializeField] private Transform freeSpace;
    [SerializeField] private Transform exitPosition;

    private BarData data;

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