using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CounterCheck : MonoBehaviour
{
    public CounterObj counter;
    public int requirement;

    public UnityEvent onCountReached;

    public void Check()
    {
        if(counter.Count >= requirement)
        {
            onCountReached.Invoke();
        }
    }

}
