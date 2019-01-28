using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class FirebaseAnalyticsSender : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.GameEnd += SendOnGameEndData;
        }

        private void OnDisable()
        {
            GameManager.GameEnd -= SendOnGameEndData;
        }

        private void SendOnGameEndData()
        {            
            //FirebaseAnalytics.LogEvent()
        }
    }
}