using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acceleration = 0;
    public void UpdateHomingPosition(Vector3 target)
    {

        transform.LookAt(target);
        Move();
        
    }

    public void UpdatePosition()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        speed += acceleration * Time.deltaTime;
    }

}
