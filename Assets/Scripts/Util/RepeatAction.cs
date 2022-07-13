using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepeatAction : MonoBehaviour
{
    [Range(1,20)]
    [SerializeField] int timesRepeated;
    [SerializeField] UnityEvent action;

    public void Repeat()
    {
        for(int i = 0; i < timesRepeated; i++)
        {
            action?.Invoke();
        }
    }

}
