using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class ParticlesMover : MonoBehaviour, IBackgroundElementMover
    {
        [SerializeField] private float speedMultiplier = 0.5f;
        [SerializeField] private float startingSpeed = 0.1f;
        [SerializeField] private ParticleSystem system;
        
        private ParticleSystem.MainModule mainModule;

        private void Awake()
        {
            mainModule = system.main;
            mainModule.simulationSpeed = startingSpeed;
        }

        public void StartMoving(float speed)
        {
            mainModule.simulationSpeed = speed * speedMultiplier;
        }

        public void StopMoving()
        {
            mainModule.simulationSpeed = startingSpeed;
        }

        public void UpdateTimeScale(float time)
        {
            //mainModule.ti
            mainModule.simulationSpeed = time * speedMultiplier;
        }
    }
}