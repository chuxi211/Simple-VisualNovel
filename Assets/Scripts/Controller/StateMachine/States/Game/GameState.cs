using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameStateMachine stateMachine;
    public GameState(GameStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public abstract void Enter();
    public virtual void Exit()
    {
        Debug.Log($"Exti currentstate");
    }
    public virtual void Update() { }
    protected virtual void SubScribeEvents() { }
    protected virtual void UnSubScribeEvents() { }
}