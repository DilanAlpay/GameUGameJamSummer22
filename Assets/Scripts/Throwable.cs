using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    #region Public Properties
    public AnimationCurve bounceCurve;
    public LayerMask ground;
    #endregion

    #region Editable Properties
    //[SerializeField]
    //private int _bounces = 2;
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

    /// <summary>
    /// How large this object currently is
    /// </summary>
    private int _size = 1;
    #endregion

    #region Private Properties
    /// <summary>
    /// How high the ball will be bouncing when thrown
    /// Calculated based on how the Player threw it
    /// </summary>
    private float _bounceHeight;

    /// <summary>
    /// How far it will move after each bounce
    /// </summary>
    private float _distance;

    /// <summary>
    /// The direction that it will move in until hitting something else
    /// </summary>
    private float _angle;   

    /// <summary>
    /// The position that it intends to land on next
    /// </summary>
    private Vector3 _goal;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"> The normalized direction to throw this object</param>
    /// <param name="force">Percentage of the _maxDistance to go</param>
    public bool Throw(float angle, float force)
    {
        _distance = _maxDistance * force;

        //We do not throw the ball if it's too close to us
        if (_distance < _minDistance) return false;

        //Otherwise it will start moving
        _angle = angle;
        _bounceHeight = Mathf.Lerp(_minHeight, _maxHeight, force);

        CalcGoal();

        return true;
    }


    private void CalcGoal()
    {

    }


    public void Throw(Vector3 d, float t)
    {
        transform.parent = null;

        //I now need to make it so it's a tall arc if short and a short arc if long


        StartCoroutine(MoveToPoint(d, t));
        print(d);
    }



    IEnumerator MoveToPoint(Vector3 dest, float time)
    {
        Vector3 start = transform.position;
        float elapsed = 0;

        while(elapsed < time)
        {
            Vector3 pos = Vector3.Lerp(start, dest, elapsed / time);
            pos.y = bounceCurve.Evaluate(elapsed / time) * 5;
            transform.position = pos;
            //transform.position = MathParabola.Parabola(start, dest, height, elapsed/time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = dest;
    }


}
