using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ListenerBase<T> : Listener
{
    [SerializeField]
    public UnityEvent<T> response;

    public virtual void Call(T obj)
    {
        response.Invoke(obj);
    }
}
