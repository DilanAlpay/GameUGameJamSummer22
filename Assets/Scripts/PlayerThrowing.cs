using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowing : MonoBehaviour
{
    #region Properties
    #region Public Properties
    public GameObject indicator;
    public Transform rangeDisplay;
    public InputObj inputThrow;
    public InputObj inputAimControl;
    public LayerMask ground;
    #endregion

    #region Animation Hashes
    private int _hashAiming;
    #endregion

    #region References
    private Animator _animator;

    /// <summary>
    /// The object that the child is attached to
    /// </summary>
    private Transform _hold;

    [SerializeField]
    private Throwable _item;
    #endregion

    #region Accessible Properties
    private float _grab = 1;
    /// <summary>
    /// A value in pixels
    /// </summary>
    [SerializeField]
    private float _aimRange = 300;
    #endregion

    #region Private Properties
    private bool _aiming = false;

    /// <summary>
    /// Relative position when aiming is started
    /// </summary>
    private Vector2 _aimStart;

    /// <summary>
    /// Relative position when aiming is finished
    /// </summary>
    private Vector2 _aimEnd;

    /// <summary>
    /// Direction that we want to throw the object
    /// </summary>
    private Vector3 _direction;

    /// <summary>
    /// Percentage of how far the object should go
    /// </summary>
    private float _throwForce;
    #endregion
    #endregion
    
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
        rangeDisplay.gameObject.SetActive(false);

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
        OnAim(ctx.ReadValue<Vector2>());
    }

    void OnThrow(InputAction.CallbackContext ctx)
    {
        Throw();
    }
    #endregion

    void StartThrow()
    {
        _aiming = true;
        _animator.SetBool(_hashAiming, true);
    }

    void LookForItem()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, _grab);

        foreach (Collider c in nearby)
        {
            print(c.name);
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
        t.Grab();
    }

    void OnAim(Vector2 v)
    {
        if (!_aiming) return;

        //Initial location
        if (_aimStart == Vector2.zero)
        {
            _aimStart = v;
            rangeDisplay.localScale = Vector3.zero;
            indicator.SetActive(true);
            rangeDisplay.gameObject.SetActive(true);
            return;
        }
        
        _aimEnd = v;       
    }

    private void Aim()
    {
        if (!_aiming || _aimStart == Vector2.zero) return;
        
        Vector2 difference = _aimEnd - _aimStart;

        //Angle for the object to be thrown
        float angle = 180 + Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

        //Percentage from full throw distance we are 
        _throwForce = Mathf.Clamp(difference.magnitude, 0, _aimRange) / _aimRange;

        //Update the rangeDisplay
        rangeDisplay.localEulerAngles = Vector3.up * angle;
        _direction = rangeDisplay.forward;

        float distance = _throwForce * _item.Range;
        rangeDisplay.localScale = new Vector3(1, 1, distance);
        
        //Update indicator
        indicator.transform.position = transform.position + (rangeDisplay.forward * distance);
    }

    private void Update()
    {
        if(_item)
            Aim();
        else
            LookForItem();

    }

    void Throw()
    {
        if (!_aiming) return;

        //Send the throwable the direction I want it to go in
        //Along with the percentage it should reach between the min and max
        //If the distance is not enough the Throwable will handle not being thrown

        //Turn off aiming UI and reset variables

        rangeDisplay.gameObject.SetActive(false);
        indicator.gameObject.SetActive(false);
        _aiming = false;
        _aimStart = Vector3.zero;
        rangeDisplay.localScale = Vector3.zero;

        _animator.SetBool(_hashAiming, false);

        if(_item.Throw(_direction, _throwForce))
        {
            _item.transform.parent = null;
            _item = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _grab / 2f);
    }
}
