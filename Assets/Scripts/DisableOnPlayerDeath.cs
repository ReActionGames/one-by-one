using System;
using UnityEngine;

public class DisableOnPlayerDeath : MonoBehaviour
{
    private void OnEnable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnDie += Disable;
        }
    }

    private void OnDisable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnDie -= Disable;
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}