using DoozyUI;
using UnityEngine;

namespace Continuous
{
    public abstract class SoundEffectPlayer : MonoBehaviour
    {
        private Soundy soundy;

        private void Awake()
        {
            soundy = FindObjectOfType<Soundy>();
        }

        protected void PlaySound(AudioClip sound)
        {
            soundy?.PlaySound(sound);
        }

        protected void PlaySound(AudioClip sound, float volume)
        {
            soundy?.PlaySound(sound, volume);
        }

        protected void PlaySound(AudioClip sound, float volume, float pitch)
        {
            soundy?.PlaySound(sound, volume, pitch);
        }
    }
}