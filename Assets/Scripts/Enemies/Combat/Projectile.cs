using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool isHoming;
    [SerializeField] Transform targetTest;
    Transform target;

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

}
