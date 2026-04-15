using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    private static Dictionary<Type, Delegate> eventTable = new Dictionary<Type, Delegate>();

    public static void Subscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);
            
        if (eventTable.TryGetValue(type, out Delegate del))
        {
            eventTable[type] = (Action<T>)del + handler;
        }
        else
        {
            eventTable[type] = handler;
        }
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        Type type = typeof(T);

        if (eventTable.TryGetValue(type, out Delegate del))
        {
            Action<T> currentDel = (Action<T>)del;
            currentDel -= handler;

            if (currentDel == null)
                eventTable.Remove(type);
            else
                eventTable[type] = currentDel;
        }
    }

    public static void Publish<T>(T eventData)
    {
        Type type = typeof(T);

        if (eventTable.TryGetValue(type, out Delegate del))
        {
            ((Action<T>)del)?.Invoke(eventData);
        }
    }

    public static void Clear()
    {
        eventTable.Clear();
    }
}
