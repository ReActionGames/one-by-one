using Sirenix.OdinInspector;
using System;
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
        public static event Action GameStart;
        public static event Action GameEnd;
        public static event Action GameRestart;

        [SerializeField] private bool debug = false;

        [Button]
        public void StartGame()
        {
            GameStart?.Invoke();
            CurrentGameState = GameState.Playing;
            if (debug)
                Debug.Log("Start Game");
        }

        [Button]
        public void EndGame()
        {
            GameEnd?.Invoke();
            CurrentGameState = GameState.End;
            if (debug)
                Debug.Log("End Game");
        }

        [Button]
        public void RestartGame()
        {
            GameRestart?.Invoke();
            CurrentGameState = GameState.Playing;
            if (debug)
                Debug.Log("Restart Game");
        }
    }
}