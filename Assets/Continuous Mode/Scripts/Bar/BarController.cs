﻿using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace Continuous
{
    public class BarController : MonoBehaviour
    {
        public enum State
        {
            Inactive,
            Moving,
            Active
        }

        [SerializeField] private float maxXStartDistanceFromCenter = 2f;

        [InlineEditor]
        [SerializeField] private BarMovementProperties movementProperties;

        [SerializeField] private Pickups pickups;

        private IMover mover;
        private BarData currentData;
        private Bar[] bars;
        private Bar activeBar;

        public State state { get; private set; }

        private void Awake()
        {
            bars = GetComponentsInChildren<Bar>();
            activeBar = bars[0];
            DeactivateBars();
            mover = new BarMover(transform, movementProperties);
            state = State.Inactive;
        }

        public void SetYPosition(float position)
        {
            transform.localPosition = new Vector3(0, position);
        }

        public void Prepare(BarData data)
        {
            float xPos = Random.Range(-maxXStartDistanceFromCenter, maxXStartDistanceFromCenter);
            transform.localPosition = transform.localPosition.With(x: xPos);

            currentData = data;

            DeactivateBars();
            activeBar = bars.Where((x) => x.Type == data.Type).FirstOrDefault();
            ActivateActiveBar();

            activeBar.Prepare(data);

            HideInstantly();
            state = State.Inactive;
        }

        private void DeactivateBars()
        {
            foreach (Bar bar in bars)
            {
                bar.gameObject.SetActive(false);
            }
        }

        private void ActivateActiveBar()
        {
            activeBar.gameObject.SetActive(true);
        }

        public void Show()
        {
            DeactivateBars();
            ActivateActiveBar();
            mover.StartMoving(1);
            activeBar.Show();
            state = State.Moving;
        }

        public void Stop()
        {
            mover.StopMoving();
            state = State.Active;
        }

        public void Hide()
        {
            DeactivateBars();
            ActivateActiveBar();
            mover.StopMoving();
            activeBar.Hide();
            state = State.Inactive;
        }

        public void HideInstantly()
        {
            ActivateActiveBar();
            mover.StopMoving();
            activeBar.HideInstantly();
            DeactivateBars();
            state = State.Inactive;
        }
    }
}