using DoozyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class BarSoundEffects : SoundEffectPlayer
    {
        [SerializeField] private AudioClip barSound;

        private void OnEnable()
        {
            PathController.BarPlaced += PlayBarSound;
        }

        private void OnDisable()
        {
            PathController.BarPlaced -= PlayBarSound;
        }

        private void PlayBarSound()
        {
            PlaySound(barSound);
        }
    }
}