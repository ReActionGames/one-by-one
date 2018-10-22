using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    [SerializeField] private ShieldProperties properties;
    [SerializeField] private GameObject shieldIndicator;
    [SerializeField] private SpriteRenderer movingSprite;

    private Tween spriteTween;

	public void Activate()
    {
        shieldIndicator.SetActive(true);
        if (spriteTween == null)
            InitiateSpriteTween();
    }

    private void InitiateSpriteTween()
    {
        spriteTween = movingSprite.transform.DOScale(1, properties.Duration)
            .From()
            .SetEase(properties.Ease)
            .SetLoops(-1);
    }

    public void Deactivate()
    {
        shieldIndicator.SetActive(false);
    }
}
