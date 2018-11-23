using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class PathController : MonoBehaviour
    {
        [SerializeField] private BarPool barPool;
        [SerializeField] private Transform barPoolParent;
        [SerializeField] private float barMovementSpeed = 1;
        [SerializeField] private float movementDelay = 2;

        private IMover barPoolMover = null;
        public Bar CurrentBar { get; private set; }

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
            barPool.PreWarm(barPoolParent);
            ActivateNextBar();

            if (barPoolMover == null)
                barPoolMover = new BarPoolMover(barPoolParent, this);

            DOVirtual.DelayedCall(movementDelay, () => barPoolMover.StartMoving(barMovementSpeed));
        }

        private void ActivateNextBar()
        {
            Bar nextBar = barPool.GetNextBar();
            nextBar.Show();
            CurrentBar = nextBar;
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing)
                return;

            if (Input.GetButtonDown("Fire1"))
                OnClick();
        }

        private void OnClick()
        {
            StopCurrentBar();
            ActivateNextBar();
            barPool.RecycleBars();
        }

        private void StopCurrentBar()
        {
            CurrentBar.Stop();
        }
    }
}