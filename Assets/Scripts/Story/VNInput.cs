using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VN : MonoBehaviour, IPointerClickHandler, IScrollHandler
{
    
    Image image;
    void Awake()
    {
        
        image= GetComponent<Image>();
    }
    void OnEnable()
    {
        EventBus.Subscribe<NextButtonUsedStateChanged>(UpdateButtonState);
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<NextButtonUsedStateChanged>(UpdateButtonState);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                //发布事件。
                //事件原本在类中定义，但现在直接在EventBus中定义了一个RequestNextEvent事件结构体
                //相当于
                //事件声明：类内——>public sturct/class
                //事件发布：类内?.Invoke——>EventBus.Publish封装
                //事件订阅：类内+=回调方法——>EventBus.Subscribe<T>(回调方法)，使用Unity生命周期函数OnEnable和OnDisable
                //事实上，使用构造函数和析构函数也可以实现订阅和取消订阅，但在Unity中，使用生命周期函数更为常见和方便。
                EventBus.Publish(new RequestNextEvent());
                break;
            case PointerEventData.InputButton.Right:
                EventBus.Publish(new RequestPrevEvent());
                break;
        }
             
    }

    public void OnScroll(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    void UpdateButtonState(NextButtonUsedStateChanged CanUsed)
    {

        image.raycastTarget = CanUsed.CanUsed;
    }

}
