using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowing : MonoBehaviour
{
    public GameObject indicator;
    public GameObject rangeDisplay;
    public LineRenderer _path;
    public InputObj inputThrow;
    public InputObj inputAimControl;

    public LayerMask ground;

    #region Animation Hashes
    private int _hashAiming;
    #endregion

    private Animator _animator;

    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _closeRange = 2;
    [SerializeField]
    private float _throwRange = 12;
    [SerializeField]
    private float _height = 5;
    [SerializeField]
    private float _centerOffset = 2;

    /// <summary>
    /// The object that the child is attached to
    /// </summary>
    private Transform _hold;

    [SerializeField]
    private Throwable _item;

    private bool _aiming = false;

    private bool _inRange = false;

    private void Awake()
    {
        Initialization();
    }
    void Initialization()
    {
        //References
        _animator = GetComponent<Animator>();

        //Hashes
        _hashAiming = Animator.StringToHash("aiming");

        indicator.SetActive(false);
        rangeDisplay.SetActive(false);

        _hold = transform.Find("Hold");

        inputThrow.Action.started += OnStartThrow;
        inputAimControl.Action.performed += OnAim;
        inputThrow.Action.canceled += OnThrow;
    }


    #region Callbacks
    void OnStartThrow(InputAction.CallbackContext ctx)
    {
        //If we do have an item, we prepare to throw
        if (_item)
        {
            StartThrow();
        }
        //If we do not have an item, we look to pick one up
        else
        {
            LookForItem();
        }
    }

    void OnAim(InputAction.CallbackContext ctx)
    {
        Aim(ctx.ReadValue<Vector2>());
    }

    void OnThrow(InputAction.CallbackContext ctx)
    {
        Throw();
    }
    #endregion

    void StartThrow()
    {
        indicator.SetActive(true);
        rangeDisplay.SetActive(true);
        _aiming = true;
        _animator.SetBool(_hashAiming, true);
    }

    void LookForItem()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, _closeRange);

        foreach (Collider c in nearby)
        {
            if (c.GetComponentInParent<Throwable>())
            {
                PickUp(c.GetComponentInParent<Throwable>());
                break;
            }
        }
    }

    void PickUp(Throwable t)
    {
        _item = t;
        _item.transform.parent = _hold;
        _item.transform.localPosition = Vector3.zero;
    }

    void Aim(Vector2 v)
    {
        if (!_aiming) return;
        Ray ray = Camera.main.ScreenPointToRay(v);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) return;
        
        Vector3 pos = transform.position + _centerOffset * Vector3.forward;
        
        indicator.transform.position = hit.point;
        _inRange = true;

        Vector3 pos2 = indicator.transform.localPosition;
        pos2 *= -1;
        indicator.transform.localPosition = pos2;

        Vector3 start = transform.position;
        Vector3 dest = indicator.transform.position;
        int fidelity = 10;
        float thickness = _height + (_item.thickness / 0.5f);

        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < fidelity; i++)
        {
            points.Add(MathParabola.Parabola(start, dest, thickness, (float)i / (fidelity - 1f)));
        }
        _path.positionCount = fidelity;
        _path.SetPositions(points.ToArray());             
    }

    void Throw()
    {
        if (!_aiming) return;

        indicator.SetActive(false);
        rangeDisplay.SetActive(false);
        _aiming = false;
        _animator.SetBool(_hashAiming, false);

        if (_inRange)
        {
            _item.Throw(indicator.transform.position, _speed);
            _item = null;
        }
    }
}
