using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class UIButtonUtility : MonoBehaviour
    {
        public void RestartGame()
        {
            GameManager.RestartGame();
        }

        public void StartGame()
        {
            GameManager.StartGame();
        }
    }
}