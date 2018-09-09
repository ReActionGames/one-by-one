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

    private Sequence sequence;
    private bool dead = false;

    public event Action OnLevelCompleted;
    public event Action OnCenterColliderEnter;
    public event Action OnEdgeColliderHit;

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

    private void Start()
    {
        //transform.DORotate(new Vector3(0, 0, 90), rotationSpeed)
        //    .SetLoops(-1, LoopType.Incremental)
        //    .SetEase(rotationEase);
        //GetComponent<Collider2D>().enabled = false;
    }

    public void ShootUpToTopOfScreen()
    {
        sequence = DOTween.Sequence();
        Tween tween = transform.DOMove(topOfScreen.position, duration).SetEase(easing);
        sequence.Append(tween)
            .PrependInterval(delay)
            .OnComplete(PlayerDoneMoving);
        sequence.Play();

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
        sequence.Kill();
        GetComponentInChildren<Explodable>()?.explode();
        GetComponent<ExplosionForce>()?.doExplosion(collider.transform.position);
        //GetComponent<Collider2D>().isTrigger = true;
        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void PlayerDoneMoving()
    {
        OnLevelCompleted?.Invoke();
        
    }
}