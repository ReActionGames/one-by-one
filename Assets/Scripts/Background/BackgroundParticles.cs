using DG.Tweening;
using UnityEngine;

public class BackgroundParticles : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 2;
    [SerializeField] private float variation = 0.2f;
    [SerializeField] private float startingSpeed = 0.1f;
    [SerializeField] private Ease ease;
    [SerializeField] private ParticleSystem system;

    private ParticleSystem.MainModule mainModule;

    private void Awake()
    {
        mainModule = system.main;
    }

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

    private void ScrollBackground(float duration, Ease easing)
    {
        float randomSpeed = RandomExtensions.RandomGaussian(maxSpeed, variation);
        ResetSpeed();

        Tween firstHalf = DOTween.To(UpdateSpeed, startingSpeed, randomSpeed, duration / 2)
            .SetEase(Ease.InExpo);
        Tween secondHalf = DOTween.To(UpdateSpeed, randomSpeed, startingSpeed, duration / 2)
            .SetEase(Ease.OutExpo);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(firstHalf)
            .Append(secondHalf)
            .OnComplete(ResetSpeed);
    }

    private void UpdateSpeed(float speed)
    {
        mainModule.simulationSpeed = speed;
    }

    private void ResetSpeed()
    {
        mainModule.simulationSpeed = startingSpeed;
    }
}