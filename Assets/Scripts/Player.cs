using System;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Transform topOfScreen;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;
    [SerializeField] private float delay;
    private Sequence sequence;

    public event Action OnLevelCompleted;

    private void OnEnable()
    {
        FindObjectOfType<LevelManager>().OnBarsSet += ShootUpToTopOfScreen;
    }

    private void OnDisable()
    {
        FindObjectOfType<LevelManager>().OnBarsSet -= ShootUpToTopOfScreen;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with" + collision.gameObject.name);
        //transform.DOPause();
        //transform.DOKill();
        sequence.Kill();
        GetComponentInChildren<Explodable>()?.explode();
        GetComponent<ExplosionForce>()?.doExplosion(collision.transform.position);
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