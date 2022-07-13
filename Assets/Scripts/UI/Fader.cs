using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public GameEvent fadeOutComplete, fadeInComplete;

    private Animator _animator;

    #region Animation Hashes
    private int _hasMoveToArea;
    #endregion

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _animator = GetComponent<Animator>();

        _hasMoveToArea= Animator.StringToHash("MoveToArea");
    }

    public void CallGameEvent(GameEvent e)
    {
        e.Call();
    }

    /// <summary>
    /// Fades in and fades out
    /// </summary>
    public void MoveToArea()
    {
        _animator.Play(_hasMoveToArea);
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
