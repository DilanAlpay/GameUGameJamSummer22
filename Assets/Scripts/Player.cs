using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerThrowing _throwing;

    public bool HasBall { get { return _throwing.HasBall; } }

    // Start is called before the first frame update
    void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _throwing = GetComponent<PlayerThrowing>();
    }

    public void SetPause(bool b)
    {
        _movement.enabled = !b;
        _throwing.enabled = !b;
    }

    public void TeleportTo(Vector3 newPos)
    {
        _movement.TeleportTo(newPos);
    }
}
