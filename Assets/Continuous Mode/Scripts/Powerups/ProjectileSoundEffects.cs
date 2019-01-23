using DoozyUI;
using UnityEngine;

namespace Continuous
{
    public class ProjectileSoundEffects : SoundEffectPlayer
    {
        [SerializeField] private AudioClip projectileSound;
        [SerializeField] private AudioClip missFireSound;
        
        private void OnEnable()
        {
            Player.OnProjectileFired += PlayProjectileSound;
        }

        private void OnDisable()
        {
            Player.OnProjectileFired -= PlayProjectileSound;
        }

        private void PlayProjectileSound(bool fired)
        {
            if(fired)
            {
                PlaySound(projectileSound);
            }
            else
            {
                PlaySound(missFireSound);
            }
        }
    }
}