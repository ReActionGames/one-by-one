using DG.Tweening;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float variation = 5;
    [SerializeField] private Ease ease;
    [SerializeField] private float height;
    [SerializeField] private bool randomizeStartPosition = true;

    private void OnEnable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBackgroundMove += ScrollBackground;
        }
    }

    private void OnDisable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBackgroundMove -= ScrollBackground;
        }
    }

    private void Start()
    {
        if (randomizeStartPosition)
        {
            var randomAmount = Random.Range(0, height);
            transform.position += Vector3.down * randomAmount;
            ResetPosition();
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