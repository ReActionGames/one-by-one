using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class BarPoolMover
    {
        private Transform barParent;
        private Transform currentBar;
        private float speed = 1;

        public BarPoolMover(Transform barParent)
        {
            this.barParent = barParent;
        }

        public void StartMoving()
        {

        }
    }
}