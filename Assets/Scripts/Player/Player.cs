﻿using DG.Tweening;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IResetable
{
    private bool dead = false;
    private Transform topOfScreen;

    public event Action OnLevelCompleted;

    public event Action OnCenterColliderEnter;

    public event Action OnDie;

    public event Action OnStartMoving;

    public event Action OnStartIdle;

    public event Action OnStopIdle;

    private void Awake()
    {
        topOfScreen = GameObject.FindGameObjectWithTag("TopOfScreen").transform;
    }

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

    public void StartGame()
    {
        StartMoving();
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
            OnDie?.Invoke();
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
        GetComponent<Explodable>()?.explode();
        GetComponent<ExplosionForce>()?.doExplosion(collider.transform.position);
        GetComponent<Rigidbody2D>().Sleep();
    }

    public void ResetObject()
    {
        transform.position = topOfScreen.position;
        dead = false;
        transform.GetChild(0).gameObject.SetActive(true);
        //GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Explodable>()?.fragmentInEditor();
        GetComponent<Rigidbody2D>().WakeUp();
    }

    private void PlayerDoneMoving()
    {
        OnLevelCompleted?.Invoke();
    }
}