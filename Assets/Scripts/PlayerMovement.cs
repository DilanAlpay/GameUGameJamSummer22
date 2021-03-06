using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    #region Public Properties
    public InputObj inputMove;
    public InputObj inputJump;
    #endregion

    #region References
    private CharacterController _controller;
    public CharacterController Controller => _controller;
    private Animator _animator;
    /// <summary>
    /// The child of the root that is rotated/flipped during movement
    /// </summary>
    private Transform _geo;
    #endregion

    #region Variable Properties
    [SerializeField]
    private float _speed;
    /// <summary>
    /// Max jump height
    /// </summary>
    [SerializeField]
    private float _jumpHeight = 2;
    /// <summary>
    /// Max duration of a jump before gravity kicks in
    /// </summary>
    [SerializeField]
    private float _jumpDur = 0.75f;

    [SerializeField]
    private float _fall = 2f;

    /// <summary>
    /// A value that changes the speed through outside sources
    /// </summary>
    private float _speedMult;
    #endregion

    #region Condition Boolean
    private bool _isMoving;
    private bool _isJumping;
    private bool _isFalling;
    private bool _isJumpPressed;
    #endregion

    #region Animation Hashes
    private int _hashDirection;
    private int _hashWalking;
    private int _hashJumping;
    #endregion

    #region Private Properties
    /// <summary>
    /// An accessor for the X and Y input from the controller
    /// </summary>
    private Vector2 _moveInput;

    private float _gravity = -9.8f;
    /// <summary>
    /// Jump velocity calculated with jumpHeight and jumpDuration
    /// </summary>
    private float _jump;

    /// <summary>
    /// Applied movement direction
    /// </summary>
    private Vector3 _direction;

    #endregion


    #region Setup
    private void Awake()
    {
        Initialization();
    }

    public void Initialization()
    {
        //References
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _geo = transform.Find("GEO");

        //Animation hashes
        _hashDirection = Animator.StringToHash("direction");
        _hashWalking = Animator.StringToHash("walking");
        _hashJumping = Animator.StringToHash("jumping");

        //Jump Velocity Calculation
        //I do not know how this math works
        //I just know it does
        _jump = (2 * _jumpHeight) / (_jumpDur / 2);
        _gravity = (-2 * _jumpHeight) / Mathf.Pow((_jumpDur / 2), 2);
    }

    private void OnEnable()
    {
        EnableCallbacks();
    }

    private void OnDisable()
    {
        _direction = Vector3.zero;

        if (inputMove.Action == null) return;
        DisableCallbacks();
    }

    public void ResetVariables()
    {
        _isMoving = false;
    }

    private void EnableCallbacks()
    {
        inputMove.Action.started += OnMove;
        inputMove.Action.canceled += OnMove;
        inputMove.Action.performed += OnMove;
        inputJump.Action.started += OnJump;
        inputJump.Action.canceled += OnJump;
    }

    private void DisableCallbacks()
    {
        inputMove.Action.started -= OnMove;
        inputMove.Action.canceled -= OnMove;
        inputMove.Action.performed -= OnMove;
        inputJump.Action.started -= OnJump;
        inputJump.Action.canceled -= OnJump;
    }
    #endregion

    #region Input Callbacks
    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        
        _direction.x = _moveInput.x * _speed;
        _direction.z = _moveInput.y * _speed;

        _isMoving = _moveInput != Vector2.zero;
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        _isJumpPressed = ctx.ReadValueAsButton();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Vector3 appliedMovement = _direction;

        _controller.Move(appliedMovement * Time.deltaTime);

        HandleGravity();
        HandleJump();
        HandleAnimation();
    }

    void HandleJump()
    {
        if (!_isJumping && _controller.isGrounded && _isJumpPressed)
        {
            _isJumping = true;
            _direction.y = _jump;
        }
        else if (!_isJumpPressed && _isJumping && _controller.isGrounded)
        {
            _isJumping = false;
        }

        _animator.SetBool("jumping", _isJumping);
    }

    void HandleGravity()
    {
        _isFalling = _direction.y <= 0 || !_isJumpPressed;

        if (_controller.isGrounded)
        {
            _direction.y = _gravity;
        }
        else
        {
            float lastY = _direction.y;
            float grav = (_gravity * Time.deltaTime);
            grav = _isFalling ? grav * _fall : grav;
            _direction.y = _direction.y + grav;

            //Caps the fall speed at -20 or otherwise takes the average
            _direction.y = Mathf.Max((lastY + _direction.y) * 0.5f, -20f);
        }
    }

    void HandleAnimation()
    {
        if (!_animator) return;
        _animator.SetInteger(_hashDirection, (int)_moveInput.x);
        _animator.SetBool(_hashWalking, _isMoving);
    }

    public void TeleportTo(Vector3 newPos)
    {
        //This is a necessary step when working with CharacterControllers
        //No, I do not know why
        _controller.enabled = false;
        _isMoving = false;
        transform.position = newPos;
        _controller.enabled = true;
    }

    
}
