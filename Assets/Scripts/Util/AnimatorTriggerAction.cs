using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTriggerAction : MonoBehaviour
{
    public string triggerName;
    [SerializeField] Animator anim;

    public void SetAnimatorTrigger()
    {
        anim.SetTrigger(triggerName);
    }
}
