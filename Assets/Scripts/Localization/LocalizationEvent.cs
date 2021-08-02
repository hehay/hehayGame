using System;
using System.Collections.Generic;

public class LocalizationEvent
{
    private static Dictionary<LocalizationEventType, Delegate> dicEvents = new Dictionary<LocalizationEventType, Delegate>();

    //添加事件
    public static void AddListener(LocalizationEventType eventType, CallBack callback)
    {
        OnListenerAdding(eventType, callback);
        dicEvents[eventType] = (CallBack)dicEvents[eventType] + callback;
    }

    //移除事件
    public static void RemoveListener(LocalizationEventType eventType, CallBack callback)
    {
        OnListenerRemoving(eventType, callback);
        dicEvents[eventType] = (CallBack)dicEvents[eventType] - callback;
        OnListenerRemoved(eventType);

    }

    //事件的广播
    public static void BroadCast(LocalizationEventType eventType)
    {
        Delegate del;
        if (dicEvents.TryGetValue(eventType, out del))
        {
            CallBack callback = del as CallBack;
            if (callback != null)
            {
                callback();
            }
            else
            {
                throw new Exception(string.Format("广播事件错误，对应事件为空"));
            }
        }

    }


    private static void OnListenerAdding(LocalizationEventType eventType, Delegate callback)
    {
        if (!dicEvents.ContainsKey(eventType))
        {
            dicEvents.Add(eventType, null);
        }
        Delegate del = dicEvents[eventType];
        //判断该事件码对应的事件类型（参数）是否一样
        if (del != null && del.GetType() != callback.GetType())
        {
            throw new Exception(string.Format("尝试添加事件失败"));
        }
    }

    private static void OnListenerRemoving(LocalizationEventType eventType, Delegate callback)
    {
        if (dicEvents.ContainsKey(eventType))
        {
            Delegate del = dicEvents[eventType];
            if (del == null)
            {
                throw new Exception(string.Format("移除失败，对应事件为空"));
            }
            else if (del.GetType() != callback.GetType())
            {
                throw new Exception(string.Format("移除失败，对应事件不同"));
            }
        }
        else
        {
            throw new Exception(string.Format("移除失败，事件码为空"));
        }
    }

    private static void OnListenerRemoved(LocalizationEventType eventType)
    {
        if (dicEvents[eventType] == null)
        {
            dicEvents.Remove(eventType);
        }
    }

}

public enum LocalizationEventType
{
    ChangeLanguage,
}
