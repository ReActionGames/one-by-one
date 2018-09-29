using DG.Tweening;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool dead = false;

    public event Action OnLevelCompleted;

    public event Action OnCenterColliderEnter;

    public event Action OnEdgeColliderHit;

    public event Action OnStartMoving;

    public event Action OnStartIdle;

    public event Action OnStopIdle;

    private void OnEnable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBarsSet += StartMoving;
            levelManager.OnLevelStart += StartIdle;
        }

        PlayerShoot playerShoot = GetComponent<PlayerShoot>();
        if (playerShoot)
        {
            playerShoot.OnDoneMoving += PlayerDoneMoving;
        }
    }

    private void OnDisable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBarsSet -= StartMoving;
            levelManager.OnLevelStart -= StartIdle;
        }

        PlayerShoot playerShoot = GetComponent<PlayerShoot>();
        if (playerShoot)
        {
            playerShoot.OnDoneMoving -= PlayerDoneMoving;
        }
    }

    private void StartIdle()
    {
        OnStartIdle?.Invoke();
    }

    private void StopIdle()
    {
        OnStopIdle?.Invoke();
    }

    private void StartMoving()
    {
        StopIdle();
        OnStartMoving?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (dead)
            return;

        if (collider.tag.Equals("EdgeCollider"))
        {
            EndGame(collider);
            OnEdgeColliderHit?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (dead)
            return;

        if (collider.tag.Equals("CenterCollider"))
        {
            OnCenterColliderEnter?.Invoke();
        }
    }

    private void EndGame(Collider2D collider)
    {
        dead = true;
        transform.DOKill();
        GetComponentInChildren<Explodable>()?.explode();
        GetComponent<ExplosionForce>()?.doExplosion(collider.transform.position);
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void PlayerDoneMoving()
    {
        OnLevelCompleted?.Invoke();
    }
}