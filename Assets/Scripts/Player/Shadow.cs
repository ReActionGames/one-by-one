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

        GameManager.Instance.OnExitState += OnExitState;
    }

    private void OnExitState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.End)
        {
            ResetObject();
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

        if (GameManager.Instance != null)
            GameManager.Instance.OnExitState -= OnExitState;
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