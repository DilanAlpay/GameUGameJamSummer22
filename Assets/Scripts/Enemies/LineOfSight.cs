using GameJam.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] float visionRadius = 5f;
    [SerializeField] bool active = true;
    [SerializeField] LayerMask layers;
    [SerializeField] LoggerTag loggerTag;
    [SerializeField] TransformEvent OnSeeTarget;
    [SerializeField] UnityEvent OnLoseTarget;

    Collider target;
    bool _hasTarget = false; 

    public float VisionRadius => visionRadius;
    public bool Active => active;

    public bool HasTarget => _hasTarget;

 

    public GameObject GetTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, visionRadius, layers);
        
        if (colliders.Length != 0)
        {
            target = colliders[0];
            return colliders[0].gameObject;
        }
        target = null;
        return null;
    }


    public void SetTargetLayers(LayerMask layerMask)
    {
        layers = layerMask;
    }

    public void SetRadius(float radius)
    {
        visionRadius = radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}
