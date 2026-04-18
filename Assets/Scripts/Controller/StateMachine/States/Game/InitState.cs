using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : GameState
{
    public InitState(GameStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        CoreController.Instance.InitSystems();
        stateMachine.ChangeState<HomeState>();
    }
    public override void Exit()
    {
        Debug.Log("exit initstate");
    }
}
