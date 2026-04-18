using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePlayer : MonoBehaviour
{
    TextMeshProUGUI text;
    string nextID;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    public void Init(string choiceText,string nextID)
    {
        text.text = choiceText;
        this.nextID = nextID;
        GetComponent<Button>().onClick.RemoveAllListeners();  //
        GetComponent<Button>().onClick.AddListener(() =>
        {
            EventBus.Publish(new ChoiceSelectedEvent(this.nextID));
        });
        
    }
    public void onclick()
    {
        Debug.Log("Choice Onclick");
    }
}
