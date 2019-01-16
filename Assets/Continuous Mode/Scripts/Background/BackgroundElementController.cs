namespace Continuous
{
    public class BackgroundElementController
    {
        private IBackgroundElementMover[] backgroundElementMovers;

        public BackgroundElementController(IBackgroundElementMover[] movers)
        {
            backgroundElementMovers = movers;
        }

        public void StartMoving(float speed)
        {
            foreach (IBackgroundElementMover mover in backgroundElementMovers)
            {
                mover.StartMoving(speed);
            }
        }

        public void UpdateTimeScale(float timeScale)
        {
            foreach (IBackgroundElementMover mover in backgroundElementMovers)
            {
                mover.UpdateTimeScale(timeScale);
            }
        }

        public void StopMoving()
        {
            foreach (IBackgroundElementMover mover in backgroundElementMovers)
            {
                mover.StopMoving();
            }
        }
    }
}