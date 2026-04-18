using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSelectedEvent
{
    public string targetID;
    public ChoiceSelectedEvent(string targetID)
    {
        this.targetID = targetID;
    }
}