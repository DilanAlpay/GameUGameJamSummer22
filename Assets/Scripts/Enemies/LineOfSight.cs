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


    private void Update()
    {
        if (!active) return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, visionRadius, layers);
            if (colliders.Length != 0)
            {
                if(!_hasTarget)
                    SeesTarget(colliders[0]);
            }
            else
            {
                if (_hasTarget)
                    LostTarget();
            }

        




    }

    public void SetActive(bool isActive)
    {
        active = isActive;
    }

    private void LostTarget()
    {
        OnLoseTarget?.Invoke();
        target = null;
        _hasTarget = false;
    }

    public GameObject GetTarget()
    {
        if (!_hasTarget) return null;

        return target.gameObject;
    }

    private void SeesTarget(Collider newTarget)
    {
        OnSeeTarget?.Invoke(newTarget.transform);
        target = newTarget;
        _hasTarget = true;
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
