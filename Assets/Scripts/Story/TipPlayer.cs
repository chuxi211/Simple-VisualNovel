using Assets.Scripts.Story;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipPlayer : MonoBehaviour
{
    TextMeshProUGUI textDisplay;
    CanvasGroup canvasGroup;
    private void Awake()
    {
        textDisplay=GetComponent<TextMeshProUGUI>();
        canvasGroup=GetComponent<CanvasGroup>();
        textDisplay.enableWordWrapping = false;
    }
    private void OnEnable()
    {
        EventBus.Subscribe<StoryNodeChangedEvent>(PlayTip);
    }
    private void OnDisable()
    {
        EventBus.UnSubscribe<StoryNodeChangedEvent>(PlayTip);
    }
    private void PlayTip(StoryNodeChangedEvent storyNodeChangedEvent)
    {
        StoryNode storyNode = CoreController.Instance.StoryController.GetNodeById(storyNodeChangedEvent.nodeId);
        if(storyNode== null)
        {
            Debug.LogError($"storyNode{storyNode.Node.ID} is null");
            return;
        }
        TipNode tipNode = storyNode.Node as TipNode;
        if(tipNode == null)
        {
            canvasGroup.alpha = 0;
            return;
        }
        canvasGroup.alpha = 1;
        StartCoroutine(TypeText(tipNode));
    }
    [Obsolete("使用PlayTip(StoryNodeChangedEvent storyNodeChangedEvent)方法替代")]
    private void PlayTip(StoryNode storynode)
    {
        TipNode tipNode = storynode.Node as TipNode;
        if (tipNode == null)
        {
            canvasGroup.alpha = 0;
            return;
        }
        canvasGroup.alpha = 1;
        StartCoroutine(TypeText(tipNode));
    }
    IEnumerator TypeText(TipNode tipNode)
    {
        textDisplay.text = ""; 
        foreach(var letter in tipNode.Text)
        {
            textDisplay.text += letter.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
    
}
