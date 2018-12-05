using UnityEngine;

namespace Continuous
{
    public class HighScoreMarker : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer marker;

        private SpriteVisibility visibility;

        private void Awake()
        {
            visibility = GetComponent<SpriteVisibility>();
        }

        private void Start()
        {
            ResizeMarkerToFitCamera();
            visibility.HideInstantly();
        }

        private void OnEnable()
        {
            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;

            ScoreKeeper.OnNewHighScore += OnNewHighScore;
        }

        private void OnDisable()
        {
            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
            ScoreKeeper.OnNewHighScore -= OnNewHighScore;
        }

        private void OnGameStartOrRestart()
        {
            visibility.HideInstantly();
            if (ScoreKeeper.HighScore <= 0)
                return;

            transform.localPosition = transform.localPosition.With(y: ScoreKeeper.HighScore * 2 + 1);
            visibility.Show();
        }

        private void OnNewHighScore(int highScore)
        {
            visibility.Hide();
        }

        private void ResizeMarkerToFitCamera()
        {
            float height = Camera.main.orthographicSize * 2.0f;
            float width = height * Camera.main.aspect;

            marker.size = new Vector2(width, marker.size.y);
        }
    }
}