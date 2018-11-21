using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class BarPool : MonoBehaviour
    {
        [SerializeField] private int numberOfBars = 20;
        [SerializeField] private int minimumNumberOfPreparedBars = 5;

        private Queue<Bar> preparedBars;
        private Queue<Bar> activeBars;

        public void PreWarm()
        {

        }
    }
}