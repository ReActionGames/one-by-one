using UnityEngine;

namespace Continuous
{
    public class HighScoreSoundEffects : SoundEffectPlayer
    {
        [SerializeField] private AudioClip highScoreSound;

        private void OnEnable()
        {
            ScoreKeeper.OnNewHighScore += PlayHighScoreSound;
        }

        private void OnDisable()
        {
            ScoreKeeper.OnNewHighScore -= PlayHighScoreSound;
        }

        private void PlayHighScoreSound(int score)
        {
            PlaySound(highScoreSound);
        }
    }
}