using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public List<GameEvent> events;

    private void OnEnable()
    {
        foreach (GameEvent e in events)
        {
            e.StartListening(this);
        }
    }

    private void OnDisable()
    {
        foreach (GameEvent e in events)
        {
            e.StopListening(this);
        }
    }

    public virtual void Call()
    {}

    public virtual void Call<AnyType>(AnyType obj)
    {}
}
