using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskCollider : MonoBehaviour
{
    [SerializeField] LayerMask targetLayers;
    [SerializeField] GameObjectEvent onCollision;

    public void SetTargets(LayerMask layers)
    {
        targetLayers = layers;
    }
    private void OnTriggerEnter(Collider other)
    {

        if ((targetLayers.value & (1 << other.transform.gameObject.layer)) > 0)
        {
<<<<<<< Updated upstream
            Debug.Log($"Hit target - {other.name}");
            if(onCollision.GetPersistentEventCount() > 0)
            {
                onCollision.Invoke(other.gameObject);
            }
=======
            onCollision?.Invoke(other.gameObject);
>>>>>>> Stashed changes
        }

    }
}
