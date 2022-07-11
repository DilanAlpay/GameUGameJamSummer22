using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    IAction currentAction = null;
    IAction prevAction = null;
    public void StartAction(IAction action)
    {
        if (currentAction == action) return;
        if(currentAction != null)
        {
            prevAction = currentAction;
            currentAction.CancelAction();
        }
        currentAction = action;
    }

    public void CancelCurrentAction()
    {
        StartAction(null);
    }
}
