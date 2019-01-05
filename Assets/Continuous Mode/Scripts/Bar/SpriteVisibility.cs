using DG.Tweening;
using ReActionGames.Extensions;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class SpriteVisibility : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] spriteRenderers;

        //[SerializeField] private bool autoFindAllSpriteRenderers = true;
        [SerializeField] private float showDuration, hideDuration;

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

        //public void UpdateRendererList()
        //{
        //    if (autoFindAllSpriteRenderers)
        //    {
        //        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        //    }

        //    if (spriteRenderers.Length != visibleObjects.Count)
        //    {
        //        AddNewRenderers();
        //    }

        //    //originalAlphaValues = new float[spriteRenderers.Length];
        //    //for (int i = 0; i < spriteRenderers.Length; i++)
        //    //{
        //    //    originalAlphaValues[i] = spriteRenderers[i].color.a;
        //    //}
        //}

        //private void AddNewRenderers()
        //{
        //    visibleObjects.Capacity = spriteRenderers.Length;
        //    foreach (SpriteRenderer renderer in spriteRenderers)
        //    {
        //        if(ContainsRenderer(renderer) == false)
        //        {
        //            visibleObjects.Add(new VisibleObject(renderer));
        //        }
        //    }
        //    foreach (VisibleObject visibleObject in visibleObjects)
        //    {
        //        if (visibleObject.renderer == null)
        //            visibleObjects.Remove(visibleObject);
        //    }
        //}

        //private bool ContainsRenderer(SpriteRenderer renderer)
        //{
        //    int index = visibleObjects.FindIndex((item) => item.renderer == renderer);
        //    return index >= 0;
        //}

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