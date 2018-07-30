using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level currentLevel, nextLevel;
    [SerializeField] private Transform topOfScreen, bottomOfScreen;
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    [SerializeField] private Ease easing;

    public event Action OnBarsSet;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        player.OnLevelCompleted += HandleLevelCompleted;
        currentLevel.OnBarsSet += HandleBarsSet;
        nextLevel.OnBarsSet += HandleBarsSet;
    }

    private void OnDisable()
    {
        player.OnLevelCompleted -= HandleLevelCompleted;
        currentLevel.OnBarsSet -= HandleBarsSet;
        nextLevel.OnBarsSet -= HandleBarsSet;
    }

    private void Start()
    {
        StartGame();
    }

    [Button]
    public void StartGame()
    {
        StartCurrentLevel();
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

        currentLevel.transform.SetParent(transform, true);
        nextLevel.transform.SetParent(transform, true);
        player.transform.SetParent(transform, true);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(delay)
            .Append(transform.DOMove(bottomOfScreen.position, duration).SetEase(easing))
            .OnComplete(OnMovementCompleted);
    }

    private void OnMovementCompleted()
    {
        currentLevel.transform.parent = null;
        nextLevel.transform.parent = null;
        player.transform.parent = null;

        SwapLevelReferences();
        nextLevel.transform.position = topOfScreen.position;
        nextLevel.Reset();
        StartCurrentLevel();
    }

    private void StartCurrentLevel()
    {

        currentLevel.gameObject.SetActive(true);
        nextLevel.gameObject.SetActive(false);

        currentLevel.transform.position = bottomOfScreen.position;
        nextLevel.transform.position = topOfScreen.position;

        currentLevel.StartBarsMoving();
    }

    private void SwapLevelReferences()
    {
        var temp = currentLevel;
        currentLevel = nextLevel;
        nextLevel = temp;
    }
}