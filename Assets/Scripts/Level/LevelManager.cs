using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;

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

        GameManager.Instance.OnTransitionState += OnTransitionState;
    }

    private void OnTransitionState(GameManager.GameState fromState, GameManager.GameState toState)
    {
        if (toState == GameManager.GameState.Active)
        {
            switch (fromState)
            {
                case GameManager.GameState.Menu:
                    StartGame();
                    break;

                case GameManager.GameState.End:
                    RestartGame();
                    break;
            }
        }
    }

    private void OnDisable()
    {
        player.OnLevelCompleted -= HandleLevelCompleted;
        currentLevel.OnBarsSet -= HandleBarsSet;
        nextLevel.OnBarsSet -= HandleBarsSet;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTransitionState -= OnTransitionState;
        }
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
        barData.ResetSizeAndSpeed();

        transform.position = topOfScreen.position;
        currentLevel.transform.SetParent(transform, true);
        nextLevel.transform.SetParent(transform, true);
        nextLevel.PrepareLevel();
        MoveToBottom();
    }

    public void RestartGame()
    {
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