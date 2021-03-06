﻿using UnityEngine;

namespace Continuous
{
    public abstract class PowerupComponent : MonoBehaviour
    {
        public enum State
        {
            Active,
            Inactive
        }

        [SerializeField] protected State state = State.Inactive;
    }
}