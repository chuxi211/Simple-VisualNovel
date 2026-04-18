using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Story;

public class CharacterManager : MonoBehaviour
{
    CharacterView view;//预制体资产
    Transform viewsTransform;    //父物体，canvas
    List<CharacterActor> characterActors;    
    List<CharacterAssetData> characterAssets;     //存
    Dictionary<string, CharacterAssetData> characterAssetDict;    
    private void Awake()
    {
        //原则上讲，是不应该在awake里写小作文的
        //canvasTransform = FindObjectOfType<Canvas>().transform;
        viewsTransform = GameObject.Find("CharacterViews").transform;
        view = Resources.Load<CharacterView>("CharacterAsset/CharacterView");
        characterActors = new List<CharacterActor>();
        characterAssets = new List<CharacterAssetData>();
        characterAssetDict = new Dictionary<string, CharacterAssetData>();
        LoadAllCharacterAsset();
    }
    private void OnEnable()
    {
        EventBus.Subscribe<StoryNodeChangedEvent>(UpdateCharacter);
        EventBus.Subscribe<BGChangedEvent>(ClearActors);//每次背景改变时清除角色，准备加载新角色
        EventBus.Subscribe<LoadDataEvent>(ClearActorswithString);//每次读档时清除角色，准备加载新角色
    }
    private void OnDisable()
    {
        EventBus.UnSubscribe<StoryNodeChangedEvent>(UpdateCharacter);
        EventBus.UnSubscribe<BGChangedEvent>(ClearActors);
        EventBus.UnSubscribe<LoadDataEvent>(ClearActorswithString);
    }
    //这样写，是因为我们假设所有HasCharacter都为true，但如果某些节点为false，就要直接隐藏视图
    //但是，所有关于Character的操作都依赖DialogueNode，也就是说，如果不是DialogueNode，就无法对角色进行处理
    void UpdateCharacter(StoryNodeChangedEvent e)
    {
        StoryNode storyNode = CoreController.Instance.StoryController.GetNodeById(e.nodeId);
        Debug.Log("update character");
        if(storyNode == null)
        {
            Debug.LogError("storyNode is null");
            return;
        }
        if (storyNode.HasCharacter == false)
        {
            foreach (var actors in characterActors)
            {
                actors.view.gameObject.SetActive(false);
            }
            return;
        }
        else
        {
            foreach (var actors in characterActors)
            {
                actors.view.gameObject.SetActive(true);
            }
        }
        DialogueNode dialogueNode = storyNode.Node as DialogueNode;
        if (dialogueNode == null)
        {
            
            return;
        }
        if (dialogueNode.Character == null)
        {
            return;
        }
        string id = dialogueNode.Character.id;
        State state = dialogueNode.Character.state;
        Position position = dialogueNode.Character.position;
        var actor = characterActors.Find(a => a.Asset.CharacterID == id);
        if (actor == null)
        {
            var assetData = GetCharacter(id);
            
            actor = CreatActor(assetData);
        }
        actor.SetState(state);
        actor.SetPosition(position);
    }

    public void ClearActors(BGChangedEvent e)
    {
        foreach (var actor in characterActors)
        {
            Destroy(actor.view.gameObject);
        }//销毁所有角色的视图对象
        characterActors.Clear();//清空角色列表
    }//跳转，存档，或进入新章节时调用，清除当前所有角色，准备加载新角色
    //防止场景切换后角色残留
    //直接通过Resources加载所有资源，之后改为Addressable
    public void ClearActorswithString(LoadDataEvent e)
    {
        ClearActors(new BGChangedEvent());//为了绑定OnLoadData事件
    }
    void LoadAllCharacterAsset()
    {
        //加载Resources下的所有CharacterAsset资源并存储到字典中
        characterAssets.AddRange(Resources.LoadAll<CharacterAssetData>("CharacterAsset"));
        foreach(var asset in characterAssets)
        {
            characterAssetDict.Add(asset.CharacterID, asset);
        }
    }
    //动态加载，需要有一个string储存从dia里传入的名字才能用
    //问题很多，如果是第一次加载会出现什么情况？

    public CharacterAssetData GetCharacter(string id)
    {
        if (!characterAssetDict.ContainsKey(id))
        {
            var asset = Resources.Load<CharacterAssetData>("CharacterAsset/" + id);
            if (asset != null)
            {
                characterAssetDict[id] = asset;
            }
        }
        return characterAssetDict[id];

    }
    public CharacterActor CreatActor(CharacterAssetData assetData)
    {
        
        var viewInstantiate=Instantiate(this.view, viewsTransform);
        viewInstantiate.name = assetData.CharacterID+"view"; //这里的ID其实就是本名，显示的名字由Dialogue直接传入
        var actor = new CharacterActor(assetData, viewInstantiate);
        characterActors.Add(actor);
        return actor;
    }
    
}
