using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Listener_Simple : Listener
{
    public UnityEvent response;

    public override void Call()
    {
        response.Invoke();
    }
}
