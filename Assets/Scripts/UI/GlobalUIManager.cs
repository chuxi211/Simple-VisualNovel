using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalUIManager : MonoBehaviour  //后续优化为状态机
{

    private void OnEnable()
    {
        EventBus.Subscribe<UIAction>(HandleUIAction);
    }
    private void OnDisable()
    {
        EventBus.UnSubscribe<UIAction>(HandleUIAction);
    }
    public void HandleUIAction(UIAction action)
    {
        switch (action)
        {
            case UIAction.StartGame:
                EventBus.Publish(new RequestToStoryEvent());
                break;
            case UIAction.Setting:
                //SceneManager.LoadScene("");
                //还没有Setting
                break;
            case UIAction.RequesttoSaveAndLoad:
                EventBus.Publish<RequestSaveEvent>(new RequestSaveEvent());
                Debug.Log("Request to save and load.");
                break;
            /*case UIAction.SaveData:
                
                Debug.Log("Game saved.");
                break;*/
            case UIAction.BacktoTitle:
                SceneManager.LoadScene("Home");
                break;
            case UIAction.ExitGame:
                Application.Quit();
                Debug.Log("exit");
            break;
            case UIAction.RequestAuto:
                //CoreController.Instance.StoryController.Auto();
                break;
        }
    }
}
