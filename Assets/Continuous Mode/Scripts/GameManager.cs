using Sirenix.OdinInspector;
using UnityEngine;

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

        [SerializeField] private bool debug = false;

        [Button]
        public void StartGame()
        {
            EventManager.TriggerEvent(EventNames.GameStart);
            CurrentGameState = GameState.Playing;
            if (debug)
                Debug.Log("Start Game");
        }

        [Button]
        public void EndGame()
        {
            EventManager.TriggerEvent(EventNames.GameEnd);
            CurrentGameState = GameState.End;
            if (debug)
                Debug.Log("End Game");
        }

        [Button]
        public void RestartGame()
        {
            EventManager.TriggerEvent(EventNames.GameRestart);
            CurrentGameState = GameState.Playing;
            if (debug)
                Debug.Log("Restart Game");
        }
    }
}