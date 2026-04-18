using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameStateMachine
{
    private Dictionary<Type, GameState> states = new();
    private GameState currentState;
    public void RegisterState(GameState state)
    {
        states[state.GetType()] = state;
    }
    public void ChangeState<T>()where T : GameState
    {
        currentState?.Exit();
        currentState = states[typeof(T)];
        currentState?.Enter();
    }
}
