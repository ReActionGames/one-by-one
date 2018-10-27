using DG.Tweening;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool dead = false;
    private Transform topOfScreen;
    private PlayerShield shield;

    public event Action OnLevelCompleted;

    public event Action OnCenterColliderEnter;

    public event Action OnDie;

    public event Action OnStartMoving;

    public event Action OnStartIdle;

    public event Action OnStopIdle;

    private void Awake()
    {
        topOfScreen = GameObject.FindGameObjectWithTag("TopOfScreen").transform;
        shield = GetComponent<PlayerShield>();
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

        GameManager.Instance.OnTransitionState += OnTransitionState;
    }

    private void OnTransitionState(GameManager.GameState fromState, GameManager.GameState toState)
    {
        if (fromState == GameManager.GameState.End && toState == GameManager.GameState.Active)
        {
            ResetObject();
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

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTransitionState -= OnTransitionState;
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
        dead = false;
        StopIdle();
        OnStartMoving?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (dead || GameManager.Instance.CurrentState != GameManager.GameState.Active)
            return;

        if (collider.tag.Equals("EdgeCollider"))
        {
            if (shield.IsActive())
            {
                shield.Deactivate();
                return;
            }

            EndGame(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (dead || GameManager.Instance.CurrentState != GameManager.GameState.Active)
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

        OnDie?.Invoke();

        GameManager.Instance.AttemptChangeState(GameManager.GameState.End);
    }

    public void ResetObject()
    {
        transform.position = topOfScreen.position;
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Explodable>()?.fragmentInEditor();
        GetComponent<Rigidbody2D>().WakeUp();
    }

    private void PlayerDoneMoving()
    {
        OnLevelCompleted?.Invoke();
    }
}