using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class HighScoreMarker : MonoBehaviour
{
    private const int numberOfBars = 10;

    [SerializeField] private Transform topOfScreen;
    [SerializeField] private SpriteRenderer marker;
    [SerializeField] private float fadeDuration;
    [SerializeField] private bool debug;

    private ScoreKeeper scoreKeeper;
    private bool gotHighScoreThisRound = false;
    private float normalAlphaValue;

    private void Awake()
    {
        normalAlphaValue = marker.color.a;
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnEnable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBackgroundMove += CheckForHighScore;
        }

        scoreKeeper.OnNewHighScore += OnNewHighScore;

        GameManager.Instance.OnEnterState += OnEnterState;
    }

    private void OnDisable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBackgroundMove -= CheckForHighScore;
        }

        scoreKeeper.OnNewHighScore -= OnNewHighScore;

        if (GameManager.Instance != null)
            GameManager.Instance.OnEnterState -= OnEnterState;
    }

    private void OnEnterState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Active)
        {
            SetUpMarker();
        }
    }

    private void CheckForHighScore(float duration, Ease easing)
    {
        //DebugManager.Log("CheckForHighScore() - first pass", this);
        //if (scoreKeeper.HighScore <= 0)
        //{
        //    DebugManager.Log("CheckForHighScore() - second pass", this);
        //    return;
        //}

        //if (scoreKeeper.Score + numberOfBars <= scoreKeeper.HighScore)
        //{
        //    DebugManager.Log("CheckForHighScore() - third pass", this);
        //    return;
        //}

        //if (gotHighScoreThisRound)
        //{
        //    DebugManager.Log("CheckForHighScore() - forth pass", this);
        //    return;
        //}

        if (scoreKeeper.HighScore <= 0 || scoreKeeper.Score + numberOfBars <= scoreKeeper.HighScore || gotHighScoreThisRound)
            return;

        //DebugManager.Log("CheckForHighScore() - fifth pass", this);

        int scoreDelta = scoreKeeper.HighScore - scoreKeeper.Score;
        transform.position = new Vector3(0, (scoreDelta * 2) + 2);
        Vector3 endPosition = transform.position;
        transform.position += topOfScreen.position;
        SetMarkerActive(true);
        transform.DOMoveY(endPosition.y, duration)
            .SetEase(easing);
    }

    private void Start()
    {
        ResizeMarkerToFitCamera();
        SetMarkerActive(false);
    }

    private void SetUpMarker()
    {
        SetMarkerActive(false);
        gotHighScoreThisRound = false;
    }

    [Button]
    private void ResizeMarkerToFitCamera()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        marker.size = new Vector2(width, marker.size.y);
    }

    private void SetMarkerActive(bool active)
    {
        marker.gameObject.SetActive(active);
        marker.DOFade(normalAlphaValue, 0);
    }

    private void OnNewHighScore()
    {
        marker.DOFade(0, fadeDuration);
        gotHighScoreThisRound = true;
    }

    public void ResetObject()
    {
        SetMarkerActive(false);
        gotHighScoreThisRound = false;
    }
}