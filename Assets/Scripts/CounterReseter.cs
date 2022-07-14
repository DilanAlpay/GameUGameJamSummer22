using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterReseter : MonoBehaviour
{
    public List<CounterObj> counters;
    // Start is called before the first frame update
    void Start()
    {
        foreach (CounterObj counter in counters)
        {
            counter.SetTo(0);
        }    
    }

    
}
