using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [Header("Button Action")]
    public UIAction action;
    public void Onclick()
    {
        Debug.Log("Button clicked: " + action);
        EventBus.Publish(action);
    }
}
