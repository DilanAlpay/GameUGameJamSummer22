using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask ground;

    private PlayerMovement _movement;
    private PlayerThrowing _throwing;
    private Interactor _interactor;

    public bool HasBall { get { return _throwing.HasBall; } }

    // Start is called before the first frame update
    void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _throwing = GetComponent<PlayerThrowing>();
        _interactor = GetComponent<Interactor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Knocked();
        }
    }

    public void SetPause(bool b)
    {
        _movement.enabled = !b;
        _throwing.enabled = !b;
        _interactor.enabled = !b;
    }

    public void TeleportTo(Vector3 newPos)
    {
        _movement.TeleportTo(newPos);
    }

    public void Knocked()
    {
        StartCoroutine(Knockback(Vector3.right));
    }

    public IEnumerator Knockback(Vector3 direction)
    {
        SetPause(true);
        _movement.Controller.enabled = false;

        float distance = 1;

        Vector3 start = transform.position;
        Vector3 end = transform.position - (direction.normalized * distance);
        RaycastHit hit;

        if (Physics.Raycast(end, Vector3.down, out hit, Mathf.Infinity, ground))
        {
            end.y = hit.point.y;
        }

        float height = 1;
        float duration = 0.5f;
        float elapsed = 0;
        float stun = 1;

        while (elapsed < duration)
        {
            Vector3 pos = MathParabola.Parabola(start, end, height, elapsed / duration);
            elapsed += Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
        transform.position = end;

        yield return new WaitForSeconds(stun);

        _movement.Controller.enabled = true;
        SetPause(false);
    }
}
