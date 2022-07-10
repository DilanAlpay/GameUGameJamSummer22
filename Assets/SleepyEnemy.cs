using GameJam.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SleepyEnemy : EnemyBase
{
    [SerializeField] Image image;
    [SerializeField] Sprite sleepingSprite;
    [SerializeField] Sprite awakeSprite;
    bool isAwake;
    bool isChasing;
    public UnityEvent onStart;
    private void Start()
    {
        onStart?.Invoke();
    }

    public void WakeUp()
    {
        isAwake = true;
        image.sprite = awakeSprite;
        LoggerManager.i.Log("SLEEPY wakes up!", LoggerTag.Enemy);
        LoggerManager.i.Log("It attacked in a grumpy rage!", LoggerTag.Enemy);
        mover.StartMoving();
    }

    public void GoToSleep()
    {
        image.sprite = sleepingSprite;
        LoggerManager.i.Log("Sleepy enemy sleeps!", LoggerTag.Enemy);
        isAwake = false;
        mover.StopMoving();
    }

    IEnumerator Chase()
    {
        while (isChasing)
        {
            if (isAwake)
            {
                mover.MoveToTarget(target);
                if (Vector3.Distance(transform.position, target.position) <= fighter.AttackRange)
                {
                    mover.CancelAction();
                    yield return fighter.AttackCO(target);
                    mover.ResetMoving();

                }
                else
                    yield return delay;
            }
            else
            {
                yield return delay;
            }
        }
    }
    public void StartChasing()
    {
        isChasing = true;
        StartCoroutine(Chase());
        mover.StartMoving();
    }

    public void StopChasing()
    {
        isChasing = false;
        StopAllCoroutines();
    }

}
