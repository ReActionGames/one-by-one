using DG.Tweening;
using ReActionGames.Extensions;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class SpriteVisibility : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] spriteRenderers;
        [SerializeField] private float showDuration, hideDuration, flashSpeed;
        [SerializeField] private Ease showEasing, hideEasing;

        private struct VisibleObject
        {
            public SpriteRenderer renderer;
            public float originalAlphaValue;

            public Color color
            {
                get => renderer.color;
                set => renderer.color = value;
            }

            public VisibleObject(SpriteRenderer renderer)
            {
                this.renderer = renderer;
                originalAlphaValue = renderer.color.a;
            }
        }

        private List<VisibleObject> visibleObjects;

        private void Awake()
        {
            visibleObjects = new List<VisibleObject>(spriteRenderers.Length);
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                visibleObjects.Add(new VisibleObject(renderer));
            }
        }
        [Button]
        public void Show()
        {
            foreach (VisibleObject visibleObject in visibleObjects)
            {
                visibleObject.renderer.DOFade(visibleObject.originalAlphaValue, showDuration)
                    .SetEase(showEasing);
            }
        }

        public void ShowInstantly()
        {
            foreach (VisibleObject visibleObject in visibleObjects)
            {
                visibleObject.renderer.color = visibleObject.color.With(a: visibleObject.originalAlphaValue);
            }
        }

        [Button]
        public void Flash()
        {
            Flash(flashSpeed);
        }

        public void Flash(float speed)
        {
            StartCoroutine(DoFlash(speed));
        }

        private IEnumerator DoFlash(float speed)
        {
            Show();
            yield return new WaitForSeconds(speed);
            Hide();
        }

        public void SetColor(Color color)
        {
            foreach (var sprite in visibleObjects)
            {
                sprite.renderer.color = color;
            }
        }

        [Button]
        public void Hide()
        {
            foreach (VisibleObject visibleObject in visibleObjects)
            {
                visibleObject.renderer.DOFade(0, hideDuration)
                    .SetEase(hideEasing);
            }
        }

        public void Hide(Action callback)
        {
            foreach (VisibleObject visibleObject in visibleObjects)
            {
                visibleObject.renderer.DOFade(0, hideDuration)
                    .SetEase(hideEasing)
                    .OnComplete(() => callback.Invoke());
            }
        }

        public void HideInstantly()
        {
            foreach (VisibleObject visibleObject in visibleObjects)
            {
                visibleObject.renderer.color = visibleObject.color.With(a: 0);
            }
        }
    }
}