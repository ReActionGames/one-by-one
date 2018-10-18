using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class HighScoreMarker : MonoBehaviour, IResetable
{
    private const int numberOfBars = 10;

    [SerializeField] private Transform /*bar, left, right, */topOfScreen;
    [SerializeField] private SpriteRenderer marker;
    //[SerializeField] private GameObject[] fragments;
    [SerializeField] private float fadeDuration;

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
    }

    private void OnDisable()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
        {
            levelManager.OnBackgroundMove -= CheckForHighScore;
        }

        scoreKeeper.OnNewHighScore -= OnNewHighScore;
    }

    private void CheckForHighScore(float duration, Ease easing)
    {
        Debug.Log($"[{Time.time}] CheckForHighScore() - first pass");
        if (scoreKeeper.HighScore <= 0)
        {
            Debug.Log($"[{Time.time}] CheckForHighScore() - second pass");
            return;
        }

        if (scoreKeeper.Score + numberOfBars <= scoreKeeper.HighScore)
        {
            Debug.Log($"[{Time.time}] CheckForHighScore() - third pass");
            return;
        }

        if (gotHighScoreThisRound)
        {
            Debug.Log($"[{Time.time}] CheckForHighScore() - forth pass");
            return;
        }

        Debug.Log($"[{Time.time}] CheckForHighScore() - fifth pass");

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

    [Button]
    private void ResizeMarkerToFitCamera()
    {
        Debug.Log($"[{Time.time}] ResizeMarkerToFitCamera()");
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        marker.size = new Vector2(width, marker.size.y);

        //bar.localScale = new Vector3(width, bar.localScale.y);

        //float edgePosition = (width / 2) - right.GetComponent<SpriteRenderer>().bounds.extents.x;

        //right.localPosition = new Vector3(edgePosition, right.localPosition.y);

        //left.localPosition = new Vector3(-edgePosition, left
//.localPosition.y);
    }

    private void SetMarkerActive(bool active)
    {
        Debug.Log($"[{Time.time}] SetMarkerActive({active})");
        //bar.gameObject.SetActive(active);
        //right.gameObject.SetActive(active);
        //left.gameObject.SetActive(active);
        marker.gameObject.SetActive(active);
        marker.DOFade(normalAlphaValue, 0);
    }

    private void OnNewHighScore()
    {
        Debug.Log($"[{Time.time}] OnNewHighScore()");
        //SetMarkerActive(false);
        marker.DOFade(0, fadeDuration);
        gotHighScoreThisRound = true;

        //foreach (var frag in fragments)
        //{
        //    frag.SetActive(true);
        //}

        //GetComponent<ExplosionForce>().doExplosion(transform.position);
    }

    public void ResetObject()
    {
        Debug.Log($"[{Time.time}] ResetObject()");
        SetMarkerActive(false);
        gotHighScoreThisRound = false;
    }
}