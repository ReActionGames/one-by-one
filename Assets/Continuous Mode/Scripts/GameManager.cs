using Sirenix.OdinInspector;

namespace Continuous
{
    public enum GameState
    {
        Playing,
        Paused,
        Menu,
        End
    }

    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public GameState CurrentGameState { get; private set; } = GameState.Menu;

        [Button]
        public void StartGame()
        {
            EventManager.TriggerEvent(EventNames.GameStart);
            CurrentGameState = GameState.Playing;
        }

        [Button]
        private void EndGame()
        {
            EventManager.TriggerEvent(EventNames.GameEnd);
            CurrentGameState = GameState.End;
        }

        [Button]
        private void RestartGame()
        {
            EventManager.TriggerEvent(EventNames.GameRestart);
            CurrentGameState = GameState.Playing;
        }
    }
}