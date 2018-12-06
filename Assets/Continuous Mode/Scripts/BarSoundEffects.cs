using DoozyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class BarSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioClip barSound;

        private Soundy soundy;

        private void Awake()
        {
            soundy = FindObjectOfType<Soundy>();
        }

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
            soundy.PlaySound(barSound);
        }
    }
}