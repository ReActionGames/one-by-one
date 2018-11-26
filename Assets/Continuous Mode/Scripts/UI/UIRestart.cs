using DoozyUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class UIRestart : MonoBehaviour
    {
        [SerializeField] private UIElement restartScreen;

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.GameEnd, OnGameEnd);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventNames.GameEnd, OnGameEnd);
        }

        private void OnGameEnd(Message message)
        {
            restartScreen.Show(false);
        }
    }
}