using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeState
{
    protected StoryController controller;
    protected StoryNode StoryNode;
    public NodeState(StoryController controller, StoryNode storyNode)
    {
        this.controller = controller;
        StoryNode = storyNode;
    }
    public abstract void Enter();
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void OnNext() { }
    public virtual void OnPrev() { }

}