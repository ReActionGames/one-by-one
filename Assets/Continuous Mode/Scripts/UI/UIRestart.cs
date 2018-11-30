using DG.Tweening;
using DoozyUI;
using TMPro;
using UnityEngine;

namespace Continuous
{
    public class UIRestart : MonoBehaviour
    {
        [SerializeField] private UIElement restartScreen;
        [SerializeField] private UIElement HUDElement;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highscoreText;
        [SerializeField] private float showScreenDelay;

        private void OnEnable()
        {
            GameManager.GameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GameManager.GameEnd -= OnGameEnd;
        }

        private void OnGameEnd()
        {
            PrepareEndGameScreen();
            ShowEndGameScreenAfterDelay(showScreenDelay);
        }

        private void ShowEndGameScreenAfterDelay(float delay)
        {
            DOVirtual.DelayedCall(delay, ShowEndGameScreen);
        }

        private void ShowEndGameScreen()
        {
            restartScreen.Show(false);
            HUDElement.Hide(false);
        }

        private void PrepareEndGameScreen()
        {
            scoreText.text = FindObjectOfType<ScoreKeeper>().Score.ToString();
            int highScore = FindObjectOfType<ScoreKeeper>().HighScore;
            highscoreText.text = "HI " + highScore;
        }
    }
}