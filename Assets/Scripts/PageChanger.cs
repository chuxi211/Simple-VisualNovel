using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChanger : MonoBehaviour
{
    public PageChangeType changeType;
    public event System.Action<PageChangeType> OnPageChange;
    public void Onclick()
    {
        Debug.Log($"State:{changeType.ToString()}");
        switch (changeType)
        {
            case PageChangeType.Next:
                SaveManager.NextPage();
                break;
            case PageChangeType.Previous:
                SaveManager.PreviousPage(); 
                break;
        }
    }
}
public enum PageChangeType
{
    Next,
    Previous
}