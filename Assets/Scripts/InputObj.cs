using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Object")]
public class InputObj : ScriptableObject
{
    private InputAction _action;
    private PlayerInput _controls;

    public InputAction Action { get { return _action; } }
    public PlayerInput Controls { get { return _controls; } set { _controls = value; } }

    /// <summary>
    /// Generates the InputAction associated with this object using its file name
    /// </summary>
    public void SetAction()
    {
        _action = _controls.FindAction(name);
    }

    public void Clear()
    {
        _controls = null;
        _action = null;
    }
}

