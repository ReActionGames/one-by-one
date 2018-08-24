using DG.Tweening;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Transform topOfScreen;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;
    [SerializeField] private float delay;
    private Sequence sequence;

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
        //Debug.Log("Collision with" + collider.gameObject.tag);
        //transform.DOPause();
        //transform.DOKill();
        if (collider.tag.Equals("EdgeCollider"))
        {
            EndGame(collider);
            OnEdgeColliderHit?.Invoke();
        }
        //else if (collider.tag.Equals("CenterCollider"))
        //{
        //    OnCenterColliderEnter?.Invoke();
        //}
    }

    private void OnTriggerExit2D(Collider2D collider)
    {        
        if (collider.tag.Equals("CenterCollider"))
        {
            OnCenterColliderEnter?.Invoke();
        }
    }

    private void EndGame(Collider2D collider)
    {
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