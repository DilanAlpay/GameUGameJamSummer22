using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowing : MonoBehaviour
{
    #region Public Properties
    public GameObject indicator;
    public GameObject rangeDisplay;
    public LineRenderer _path;
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

    #region Editable Properties
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _closeRange = 2;
    [SerializeField]
    private float _throwRange = 12;
    [SerializeField]
    private float _aimRange = 12;
    [SerializeField]
    private float _height = 5;
    [SerializeField]
    private float _maxRange = 8;
    #endregion

    #region Private Properties
    private bool _aiming = false;
    private bool _inRange = false;

    /// <summary>
    /// Relative position when aiming is started
    /// </summary>
    private Vector2 _aimStart;

    /// <summary>
    /// Relative position when aiming is finished
    /// </summary>
    private Vector2 _aimEnd;

    /// <summary>
    /// Where the ball will want to end up
    /// </summary>
    private Vector3 _dest;
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

        //Initial location
        if (_aimStart == Vector2.zero)
        {
            _aimStart = v;
            return;
        }
        
        _aimEnd = v;       
    }

    /*
        void ShowArc()
        {

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







        print(v);
        Ray ray = Camera.main.ScreenPointToRay(v);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, ground)) return;
        
        indicator.transform.position = hit.point;
        _inRange = true;

        Vector3 pos = indicator.transform.localPosition;
        pos *= -1;
        indicator.transform.localPosition = pos;
                     
        rangeDisplay.transform.LookAt(indicator.transform.position);
        //Debug.DrawLine(hit.point, indicator.transform.position, Color.red);

        //rangeDisplay.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, angle));
        }
    */


    private void Update()
    {
        Vector2 difference = _aimEnd - _aimStart;

        float angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

        rangeDisplay.transform.localEulerAngles = Vector3.up * (180 + angle);

        float distance = Mathf.Clamp(difference.magnitude, 0, _aimRange) / _aimRange * _maxRange;
        rangeDisplay.transform.localScale = new Vector3(1, 1, distance);

        _dest = transform.position + (rangeDisplay.transform.forward * distance);
        indicator.transform.position = _dest;
        indicator.name = _dest.ToString();
    }

    void Throw()
    {
        if (!_aiming) return;


        //Send the throwable the direction I want it to go in
        //Along with the percentage it should reach between the min and max
        //If the distance is not enough the Throwable will handle not being thrown

        //Turn off aiming UI and reset variables
        rangeDisplay.SetActive(false);
        _aiming = false;
        _aimStart = Vector2.zero;
        _animator.SetBool(_hashAiming, false);
        _item.Throw(_dest, _speed);
        _item = null;
    }
}
