using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class Character2D
{
    public string name;    //一个在文本框显示的名字
    public string id;     //实际的唯一的真正的名字
    public State state;
    public Position position;
}
public enum State
{
    Idle,
    Unknown,
    Smile,
    Tears,
}
public enum Position
{
    Unknown,
    Left,
    Right,
    Middle
}
