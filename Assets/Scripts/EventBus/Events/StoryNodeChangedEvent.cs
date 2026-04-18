using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StoryNodeChangedEvent
{
    public string nodeId;
    public StoryNodeChangedEvent(string nodeId)
    {
        this.nodeId = nodeId;
    }
}
