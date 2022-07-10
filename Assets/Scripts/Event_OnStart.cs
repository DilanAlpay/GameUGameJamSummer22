using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Event_OnStart : MonoBehaviour
{
    public float delay;
    public UnityEvent2 onStart;

    void Start()
    {
        Invoke("CallEvent", delay);
    }


    public void CallEvent()
    {
        onStart.Invoke();
    }
}
