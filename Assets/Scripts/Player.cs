using DG.Tweening;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Transform topOfScreen;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;
    [SerializeField] private float delay;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Ease rotationEase;

    private bool dead = false;

    public event Action OnLevelCompleted;
    public event Action OnCenterColliderEnter;
    public event Action OnEdgeColliderHit;
    public event Action<float, Vector2, float, Ease> OnStartMoving;

    private void OnEnable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            FindObjectOfType<LevelManager>().OnBarsSet += ShootUpToTopOfScreen;
        }
    }

    private void OnDisable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            FindObjectOfType<LevelManager>().OnBarsSet -= ShootUpToTopOfScreen;
        }
    }
    
    public void ShootUpToTopOfScreen()
    {
        transform.DOMove(topOfScreen.position, duration)
            .SetDelay(delay)
            .SetEase(easing)
            .OnComplete(PlayerDoneMoving);
        OnStartMoving?.Invoke(delay, topOfScreen.position, duration, easing);
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