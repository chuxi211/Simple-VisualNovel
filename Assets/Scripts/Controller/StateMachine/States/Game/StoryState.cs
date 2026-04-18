using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryState : GameState
{
    private StoryController controller;

    public StoryState(GameStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        SceneManager.LoadSceneAsync(2).completed += OnSceneLoaded;
    }
    public override void Exit()
    {
        UnSubScribeEvents();
    }
    protected override void SubScribeEvents()
    {
        EventBus.Subscribe<RequestSaveEvent>(ToSaveLoadScene);
    }
    protected override void UnSubScribeEvents()
    {
        EventBus.UnSubscribe<RequestSaveEvent>(ToSaveLoadScene);
    }
    private void OnSceneLoaded(AsyncOperation op)
    {
        controller = CoreController.Instance.StoryController;
        SubScribeEvents();
        controller.JumpTo("X1C1");
    }
    private void ToSaveLoadScene(RequestSaveEvent e)
    {
        stateMachine.ChangeState<SaveLoadState>();
    }
}
