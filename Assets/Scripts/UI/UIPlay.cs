using DoozyUI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlay : MonoBehaviour
{
    public delegate void PlayButtonClicked();

    public static event PlayButtonClicked OnPlayButtonClicked;

    [SerializeField] private UIElement main;

    private void Start()
    {
    }

    public void Play()
    {
        GameManager.Instance.AttemptChangeState(GameManager.GameState.Active);
    }
}