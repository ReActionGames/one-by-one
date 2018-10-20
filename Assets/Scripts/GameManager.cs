using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public enum GameState
    {
        Menu,
        Active,
        Paused,
        End
    }

    public Action<GameState> OnEnterState;
    public Action<GameState, GameState> OnTransitionState;
    public Action<GameState> OnExitState;

    [ReadOnly]
    [SerializeField] private GameState currentState;

    [SerializeField] private bool debug;

    public GameState CurrentState => currentState;

    public bool AttemptChangeState(GameState state)
    {
        switch (currentState)
        {
            case GameState.Menu:
                if (state == GameState.Active)
                {
                    TransitionToState(state);
                    return true;
                }
                break;

            case GameState.Active:
                if (state == GameState.Paused || state == GameState.End)
                {
                    TransitionToState(state);
                    return true;
                }
                break;

            case GameState.Paused:
                if (state == GameState.Active || state == GameState.Menu)
                {
                    TransitionToState(state);
                    return true;
                }
                break;

            case GameState.End:
                if (state == GameState.Active || state == GameState.Menu)
                {
                    TransitionToState(state);
                    return true;
                }
                break;
        }
        return false;
    }

    private void TransitionToState(GameState state)
    {
        DebugManager.Log($"OnExitState({ currentState})", this);
        OnExitState?.Invoke(currentState);
        DebugManager.Log($"OnTransitionState({currentState}, {state})", this);
        OnTransitionState?.Invoke(currentState, state);
        currentState = state;
        DebugManager.Log($"OnEnterState({ currentState})", this);
        OnEnterState?.Invoke(currentState);
    }

    private void Awake()
    {
        currentState = GameState.Menu;
        OnEnterState?.Invoke(currentState);
    }
}