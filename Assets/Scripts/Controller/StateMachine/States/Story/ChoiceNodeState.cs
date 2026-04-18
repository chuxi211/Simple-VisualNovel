using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceNodeState : NodeState
{
    //选项节点是最特殊，最难处理的
    public ChoiceNodeState(StoryController controller,StoryNode storyNode) : base(controller,storyNode)
    {
    }

    public override void Enter()
    {
        controller.ShowChoiceNode(StoryNode);
        EventBus.Subscribe<ChoiceSelectedEvent>(OnChoiceSelected);
        EventBus.Publish(new NextButtonUsedStateChanged(false));
    }
    public override void Exit()
    {
        EventBus.UnSubscribe<ChoiceSelectedEvent>(OnChoiceSelected);
        controller.ClearChoices();
    }
    private void OnChoiceSelected(ChoiceSelectedEvent e)
    {
        controller.JumpTo(e.targetID);
    }
}