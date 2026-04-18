using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

public class StoryLoader
{
    
    public StoryNodeList StoryNodeList;
    public Dictionary<string, StoryNode> storyNodeDict;
    TextAsset textAsset;
    public StoryLoader()
    {
        LoadAllNode(new ChapterChangedEvent("1"));
    }
    public void LoadAllNode(ChapterChangedEvent e)
    {
        string chapterIndex = e.ChapterID;
        string path= $"Chapter/X2-sivaChapter{chapterIndex}";
        Debug.Log("Prepare to load story from path: " + path);
        textAsset = Resources.Load<TextAsset>(path);
        Debug.Log(path + "Loaded");
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = { new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore,
            Error = (sender, args) =>
            {
                Debug.LogWarning("Json Error: " + args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            }
        };
        StoryNodeList = JsonConvert.DeserializeObject<StoryNodeList>(textAsset.text, settings);
        /*foreach (var storyNode in StoryNodeList.storyNodes)
        {
            Debug.Log($"Type={storyNode.Type}, NodeRaw={storyNode.NodeRaw?.ToString()}");
        }*/
        foreach (StoryNode storyNode in StoryNodeList.storyNodes)
        {
           // Debug.Log($"Type={storyNode.Type}, NodeRaw type={storyNode.NodeRaw?.GetType().Name}, content={storyNode.NodeRaw?.ToString()}");
            if (storyNode.Type == Type.Dialogue)
            {
                storyNode.Node = storyNode.NodeRaw.ToObject<DialogueNode>();
            }
            if (storyNode.Type == Type.Choice)
            {
                storyNode.Node = storyNode.NodeRaw.ToObject<ChoiceNode>();
                ChoiceNode choiceNode=storyNode.Node as ChoiceNode;
                Debug.Log(choiceNode.Choices.Length);
                foreach(var choice in choiceNode.Choices)
                {
                    Debug.Log(choice.Text);
                    Debug.Log(choice.NextID);
                }
            }
            if (storyNode.Type == Type.Tip)
            {
                storyNode.Node=storyNode.NodeRaw.ToObject<TipNode>();
                Debug.Log(storyNode.Type);
            }
        }
        InitstoryNodeDict();
    }
    
    void InitstoryNodeDict()
    {
        if (storyNodeDict == null)
        {
            storyNodeDict = new Dictionary<string, StoryNode>();
        }
        if (StoryNodeList == null)
        {
            Debug.LogError("List is null");
            return;
        }
        if(StoryNodeList.storyNodes.Count == 0)
        {
            Debug.LogError("Nodes is null");
            return;
        }
        foreach(var node in StoryNodeList.storyNodes)
        {
            storyNodeDict.Add(node.Node.ID, node);
            if (node.Type == Type.Choice) {
                if (storyNodeDict.TryGetValue(node.Node.ID, out StoryNode Node))
                {
                    ChoiceNode choiceNode = Node.Node as ChoiceNode;
                    foreach (var choice in choiceNode.Choices)
                    {
                        Debug.Log($"ChoiceText:{choice.Text},ChoiceNextID:{choice.NextID}");
                    }
                }
            }
            
        }
        
    }
}
