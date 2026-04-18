using Assets.Scripts.Story;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioClip music;
    AudioClip soundeffect;           //先放着，目前没有音效
    AudioClip currentmusic;          //判重用
    AudioClip currentsoundeffect;    //判重用
    AudioSource audioSource;
    private void Awake()
    {
        music = null;
        soundeffect = null;
        currentmusic = null;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        EventBus.Subscribe<StoryNodeChangedEvent>(PlayMusic);
    }
    private void OnDisable()
    {
        EventBus.UnSubscribe<StoryNodeChangedEvent>(PlayMusic);
    }
    void PlayMusic(StoryNodeChangedEvent e)
    {
        StoryNode storyNode = CoreController.Instance.StoryController.GetNodeById(e.nodeId);
        if (storyNode == null)
        {
            Debug.LogError("dialogue is null");
            return;
        }
        DialogueNode dialogueNode = storyNode.Node as DialogueNode;
        if (dialogueNode == null)
        {
            return;
        }
        if (dialogueNode.Bgm == null)
        {
            Debug.LogError("bgm is null");
            return;
        }
        //判重，后续改为assetbundle或者addressable后可以直接用地址判重
        if (currentmusic != null && dialogueNode.Bgm == currentmusic.name)
        {
            Debug.Log("same music");
            return;
        }
        music = Resources.Load<AudioClip>("audio/" + dialogueNode.Bgm);
        if (music == null)
        {
            Debug.LogError("bgm is null");
            return;
        }
        currentmusic = music;    //更新当前音乐
        audioSource.clip = music;
        audioSource.Play();
    }
    [Obsolete("使用PlayMusic(StoryNodeChangedEvent e)方法替代")]
    void Play(StoryNode storyNode)
    {
        if (storyNode == null)
        {
            Debug.LogError("dialogue is null");
            return;
        }
        DialogueNode dialogueNode = storyNode.Node as DialogueNode;
        if (dialogueNode == null)
        {
            return;
        }
        if (dialogueNode.Bgm == null)
        {
            Debug.LogError("bgm is null");
            return;
        }
        //判重，后续改为assetbundle或者addressable后可以直接用地址判重
        if (currentmusic != null && dialogueNode.Bgm == currentmusic.name)
        {
            Debug.Log("same music");
            return;
        }
        //先用这个，后续改为assetbundle或者addressable
        music = Resources.Load<AudioClip>("audio/"+dialogueNode.Bgm);
        if (music == null)
        {
            Debug.LogError("bgm is null");
            return;
        }
        currentmusic = music;    //更新当前音乐
        audioSource.clip = music;
        audioSource.Play();
    }
    
}
   

