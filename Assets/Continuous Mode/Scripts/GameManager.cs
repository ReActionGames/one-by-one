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
        public static GameState CurrentGameState { get; private set; } = GameState.Menu;

        public static event Action GameStart;

        public static event Action GameEnd;

        public static event Action GameRestart;

        [SerializeField] private bool debug = false;

        [Button]
        public static void StartGame()
        {
            GameStart?.Invoke();
            CurrentGameState = GameState.Playing;
            if (Instance.debug)
                Debug.Log("Start Game");
        }

        [Button]
        public static void EndGame()
        {
            GameEnd?.Invoke();
            CurrentGameState = GameState.End;
            if (Instance.debug)
                Debug.Log("End Game");
        }

        [Button]
        public static void RestartGame()
        {
            GameRestart?.Invoke();
            CurrentGameState = GameState.Playing;
            if (Instance.debug)
                Debug.Log("Restart Game");
        }
    }
}