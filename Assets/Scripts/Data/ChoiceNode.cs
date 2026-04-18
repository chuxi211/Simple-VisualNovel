using Assets.Scripts.Story;
using System.Collections.Generic;
[System.Serializable]
public class ChoiceNode:Node
{
    public ChoiceData[] Choices;
}
public class ChoiceData
{
    public string Text;
    public string NextID;
}