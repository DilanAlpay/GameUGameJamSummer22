using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Listener_Mono : ListenerBase<MonoBehaviour>
{
    public UnityEvent_Mono myResponse;

    public override void Call(MonoBehaviour obj)
    {
        response = myResponse;
        base.Call(obj);
    }
}


[System.Serializable]
public class UnityEvent_Mono : UnityEvent<MonoBehaviour>
{

}