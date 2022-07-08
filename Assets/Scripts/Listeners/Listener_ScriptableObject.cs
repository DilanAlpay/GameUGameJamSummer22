using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Listener_ScriptableObject : ListenerBase<ScriptableObject>
{
    public UnityEvent_ScriptableObject myResponse;

    public override void Call(ScriptableObject obj)
    {
        response = myResponse;
        base.Call(obj);
    }
}


[System.Serializable]
public class UnityEvent_ScriptableObject : UnityEvent<ScriptableObject>
{

}
