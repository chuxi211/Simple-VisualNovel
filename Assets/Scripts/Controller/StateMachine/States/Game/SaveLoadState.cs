using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadState : GameState
{
    public SaveLoadState(GameStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        SubScribeEvents();
        SceneManager.LoadScene(3,LoadSceneMode.Additive);
    }
    public override void Exit()
    {
        UnSubScribeEvents();
    }
    protected override void SubScribeEvents()
    {
        EventBus.Subscribe<RequestToStoryEvent>(ToStoryScene);
    }
    protected override void UnSubScribeEvents()
    {
        EventBus.UnSubscribe<RequestToStoryEvent>(ToStoryScene);
    }
    private void ToStoryScene(RequestToStoryEvent e)
    {
        stateMachine.ChangeState<StoryState>();
    }
}