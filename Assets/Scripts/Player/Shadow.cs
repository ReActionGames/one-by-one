using UnityEngine;

public class Shadow : MonoBehaviour, IResetable
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

    public void ResetObject()
    {
        ParentToPlayer();
        transform.localPosition = Vector3.zero;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}