using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] bool fireOnDestroy = false;
    [SerializeField] UnityEvent onTimerEnd = new UnityEvent();
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerCO(duration));
    }


    private void OnDestroy()
    {
        if (fireOnDestroy)
        {
            OnTimerEnd();
        }
    }
    private IEnumerator TimerCO(float duration)
    {
        yield return new WaitForSeconds(duration);
        OnTimerEnd();
    }


    public void EndTimerEarly(bool fireAction)
    {
        StopAllCoroutines();
        if (fireAction)
        {
            OnTimerEnd();
        }
    }
    void OnTimerEnd()
    {
        onTimerEnd?.Invoke();
    }
}
