using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HasNoChildrenEvent : MonoBehaviour
{
    public UnityEvent noChildren;
    bool hasTriggered = false;

    private void Update()
    {
        if(transform.childCount == 0 && !hasTriggered)
        {
            Debug.Log("I have no children");
            hasTriggered = true;
            noChildren.Invoke();
        }
    }
}
