using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour, IPausable
{
    #region Properties
    
    #region Public Properties
    public AnimationCurve bounceCurve;
    public LayerMask ground;
    public LayerMask bounceSurfaces;
    #endregion

    #region Accesible Properties
    /// <summary>
    /// How long one bounce takes
    /// </summary>
    [SerializeField]
    private float _speed = 0.5f;

    [SerializeField]
    private float _minHeight = 2;

    [SerializeField]
    private float _maxHeight = 5;

    /// <summary>
    /// Mmust be thrown farther than this to leave the Thrower
    /// This will increase with Size
    /// </summary>
    [SerializeField]
    private float _minDistance = 2;

    [SerializeField]
    private float _maxDistance = 5;

    public float Range => _maxDistance;

    /// <summary>
    /// How large this object currently is
    /// </summary>
    private int _size = 1;
    #endregion

    #region Private Properties
    /// <summary>
    /// Percentage of force to use when thrown
    /// </summary>
    private float _force;

    /// <summary>
    /// How high the ball will be bouncing when thrown
    /// Calculated based on how the Player threw it
    /// </summary>
    private float _bounceHeight;

    /// <summary>
    /// How far it will move after each bounce since the last throw
    /// </summary>
    private float _distance;

    /// <summary>
    /// The direction that it will move in until hitting something else
    /// </summary>
    private Vector3 _direction;

    /// <summary>
    /// Where the object was when the current bounce was started
    /// </summary>
    private Vector3 _start;

    /// <summary>
    /// The position that it intends to land on next
    /// </summary>
    private Vector3 _goal;

    /// <summary>
    /// How close we are to our goal
    /// </summary>
    private float _elapsed = 0;

    /// <summary>
    /// Handles starting and stopping motion
    /// </summary>
    private IEnumerator _movement;

    private bool isPaused = false;
    #endregion
    #endregion
    
    
    /// <summary>
    /// Starts the object in motion
    /// </summary>
    /// <param name="direction"> The normalized direction to throw this object</param>
    /// <param name="force"> Percentage of the _maxDistance it will go per bounce</param>
    public bool Throw(Vector3 direction, float force)
    {
        _direction = direction;
        _force = force;

        _distance = _maxDistance * _force;

        //We do not throw the ball if it's too close to us
        if (_distance < _minDistance) return false;

        CalcBounceHeight();

        //Otherwise it will start moving
        CalcGoal();

        _start = transform.position;
        _elapsed = 0;

        _movement = MoveToGoal();
        StartCoroutine(_movement);

        return true;
    }

    private void CalcBounceHeight()
    {
        _bounceHeight = Mathf.Lerp(_maxHeight, _minHeight, _force);
    }
    
    private void CalcGoal()
    {
        float left = 1f - (_elapsed / _speed);
        left *= _distance;
        //Position where we want it to land
        Vector3 newGoal = transform.position + (_direction.normalized * left);

        newGoal.y = transform.position.y + _bounceHeight;
        //Calculate the final Y position
        RaycastHit hit;
        if (Physics.SphereCast(newGoal, _size, Vector2.down, out hit, Mathf.Infinity, ground))
        {
            newGoal.y = hit.point.y;
        }

        _goal = newGoal;               
    }

    IEnumerator MoveToGoal()
    {
        float startY = _start.y;
        while(_elapsed < _speed)
        {
            float t = _elapsed / _speed;
            Vector3 pos = Vector3.Lerp(_start, _goal, t);
            float y = Mathf.Lerp(startY, _goal.y, t);
            pos.y = y + (bounceCurve.Evaluate(t) * _bounceHeight);

            transform.position = pos;
            _elapsed += isPaused ? 0 : Time.deltaTime;
            
            yield return null;
        }

        transform.position = _goal;
        Bounce();
    }

    private void Bounce()
    {
        _elapsed = 0;
        _start = transform.position;
        CalcBounceHeight();
        CalcGoal();
        _movement = MoveToGoal();
        StartCoroutine(_movement);
    }

    private void Update()
    {
        if(_movement != null)
            CheckForCollision();
    }

    private void CheckForCollision()
    {
        //The pivot point of the object is always the bottom
        //We move the center up relative to the size
        Vector3 center = transform.position + Vector3.up * (_size / 2f);
        RaycastHit hit;
        if (!Physics.SphereCast(center, _size / 2f, _direction, out hit, (_size * 0.1f), bounceSurfaces))
            return;

        _start = hit.point;
        _direction = hit.normal;

        CalcGoal();
    }

    public void Grab()
    {
        if(_movement != null)
            StopCoroutine(_movement);
        _movement = null;
        _elapsed = 0;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_goal, 0.5f);
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Unpause()
    {
        isPaused = false;
    }
}
