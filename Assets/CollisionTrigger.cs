using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] LayerMask targetLayers;
    public UnityEvent onHit;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if ((targetLayers.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
         
            
                onHit.Invoke();
            
        }
       
    }
}
