using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public GameEvent fadeOutComplete, fadeInComplete;

    private Animator _animator;

    #region Animation Hashes
    private int _hashTransition;
    #endregion

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _animator = GetComponent<Animator>();

        _hashTransition = Animator.StringToHash("Transition");
    }

    /// <summary>
    /// Fades in and fades out
    /// </summary>
    public void Transition()
    {
        _animator.Play(_hashTransition);
    }

    public void FadeOutComplete()
    {
        fadeOutComplete.Call();
    }

    public void FadeInComplete()
    {
        fadeInComplete.Call();
    }

}
