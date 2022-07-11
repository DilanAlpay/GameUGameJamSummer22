using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPausable
{
    [SerializeField] float speed;
    [SerializeField] bool isHoming;
    [SerializeField] Transform targetTest;
    Transform target;
    bool isPaused;

    private void Start()
    {
        if (targetTest != null)
        {
            SetTarget(targetTest);
        }
    }

    public void SetTarget(Transform newTarget, float damage = 0)
    {
        target = newTarget;
        transform.LookAt(GetAimLocation());
    }

    private void Update()
    {
        if (isPaused) return;

        if (isHoming)
        {
            transform.LookAt(GetAimLocation());
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    Vector3 GetAimLocation()
    {
        return new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    public void Pause()
    {
        isPaused = true;
        
    }

    public void Unpause()
    {
        isPaused = false;
    }
}
