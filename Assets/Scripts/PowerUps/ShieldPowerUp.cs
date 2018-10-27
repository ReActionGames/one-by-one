using ReActionGames.Events;
using ReActionGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUp {

    [SerializeField] private SpriteRenderer sprite;

    public void SetUp()
    {
        sprite.gameObject.SetActive(true);
    }

	public void Collect()
    {
        sprite.gameObject.SetActive(false);
        EventManager.TriggerEvent(EventNames.PowerUpCollected, new Message(this));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }
}
