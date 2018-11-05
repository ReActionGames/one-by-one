using ReActionGames.Events;
using ReActionGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;

public class ShieldPowerUp : PowerUp {

    [SerializeField] private SpriteRenderer sprite;

    private SoundEffect soundEffect;

    private void Awake()
    {
        soundEffect = GetComponent<SoundEffect>();
    }

    public void SetUp()
    {
        sprite.gameObject.SetActive(true);
    }

	public void Collect()
    {
        sprite.gameObject.SetActive(false);
        soundEffect.PlaySoundEffect();
        EventManager.TriggerEvent(EventNames.PowerUpCollected, new ReActionGames.Events.Message(this));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }
}
