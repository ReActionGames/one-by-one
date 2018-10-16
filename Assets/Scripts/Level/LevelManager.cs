using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level currentLevel, nextLevel;
    [SerializeField] private Transform topOfScreen, bottomOfScreen;
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;
    [SerializeField] private bool playOnStart = false;
    [SerializeField] private BarData barData;

    public event Action<float, Ease> OnBackgroundMove;

    public event Action OnBarsSet;

    public event Action OnLevelStart;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        barData.ResetSizeAndSpeed();
    }

    private void OnEnable()
    {
        player.OnLevelCompleted += HandleLevelCompleted;
        currentLevel.OnBarsSet += HandleBarsSet;
        nextLevel.OnBarsSet += HandleBarsSet;

        UIEndGame endGame = FindObjectOfType<UIEndGame>();
        if (endGame != null)
            endGame.OnRestartGame += RestartGame;

        UIPlay.OnPlayButtonClicked += HandlePlayButtonClicked;
    }

    private void HandlePlayButtonClicked()
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
        }

        StartGame();
    }

    private void OnDisable()
    {
        player.OnLevelCompleted -= HandleLevelCompleted;
        currentLevel.OnBarsSet -= HandleBarsSet;
        nextLevel.OnBarsSet -= HandleBarsSet;

        UIEndGame endGame = FindObjectOfType<UIEndGame>();
        if (endGame != null)
            endGame.OnRestartGame -= RestartGame;

        UIPlay.OnPlayButtonClicked -= HandlePlayButtonClicked;
    }

    private void Start()
    {
        if (playOnStart)
        {
            StartGame();
        }
    }

    [Button]
    public void StartGame()
    {
        //player.StartGame();
        //StartCurrentLevel();
        //OnBarsSet?.Invoke();
        transform.position = topOfScreen.position;
        currentLevel.transform.SetParent(transform, true);
        nextLevel.transform.SetParent(transform, true);
        nextLevel.PrepareLevel();
        MoveToBottom();
    }

    public void RestartGame()
    {
        //currentLevel.HideBars();
        //player.transform.position = topOfScreen.position;
        //player.ResetObject();

        var resetables = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>();
        foreach (var resetable in resetables)
        {
            resetable.ResetObject();
        }

        barData.ResetSizeAndSpeed();

        NextLevel();
    }

    private void HandleBarsSet()
    {
        OnBarsSet?.Invoke();
    }

    private void HandleLevelCompleted()
    {
        NextLevel();
    }

    private void NextLevel()
    {
        transform.position = topOfScreen.position;

        nextLevel.gameObject.SetActive(true);
        nextLevel.PrepareLevel();

        currentLevel.transform.SetParent(transform, true);
        nextLevel.transform.SetParent(transform, true);
        player.transform.SetParent(transform, true);

        MoveToBottom();
    }

    private void MoveToBottom()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(delay)
            .AppendCallback(InvokeOnBackgroundMove)
            .Append(transform.DOMove(bottomOfScreen.position, duration).SetEase(easing))
            .OnComplete(OnMovementCompleted);
    }

    private void InvokeOnBackgroundMove()
    {
        OnBackgroundMove?.Invoke(duration, easing);
    }

    private void OnMovementCompleted()
    {
        currentLevel.transform.parent = null;
        nextLevel.transform.parent = null;
        player.transform.parent = null;

        SwapLevelReferences();
        nextLevel.transform.position = topOfScreen.position;
        nextLevel.HideBars(true);
        StartCurrentLevel();
    }

    private void StartCurrentLevel()
    {
        currentLevel.gameObject.SetActive(true);
        nextLevel.gameObject.SetActive(false);

        currentLevel.transform.position = bottomOfScreen.position;
        nextLevel.transform.position = topOfScreen.position;

        currentLevel.StartBarsMoving(barData);
        OnLevelStart?.Invoke();
    }

    private void SwapLevelReferences()
    {
        Level temp = currentLevel;
        currentLevel = nextLevel;
        nextLevel = temp;
    }
}