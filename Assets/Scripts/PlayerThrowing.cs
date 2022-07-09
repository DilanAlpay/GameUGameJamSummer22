using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowing : MonoBehaviour
{
    public GameObject indicator;
    public GameObject rangeDisplay;

    public InputObj inputThrow;
    public InputObj inputAimControl;

    public LayerMask ground;

    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _closeRange = 2;
    [SerializeField]
    private float _throwRange = 12;
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            Vector3 pos = transform.position + _centerOffset * Vector3.forward;
            if (Vector3.Distance(pos, hit.point) < _throwRange / 2f)
            {
                indicator.transform.position = hit.point;
                _inRange = true;
            }
        }
    }

    void Throw()
    {
        if (!_aiming) return;

        indicator.SetActive(false);
        rangeDisplay.SetActive(false);
        _aiming = false;

        if (_inRange)
        {
            _item.Throw(indicator.transform.position, _speed);
            _item = null;
        }
    }
}
