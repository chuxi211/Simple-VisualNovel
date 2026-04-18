using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    //存储事件类型和对应的委托列表
    private static Dictionary<Type, Delegate> eventMap = new();
    //将事件作为参数传入并添加，相当于OnEnable中注册事件
    //订阅者在OnEnable中订阅事件（使用Unity的生命周期函数）
    public static void Subscribe<T>(Action<T> callback)
    {
        if(callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }
        Type type = typeof(T);
        Debug.Log($"Subscribe {callback.ToString()} to event type {type}");
        if (!eventMap.ContainsKey(type))
        {
            eventMap[type] = null;
        }
        eventMap[type] =(Action<T>) eventMap[type] + callback;
    }
    //将事件作为参数传入并移除
    //订阅者在OnDisable中取消订阅事件（使用Unity的生命周期函数）
    public static void UnSubscribe<T>(Action<T> callback) { 
        Type type = typeof(T);
        if (!eventMap.ContainsKey(type))
        {
            return;
        }
        eventMap[type] = (Action<T>)eventMap[type] - callback;
        if (eventMap[type] == null)
        {
            eventMap.Remove(type);
        }
    }
    //发布事件
    public static void Publish<T>(T eventData)
    {
        Type type = typeof(T);
        if(eventMap.TryGetValue(type, out Delegate @delegate))
        {
            Action<T> callback = @delegate as Action<T>;
            Debug.Log($"Try Publish {callback.ToString()}");
            callback?.Invoke(eventData);
        }
    }
    public static void Clear()
    {
        eventMap.Clear();
    }
}