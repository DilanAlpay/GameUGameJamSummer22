using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerBehavior : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] bool fireOnDestroy = false;
    [SerializeField] bool startTimerImmediate = false;
    [SerializeField] bool isRepeating = false;
    [SerializeField] UnityEvent onTimerEnd = new UnityEvent();
    
    bool isRunning = false;
    public bool IsRunning => isRunning;
    // Start is called before the first frame update
    void Start()
    {
        if (startTimerImmediate)
            StartTimer();
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
        isRunning = true;
        yield return new WaitForSeconds(duration);
        isRunning = false;
        OnTimerEnd();
    }

    public void StartTimer()
    {
        if (isRunning)
        {
            Debug.LogWarning($"Ending the previous timer without triggering the end and starting the new one");
            StopAllCoroutines();
        }
        StartCoroutine(TimerCO(duration));
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
        if (isRepeating)
            StartTimer();
    }
}
