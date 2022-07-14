using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableZone : MonoBehaviour
{
    public UnityEvent onEnter;
    public bool destroyAfter;

    private void OnTriggerEnter(Collider other)
    {
        onEnter.Invoke();
        if (destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}
