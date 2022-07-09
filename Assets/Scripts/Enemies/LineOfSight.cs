using GameJam.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] float visionRadius = 5f;
    [SerializeField] bool active = true;
    [SerializeField] Transform target;
    [SerializeField] LoggerTag loggerTag;
    [SerializeField] UnityEvent OnSeeTarget;
    [SerializeField] UnityEvent OnLoseTarget;
    

    bool _hasTarget = false; 

    public float VisionRadius => visionRadius;
    public bool Active => active;

    public bool HasTarget => _hasTarget;

    bool warning;
    private void Update()
    {
        if (!active) return;
        if (target == null)
        {
            if (!warning)
            {
                LoggerManager.i.Log($"{name} does not have a Line Of Sight target assigned!", loggerTag);
                warning = true;
            }
            return;
        }

        float distToTarget = Vector3.Distance(transform.position, target.position);

        if(!_hasTarget && distToTarget <= visionRadius)
        {
            SeesTarget();
        }

        if(_hasTarget && distToTarget > visionRadius)
        {
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
        _hasTarget = false;
    }

    private void SeesTarget()
    {
        OnSeeTarget?.Invoke();
        _hasTarget = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}
