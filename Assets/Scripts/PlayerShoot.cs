using DG.Tweening;
using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [CreateAssetMenu(fileName = "PlayerShootData", menuName = "Scriptable Objects/Player Shoot Data")]
    public class PlayerShootData : ScriptableObject
    {
        [SerializeField] private float waitTime;
        [SerializeField] private float duration;
        [SerializeField] private Ease easing;

        public float WaitTime => waitTime;

        public float Duration => duration;

        public Ease Easing => easing;
    }

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