using DoozyUI;
using TMPro;
using UnityEngine;

namespace Continuous
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private UIElement highScoreEffect;

        private void Awake()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            ScoreKeeper.OnScoreChanged += UpdateScoreUI;
            ScoreKeeper.OnNewHighScore += ShowHighScoreEffect;
        }

        private void OnDisable()
        {
            ScoreKeeper.OnScoreChanged -= UpdateScoreUI;
            ScoreKeeper.OnNewHighScore -= ShowHighScoreEffect;
        }

        private void ShowHighScoreEffect(int score)
        {
            if (score <= 1)
                return;
            highScoreEffect.Show(false);
        }

        private void UpdateScoreUI(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}