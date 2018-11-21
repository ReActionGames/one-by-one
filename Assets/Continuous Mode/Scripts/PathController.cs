using System;
using UnityEngine;

namespace Continuous
{
    public class PathController : MonoBehaviour
    {
        [SerializeField] private BarPool barPool;

        private void OnEnable()
        {
            EventManager.StartListening(EventNames.GameStart, OnGameStart);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventNames.GameStart, OnGameStart);
        }

        private void OnGameStart(Message message)
        {
            barPool.PreWarm();
        }
    }
}