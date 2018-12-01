using DG.Tweening;
using ReActionGames.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Continuous
{
    public class BarVisibility : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] spriteRenderers;
        [SerializeField] private float showDuration, hideDuration;
        [SerializeField] private Ease showEasing, hideEasing;

        private float originalAlphaValue;

        private void Awake()
        {
            originalAlphaValue = spriteRenderers[0].color.a;
        }

        [Button]
        public void Show()
        {
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.DOFade(originalAlphaValue, showDuration)
                    .SetEase(showEasing);
            }
        }

        public void ShowInstantly()
        {
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.color = sprite.color.With(a: originalAlphaValue);
            }
        }

        [Button]
        public void Hide()
        {
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.DOFade(0, hideDuration)
                    .SetEase(hideEasing);
            }
        }

        public void HideInstantly()
        {
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.color = sprite.color.With(a: 0);
            }
        }
    }
}