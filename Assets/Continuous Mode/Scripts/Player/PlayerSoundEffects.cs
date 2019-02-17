using DoozyUI;
using UnityEngine;

namespace Continuous
{
    public class PlayerSoundEffects : SoundEffectPlayer
    {
        [SerializeField] private AudioClip scoreSound;
        [SerializeField] private AudioClip dieSound;
        
        private void OnEnable()
        {
            //Player.ScorePoint += PlayScoreSound;
            Player.Die += PlayDieSound;
        }

        private void OnDisable()
        {
            //Player.ScorePoint -= PlayScoreSound;
            Player.Die -= PlayDieSound;
        }

        private void PlayScoreSound()
        {
            PlaySound(scoreSound);
        }

        private void PlayDieSound()
        {
            PlaySound(dieSound);
        }
    }
}