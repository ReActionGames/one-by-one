using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager> {

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
                if(state == GameState.Paused || state == GameState.End)
                {
                    TransitionToState(state);
                    return true;
                }
                break;
            case GameState.Paused:
                if(state == GameState.Active || state == GameState.Menu)
                {
                    TransitionToState(state);
                    return true;
                }
                break;
            case GameState.End:
                if(state == GameState.Active || state == GameState.Menu)
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

        Debug.Log($"[{Time.time}] [GameManager] OnExitState({currentState})");
        OnExitState?.Invoke(currentState);
        Debug.Log($"[{Time.time}] [GameManager] OnTransitionState({currentState}, {state})");
        OnTransitionState?.Invoke(currentState, state);
        currentState = state;
        Debug.Log($"[{Time.time}] [GameManager] OnEnterState({currentState})");
        OnEnterState?.Invoke(currentState);
    }

    private void Awake()
    {
        currentState = GameState.Menu;
        OnEnterState?.Invoke(currentState);
    }
}
