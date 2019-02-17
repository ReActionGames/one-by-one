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
        [SerializeField] private AudioClip gameOverSound;
        [SerializeField] private float raisePitchTime = 1;
        [SerializeField] private float pitchRaiseAmount = 0.5f;
        [SerializeField] private float maxPitch = 5f;

        private float pitch = 1;
        private float lastPlayTime = 0;

        private void OnEnable()
        {
            //PathController.BarPlaced += PlayBarSound;
            PlayerLookAhead.ScorePoint += PlayBarSound;
            PlayerLookAhead.LookAheadCollision += PlayGameOverSound;
        }

        private void OnDisable()
        {
            //PathController.BarPlaced -= PlayBarSound;
            PlayerLookAhead.ScorePoint -= PlayBarSound;
            PlayerLookAhead.LookAheadCollision -= PlayGameOverSound;
        }

        private void PlayBarSound()
        {
            float timeSinceLastPoint = Time.time - lastPlayTime;
            if (timeSinceLastPoint < raisePitchTime)
            {
                pitch += pitchRaiseAmount;
            }
            else
            {
                pitch = 1;
            }

            if (pitch > maxPitch) pitch = 1;

            lastPlayTime = Time.time;
            PlaySound(barSound, 100, pitch);
        }

        private void PlayGameOverSound(RaycastHit2D collider)
        {
            //PlaySound(gameOverSound);
            //PlaySound(barSound, 100, 0.2f);
        }
    }
}