using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;

    /// <summary>
    /// Floating sign above this object prompting the player to press the interact button
    /// </summary>
    [SerializeField]
    private GameObject _alert;

    public void SetAlert(bool a)
    {
        _alert.SetActive(a);
    }


    public void Interact()
    {
        SetAlert(false);
        onInteract.Invoke();
    }
}
