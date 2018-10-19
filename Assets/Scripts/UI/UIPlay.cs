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
        //Scene main = SceneManager.GetSceneByBuildIndex(0);
        //SceneManager.LoadScene(main.buildIndex, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(main);
    }

    public void Play()
    {
        //UIManager.HideUiElement("Game", "Main Menu");
        //main.Hide(false);
        //OnPlayButtonClicked?.Invoke();
        GameManager.Instance.AttemptChangeState(GameManager.GameState.Active);
    }
}