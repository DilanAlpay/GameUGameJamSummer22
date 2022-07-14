using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBoolAction : MonoBehaviour
{
    public string boolName;
    [SerializeField] Animator anim;

    public void SetAnimatorBool(bool isDigging)
    {
        anim.SetBool(boolName, isDigging);
    }
}
