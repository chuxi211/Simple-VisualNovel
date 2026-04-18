using Assets.Scripts.Story;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPlayer : MonoBehaviour
{
    TextMeshProUGUI textDisplay;
    CanvasGroup canvasGroup;
    Coroutine Typingcoroutine;
    //string test;
    private void Awake()
    {
        textDisplay= GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        //test = "Hello World!";
        //textDisplay.text = test;
    }
    
    //订阅和取消订阅事件
    private void OnEnable()
    {
        EventBus.Subscribe<StoryNodeChangedEvent>(PlayText);
    }
    private void OnDisable()
    {
        EventBus.UnSubscribe<StoryNodeChangedEvent>(PlayText);
    }
    //原先直接持有StoryController的引用，调用PlayText(StoryNode storyNode)方法播放文本
    //现在的逻辑流为
    public void PlayText(StoryNodeChangedEvent storyNodeChangedEvent)
    {
        StoryNode storyNode=CoreController.Instance.StoryController.GetNodeById(storyNodeChangedEvent.nodeId);
        DialogueNode dialogueNode = storyNode.Node as DialogueNode;
        if (Typingcoroutine != null)
        {
            StopCoroutine(Typingcoroutine);
        }
        if (dialogueNode == null)
        {
            canvasGroup.alpha = 0;
            return;
        }
        canvasGroup.alpha = 1;
        Typingcoroutine=StartCoroutine(TypeText(dialogueNode));
    }
    [Obsolete("使用PlayText(StoryNodeChangedEvent storyNodeChangedEvent)方法替代")]
    //播放文本
    public void PlayText(StoryNode storyNode)
    {
        DialogueNode dialogueNode = storyNode.Node as DialogueNode;
        if (dialogueNode == null)
        {
            canvasGroup.alpha = 0;
            return;
        }
        canvasGroup.alpha = 1;
        StartCoroutine(TypeText(dialogueNode));
    }
    //打字机效果，有bug，如果连点会叠加协程。细节问题，先用着
    IEnumerator TypeText(DialogueNode dialogue)
    {
        
        textDisplay.text = "";
        foreach (char letter in dialogue.Text)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        Typingcoroutine = null;
    }
}