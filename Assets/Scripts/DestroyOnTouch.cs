using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == collisionLayer)
            Destroy(collider.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == collisionLayer)
            Destroy(collision.gameObject);

    }


}