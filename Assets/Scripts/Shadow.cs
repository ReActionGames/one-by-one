using UnityEngine;

public class Shadow : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerShoot shoot = GetComponent<PlayerShoot>();
        if (shoot)
        {
            shoot.OnStartMoving += ParentToRoot;
            shoot.OnDoneMoving += ParentToPlayer;
        }
    }

    private void OnDisable()
    {
        PlayerShoot shoot = GetComponent<PlayerShoot>();
        if (shoot)
        {
            shoot.OnStartMoving -= ParentToRoot;
            shoot.OnDoneMoving -= ParentToPlayer;
        }
    }

    private void ParentToRoot()
    {
        transform.parent = null;
    }

    private void ParentToPlayer()
    {
        transform.parent = FindObjectOfType<Player>().transform;
    }
}