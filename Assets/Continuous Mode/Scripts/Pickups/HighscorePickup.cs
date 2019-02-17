using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class HighscorePickup : MonoBehaviour, ICollectible
    {
        [SerializeField] private float floatSpeed = 1;

        public void Collect()
        {
            transform.parent = null;

            Vector2 playerPos = FindObjectOfType<Player>().transform.position;
            float duration = Vector2.Distance(transform.position, playerPos) * floatSpeed * 0.1f;

            transform.DOMove(playerPos, duration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}