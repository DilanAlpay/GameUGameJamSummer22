using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask ground;

    private PlayerMovement _movement;
    private PlayerThrowing _throwing;
    private Interactor _interactor;
    private Animator _animator;
    private HealthPlayer _health;

    private int _hashHurt;
    private int _hashHurting;
    private int _hashIdle;
    private int _hashDie;

    public bool HasBall { get { return _throwing.HasBall; } }

    // Start is called before the first frame update
    void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _throwing = GetComponent<PlayerThrowing>();
        _interactor = GetComponent<Interactor>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<HealthPlayer>();

        _hashHurt = Animator.StringToHash("Hurt");
        _hashHurting = Animator.StringToHash("hurting");
        _hashIdle = Animator.StringToHash("Idle");
        _hashDie = Animator.StringToHash("Die");
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

    public void Hurt(GameObject source)
    {
        if(_health.HP > 0)
        {
            Vector3 direction = source.transform.position - transform.position;
            StartCoroutine(Knockback(direction));
        }
    }

    public IEnumerator Knockback(Vector3 direction)
    {
        _animator.SetBool(_hashHurting, true);
        _animator.Play(_hashHurt);
        SetPause(true);
        _movement.Controller.enabled = false;

        float distance = 1;
        float height = 1;
        float duration = 0.5f;
        float elapsed = 0;
        float stun = 0.5f;

        Vector3 start = transform.position;
        Vector3 end = transform.position - (direction.normalized * distance);
        RaycastHit hit;

        if (Physics.Raycast(end + (Vector3.up * height), Vector3.down, out hit, Mathf.Infinity, ground))
        {
            end.y = hit.point.y;
        }


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
        _animator.SetBool(_hashHurting, false);
        SetPause(false);
    }

    public void StartDeath()
    {
        //Stop moving
        SetPause(true);
        //Play animation
        _animator.Play(_hashDie);
    }

    /// <summary>
    /// Puts you right back at the beginning
    /// </summary>
    public void ReturnToStart()
    {
        TeleportTo(Vector3.zero);
        _animator.Play(_hashIdle);
        _throwing.GetItemBack();
        _health.Revive();
        _health.Heal(4);

    }


}
