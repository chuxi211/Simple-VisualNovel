using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int Index { get; private set; }
    private SaveDataPanel panel;

    public void Init(SaveDataPanel p,int index)
    {
        this.panel = p;
        this.Index = index;
    }
    public void Onclick()
    {
        Debug.Log("LoadDataBox clicked, index: " + Index);
        if (panel == null)
        {
            Debug.LogError("panel is null");
            return;
        }
        panel.Load(Index);
    }
}
