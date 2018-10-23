using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    [SerializeField] private ShieldProperties properties;
    [SerializeField] private GameObject shieldIndicator;
    [SerializeField] private SpriteRenderer movingSprite;

    private Sequence spriteTween;

    [Button]
	public void Activate()
    {
        shieldIndicator.SetActive(true);
        if (spriteTween == null)
            InitiateSpriteTween();
        spriteTween.Play();
    }

    private void InitiateSpriteTween()
    {
        var scaleTween = movingSprite.transform.DOScale(0, properties.Duration)
            .From()
            .SetEase(properties.ScaleEase);
        var fadeTween = movingSprite.DOFade(0, properties.Duration)
            //.From()
            .SetEase(properties.FadeEase);

        spriteTween = DOTween.Sequence();
        spriteTween.Append(scaleTween)
            .Join(fadeTween)
            .SetLoops(-1);
    }

    [Button]
    public void Deactivate()
    {
        shieldIndicator.SetActive(false);
        spriteTween.Pause();
    }
}
