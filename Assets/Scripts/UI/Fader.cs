using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private Animator _animator;

    #region Animation Hashes
    private int _hashMoveToArea;
    private int _hashFadeOut;
    #endregion

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _animator = GetComponent<Animator>();

        _hashMoveToArea= Animator.StringToHash("MoveToArea");
        _hashFadeOut = Animator.StringToHash("FadeOut");
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
        _animator.Play(_hashMoveToArea);
    }

    public void FadeOut()
    {
        _animator.Play(_hashFadeOut);
    }
}
