using System;
using UnityEngine;

namespace Continuous
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Transform bar, left, right;
        [SerializeField] private BoxCollider2D center;
        [SerializeField] private BarMovementProperties movementProperties;
        [SerializeField] private string activeLayer;
        //[SerializeField] private string movingLayer = "MovingBar";
        [SerializeField] private string inactiveLayer;

        private IMover mover;
        private BarData currentData;
        private SpriteVisibility visibility;
        private ObjectVisibility objectVisibility;
        private BarScaler scaler;
        private BarPowerups powerups;

        private void Awake()
        {
            mover = new BarMover(bar, movementProperties);
            scaler = new BarScaler(left, right, center);
            visibility = GetComponent<SpriteVisibility>();
            objectVisibility = GetComponent<ObjectVisibility>();
            powerups = GetComponent<BarPowerups>();
            SetLayers(false);
        }

        public void Prepare(float yPos, BarData data)
        {
            currentData = data;
            transform.localPosition = new Vector3(0, yPos);
            scaler.Scale(data.Size);
            powerups.SetupPowerup(data.PowerupType, data.Size);
            visibility.HideInstantly();
            objectVisibility.Hide();
            SetLayers(false);
        }
        
        public void Show()
        {
            mover.StartMoving(currentData.Speed);
            visibility.Show();
            objectVisibility.Show();
            SetLayers(false);
        }

        public void Stop()
        {
            mover.StopMoving();
            SetLayers(true);
        }

        public void Hide()
        {
            mover.StopMoving();
            visibility.Hide();
            objectVisibility.Hide();
            SetLayers(false);
        }

        private void SetLayers(bool active)
        {
            if (active)
            {
                left.gameObject.SetLayer(activeLayer);
                right.gameObject.SetLayer(activeLayer);
                return;
            }

            left.gameObject.SetLayer(inactiveLayer);
            right.gameObject.SetLayer(inactiveLayer);
        }

    }
}