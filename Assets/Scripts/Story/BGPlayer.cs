using Assets.Scripts.Story;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGPlayer : MonoBehaviour
{
    private Image background;
    public event Action OnBGChanged;
    private void Awake()
    {
        background = GetComponent<Image>();
    }
    private void OnEnable()
    {
        EventBus.Subscribe<StoryNodeChangedEvent>(ChangeBG);
    }
    private void OnDisable()
    {
        EventBus.UnSubscribe<StoryNodeChangedEvent>(ChangeBG);
    }
    private void ChangeBG(StoryNodeChangedEvent e)
    {
        StoryNode storyNode = CoreController.Instance.StoryController.GetNodeById(e.nodeId);
        if(storyNode == null)
        {
            Debug.LogError($"storyNode{storyNode.Node.ID} is null");
            return;
        }
        DialogueNode dialogueNode = storyNode.Node as DialogueNode;
        if (background.sprite == null)
        {
            background.color = new Color(1, 1, 1, 0);
        }
        if (dialogueNode == null)
        {
            Debug.LogError("node is null");
            return;
        }
        if (dialogueNode.Background == null)
        {
            background.color = new Color(1, 1, 1, 0);
            Debug.Log("BackGround is null");
            return;
        }
        if (dialogueNode.Background == this.background.sprite.name)
        {
            Debug.Log("Same BG, no need to change");
            return;
        }
        Sprite sprite = Resources.Load<Sprite>("image/CG/" + dialogueNode.Background);
        background.sprite = sprite;
        background.color = new Color(1, 1, 1, 1);
        EventBus.Publish(new BGChangedEvent() );
    }
    [Obsolete("使用ChangeBG(StoryNodeChangedEvent e)方法替代")]
    private void ChangeBG(StoryNode storynode)
    {
        DialogueNode dialogueNode=storynode.Node as DialogueNode;
        if (background .sprite==null)
        {
            background.color = new Color(1, 1, 1, 0);
        }
        if (dialogueNode == null)
        {
            Debug.LogError("node is null");
            return;
        }
        if (dialogueNode.Background == null)
        {
            background.color=new Color(1, 1, 1, 0);
            Debug.Log("BackGround is null");
            return;
        }
        if (dialogueNode.Background == this.background.sprite.name)
        {
            Debug.Log("Same BG, no need to change");
            return;
        }
        Sprite sprite=Resources.Load<Sprite > ("image/CG/" + dialogueNode.Background);
        background .sprite = sprite;
        background.color = new Color(1, 1, 1, 1);
        OnBGChanged?.Invoke();
        
    }
}
