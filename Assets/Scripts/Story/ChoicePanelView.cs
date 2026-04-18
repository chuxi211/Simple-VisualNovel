using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePanelView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CoreController.Instance.StoryController.RegisterChoicePanel(transform);
    }

    
}
