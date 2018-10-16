using DG.Tweening;
using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private Transform topOfScreen;
    [SerializeField] private PlayerShootData playerShootData;

    public event Action OnStartMoving;

    public event Action OnDoneMoving;

    private void OnEnable()
    {
        Player player = GetComponentInParent<Player>();
        if (player)
        {
            player.OnStartMoving += ShootUpToTopOfScreen;
        }
    }

    private void OnDisable()
    {
        Player player = GetComponentInParent<Player>();
        if (player)
        {
            player.OnStartMoving -= ShootUpToTopOfScreen;
        }
    }

    public void ShootUpToTopOfScreen()
    {
        OnStartMoving?.Invoke();
        transform.DOMove(topOfScreen.position, playerShootData.Duration)
            .SetDelay(playerShootData.WaitTime + delay * playerShootData.Duration)
            .SetEase(playerShootData.Easing)
            .OnComplete(() => OnDoneMoving?.Invoke());
    }
}