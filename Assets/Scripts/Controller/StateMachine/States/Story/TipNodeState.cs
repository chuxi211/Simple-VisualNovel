using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipNodeState : NodeState
{
    public TipNodeState(StoryController storyController,StoryNode storyNode) : base(storyController,storyNode)
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
