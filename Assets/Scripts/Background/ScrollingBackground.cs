using DG.Tweening;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float variation = 5;
    [SerializeField] private Ease ease;
    [SerializeField] private float height;
    [SerializeField] private bool randomizeStartPosition = true;

    [Space]
    [SerializeField] private Ease mainMenuEase;

    [SerializeField] private float mainMenuSpeed;

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
            float randomAmount = UnityEngine.Random.Range(0, height);
            transform.position += Vector3.down * randomAmount;
            ResetPosition();
        }
        MainMenuScroll();
    }

    private void ScrollBackground(float duration, Ease easing)
    {
        float randomDistance = RandomExtensions.RandomGaussian(distance, variation);
        transform.DOKill();
        transform.DOMoveY(transform.position.y - randomDistance, duration, false).SetEase(ease).OnComplete(ResetPosition);
    }

    private void MainMenuScroll()
    {
        transform.DOMoveY(transform.position.y - height, mainMenuSpeed)
            .SetEase(mainMenuEase)
            .OnComplete(RepeatMainMenuScroll);
    }

    private void RepeatMainMenuScroll()
    {
        ResetPosition();
        MainMenuScroll();
    }

    private void ResetPosition()
    {
        if (transform.position.y < -height)
        {
            transform.position += Vector3.up * height;
        }
    }
}