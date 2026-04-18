using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeState : GameState
{
    public HomeState(GameStateMachine stateMachine) : base(stateMachine)
    {
        
    }
    public override void Enter()
    {
        SceneManager.LoadSceneAsync(1);
        SubScribeEvents();
    }
    public override void Exit()
    {
        UnSubScribeEvents();
    }
    protected override void SubScribeEvents()
    {
        EventBus.Subscribe<RequestToStoryEvent>(ToStoryScene);
        EventBus.Subscribe<RequestSaveEvent>(ToSaveLoadScene);
    }
    protected override void UnSubScribeEvents()
    {
        EventBus.UnSubscribe<RequestToStoryEvent>(ToStoryScene);
        EventBus.UnSubscribe<RequestSaveEvent>(ToSaveLoadScene);
    }
    private void ToStoryScene(RequestToStoryEvent e)
    {
        stateMachine.ChangeState <StoryState> ();
    }
    private void ToSaveLoadScene(RequestSaveEvent e)
    {
        stateMachine.ChangeState<SaveLoadState>();
    }
}