using System;
using UnityEngine;

public class DisableOnPlayerDeath : MonoBehaviour
{
    private void OnEnable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnEdgeColliderHit += Disable;
        }
    }

    private void OnDisable()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.OnEdgeColliderHit -= Disable;
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}