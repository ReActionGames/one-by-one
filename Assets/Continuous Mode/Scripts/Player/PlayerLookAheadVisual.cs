using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

namespace Continuous
{
    public class PlayerLookAheadVisual : MonoBehaviour
    {
        [SerializeField] private SpriteVisibility[] bars;
        [SerializeField] private Transform visual;
        [SerializeField] private Color clearColor;
        [SerializeField] private Color blockedColor;
        [SerializeField] private float flashSpeed;
        [SerializeField] private Ease ease;
        [SerializeField] private float endHeight;
        [SerializeField] private float duration;

        //[SerializeField] private float flashInterval;

        private void Start()
        {
            foreach (SpriteVisibility bar in bars)
            {
                bar.SetColor(clearColor);
                bar.HideInstantly();
            }
        }

        private void OnEnable()
        {
            PlayerLookAhead.ScorePoint += ShowClear;
            PlayerLookAhead.LookAheadCollision += HandleCollision;
        }

        private void OnDisable()
        {
            PlayerLookAhead.ScorePoint -= ShowClear;
            PlayerLookAhead.LookAheadCollision -= HandleCollision;
        }

        [Button]
        public void ShowClear()
        {
            foreach (SpriteVisibility bar in bars)
            {
                bar.SetColor(clearColor);
                bar.HideInstantly();
            }

            visual.localPosition = visual.localPosition.With(y: 0);
            bars[0].Flash();
            visual.DOLocalMoveY(endHeight, duration)
                .SetEase(ease);

        }

        private void HandleCollision(RaycastHit2D hit)
        {
            ShowBlocked();
        }

        [Button]
        public void ShowBlocked()
        {
            foreach (SpriteVisibility bar in bars)
            {
                bar.SetColor(blockedColor);
                bar.HideInstantly();
            }

        }
    }
}