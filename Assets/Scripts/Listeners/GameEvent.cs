using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<Listener> listeners = new List<Listener>();

    public void StartListening(Listener listener)
    {
        listeners.Add(listener);
    }
    public void StopListening(Listener listener)
    {
        listeners.Remove(listener);
    }

    public void Call()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            Listener newListener = listeners[i];
            newListener.Call();
        }
    }

    public void Call<T>(T obj)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            ListenerBase<T> newListener = (ListenerBase<T>)listeners[i];
            newListener.Call(obj);
        }
    }
}
