using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNodeState : NodeState
{
    public DialogueNodeState(StoryController controller, StoryNode storyNode) : base(controller, storyNode)
    {
    }

    public override void Enter()
    {
        EventBus.Publish(new StoryNodeChangedEvent(StoryNode.Node.ID));
        EventBus.Publish(new NextButtonUsedStateChanged(true));
    }
    public override void OnNext()
    {
        controller.JumpTo(StoryNode.Node.NextID);
    }
    public override void OnPrev()
    {
        controller.JumpTo(StoryNode.Node.LastID);
    }
}