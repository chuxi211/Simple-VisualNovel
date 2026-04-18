using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataBox : MonoBehaviour
{
    public int Index { get; private set; }
    private SaveDataPanel panel;
    private void Awake()
    {
        
    }

    public void Init(SaveDataPanel p, int idx)
    {
        this.panel = p;
        this.Index = idx;
    }
    public void Onclick()
    {
        if (panel == null)
        {
            Debug.LogError("panel is null");
            return;
        }
        panel.Save(Index);
    }
}
