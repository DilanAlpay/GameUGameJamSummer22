using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateEvent : MonoBehaviour
{
    public UnityEvent update;

    // Update is called once per frame
    void Update()
    {
        update?.Invoke();
    }
}
