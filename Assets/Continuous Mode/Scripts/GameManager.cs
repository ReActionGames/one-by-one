using Sirenix.OdinInspector;

namespace Continuous
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Button]
        private void StartGame()
        {
            EventManager.TriggerEvent(EventNames.GameStart);
        }

        [Button]
        private void EndGame()
        {
            EventManager.TriggerEvent(EventNames.GameEnd);
        }

        [Button]
        private void RestartGame()
        {
            EventManager.TriggerEvent(EventNames.GameRestart);
        }
    }
}