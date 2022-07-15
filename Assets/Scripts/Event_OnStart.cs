using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Event_OnStart : MonoBehaviour
{
    public float delay;
    public UnityEvent2 onStart;
    public bool destroyAfter;

    void OnEnable()
    {
        Invoke("CallEvent", delay);
        if (destroyAfter)
            Destroy(this);
    }


    public void CallEvent()
    {
        onStart.Invoke();
    }
}
