using DoozyUI;
using UnityEngine;

namespace Continuous
{
    public class PlayerSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioClip scoreSound;
        [SerializeField] private AudioClip dieSound;

        private Soundy soundy;

        private void Awake()
        {
            soundy = FindObjectOfType<Soundy>();
        }

        private void OnEnable()
        {
            Player.ScorePoint += PlayScoreSound;
            Player.Die += PlayDieSound;
        }

        private void OnDisable()
        {
            Player.ScorePoint -= PlayScoreSound;
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

        private void PlaySound(AudioClip sound)
        {
            soundy.PlaySound(sound);
        }
    }
}