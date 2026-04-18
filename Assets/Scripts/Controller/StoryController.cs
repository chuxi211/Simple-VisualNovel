using Assets.Scripts.Story;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// StoryController And StoryStateMachine
/// </summary>
public class StoryController : MonoBehaviour
{
    //也许应该把不同功能拆成不同类。
    //重构，至少应该包含HandleNode，JumpTo，WaitInput几个状态
    StoryLoader SL;
    
    public string currentID { get; private set; }
    private NodeState currentState;
    GameObject choicePrefab;  
    public Transform choicePanel { get; private set; }
    List<GameObject> currentChoices = new List<GameObject>();
    //所有事件后期都优化EventBus
    private void OnEnable()
    {
        EventBus.Subscribe<LoadDataEvent>(HandleLoadData);
        EventBus.Subscribe<RequestNextEvent>(RequestNext);
        EventBus.Subscribe<RequestPrevEvent>(RequestPrev);
    }
    private void OnDisable()
    {
        EventBus.UnSubscribe<LoadDataEvent>(HandleLoadData);
        EventBus.UnSubscribe<RequestNextEvent>(RequestNext);
        EventBus.UnSubscribe<RequestPrevEvent>(RequestPrev);
    }
    public void RegisterChoicePanel(Transform panel)
    {
        choicePanel = panel;
    }

    private void Awake()
    {
        SL = CoreController.Instance.StoryLoader;
        
        choicePrefab = Resources.Load<GameObject>("Chapter/ChoiceButton");//加载资源？
        
    }
    private void HandleLoadData(LoadDataEvent loaddata)
    {
        string id=loaddata.nodeId;
        JumpTo(id);//如何在不销毁原有场景的情况下跳转存档界面，读档完成后自动跳转回原场景
        SceneManager.UnloadSceneAsync("SaveAndLoadData");
        //将Camera切换为Overlay，然后填sort order更简单，但是多监听器还是会有警告
        //其次，如果后续存档界面也有bgm，切换场景会有bgm重叠的问题
    }
    public StoryNode GetNodeById(string id)
    {
        if (SL.storyNodeDict.TryGetValue(id, out StoryNode storyNode))
        {
            return storyNode;
        }
        Debug.LogError($"No such id in storyNodeDict: {id}");
        return null;
    }
    public void RequestNext(RequestNextEvent e)
    {
        currentState?.OnNext();
    }

    [Obsolete]
    public void Next()
    {
        Debug.Log("next");
        if(!SL.storyNodeDict.TryGetValue(currentID, out StoryNode storyNode))
        {
            Debug.LogError("No such id in storyNodeDict");
            return;
        }
        string nextID = null;
        if(storyNode.Type == Assets.Scripts.Story.Type.Dialogue)
        {
            nextID = storyNode.Node.NextID;
        }
        if (storyNode.Type == Assets.Scripts.Story.Type.Tip)
        {
            nextID=storyNode.Node.NextID;
        }
        else if (storyNode.Type == Assets.Scripts.Story.Type.Choice)
        {
            return;
        }


        JumpTo(nextID);
    }
    public void RequestPrev(RequestPrevEvent e)
    {
        currentState?.OnPrev();
    }
    [Obsolete]
    public void Prev()
    {
        Debug.Log("last");
        if(!SL.storyNodeDict.TryGetValue(currentID, out StoryNode storyNode))
        {
            Debug.LogError("No such id in storyNodeDict");
            return;
        }
        string lastID = null;
        if (storyNode.Type == Assets.Scripts.Story.Type.Dialogue)
        {
            lastID = storyNode.Node.LastID;
            
        }
        if (storyNode.Type == Assets.Scripts.Story.Type.Tip)
        {
            lastID=storyNode.Node.LastID;
        }
        else if (storyNode.Type == Assets.Scripts.Story.Type.Choice)
        {
            return;
        }
        
        JumpTo(lastID);
    }
    //事实上，该函数应该为Manager的方法，StoryController/StateMachine只负责调用Manager的方法
    public void JumpTo(string id)
    {
        if(string.IsNullOrEmpty(id))
        {
            Debug.LogError("Null Id");
            return;
        }
        if(!SL.storyNodeDict.TryGetValue(id, out StoryNode storyNode))
        {
            Debug.LogError($"Not such this ID:{id}");
            return;
        }
        currentID = id;
        currentState?.Exit();
        currentState = CreateNodeState(storyNode);
        currentState?.Enter();
        HandleChapterChanged(storyNode);
        
        
    }
    private NodeState CreateNodeState(StoryNode node)
    {
        switch (node.Type)
        {
            case Assets.Scripts.Story.Type.Dialogue:
                return new DialogueNodeState(this, node);
            case Assets.Scripts.Story.Type.Tip:
                return new TipNodeState(this, node);
            case Assets.Scripts.Story.Type.Choice:
                return new ChoiceNodeState(this,node);
            default:
                Debug.LogError($"Unsupported node type: {node.Type}");
                return null;
        }
    }
    public void ShowChoiceNode(StoryNode node)
    {
        ClearChoices();
        if (choicePrefab == null)
        {
            Debug.LogError("ChoicePrefab is not assigned");
            return;
        }
        ChoiceNode choiceNode = node.Node as ChoiceNode;//向下转型
        foreach (var choice in choiceNode.Choices)
        {
            if (choicePrefab == null)
            {
                Debug.LogError("choiceprefab==null");
                return;
            }
            if (choicePanel == null)
            {
                return;
            }
            GameObject obj = Instantiate(choicePrefab);//从预制体实例化一个对象
            obj.transform.SetParent(choicePanel.transform, false);//设置为choicePanel的子物体，并保持本地缩放和旋转
            currentChoices.Add(obj);

            ChoicePlayer ui = obj.GetComponent<ChoicePlayer>();
            ui.Init(choice.Text, choice.NextID);
            //EventBus.Publish(new ChoiceSelectedEvent(choice.NextID));
        }
    }
    [Obsolete]
    private void OnChoiceSelected(string targetID)
    {
        ClearChoices();
        JumpTo(targetID);
    }

    public void ClearChoices()
    {
        foreach (var obj in currentChoices)
            Destroy(obj);

        currentChoices.Clear();
    }
    [Obsolete("",true)]
    void HandleNode(StoryNode node)
    {
        switch(node.Type)
        {
            case Assets.Scripts.Story.Type.Dialogue:
                //OnNextButtonStateChanged?.Invoke(true);
                
                break;
            case Assets.Scripts.Story.Type.Tip:
                
                break;
            case Assets.Scripts.Story.Type.Choice:
                Debug.Log($"NodeType:{node.Type}");
                ShowChoiceNode(node);
                Debug.Log("生成选项");
                //OnNextButtonStateChanged?.Invoke(false);
                break;
        }

    }
    void HandleChapterChanged(StoryNode storyNode)
    {
        //提取storyNode的ID的前两个字符，X1···，X2···等
        //与currentID的前两字符进行比较，如果不同则触发OnChapterChanged事件
        if (storyNode.Node.NextID == null)
        {
            return;
        }
        if (storyNode.Node.NextID.Length >= 2 && currentID.Length >= 2 &&
        storyNode.Node.NextID[0] == currentID[0] &&
        storyNode.Node.NextID[1] == currentID[1])
        {
            Debug.Log(storyNode.Node.NextID);
            Debug.Log(currentID);
            Debug.Log("Same Chapter");
            return;
        }
        EventBus.Publish(storyNode.Node.NextID[1]);
    } 
}

