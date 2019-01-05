using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Continuous
{
    public enum GameState
    {
        Playing,
        Paused,
        Ending,
        End,
        Menu
    }

    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public static GameState CurrentGameState { get; private set; } = GameState.Menu;

        public static event Action GameStart;
        public static event Action GameEnd;
        public static event Action GameRestart;
        public static event Action GameEnding;

        [SerializeField] private bool debug = false;
        
        [Button]
        public static void StartGame()
        {
            GameStart?.Invoke();
            CurrentGameState = GameState.Playing;
            if (Instance.debug)
                Debug.Log("Start Game");
        }

        public static void StartEndGame()
        {
            GameEnding?.Invoke();
            CurrentGameState = GameState.Ending;
            if (Instance.debug)
                Debug.Log("Ending Game");

        }

        [Button]
        public static void FinishEndGame()
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

        public void startGame()
        {
            StartGame();
        }

        public void endGame()
        {
            FinishEndGame();
        }

        public void restartGame()
        {
            RestartGame();
        }
    }
}