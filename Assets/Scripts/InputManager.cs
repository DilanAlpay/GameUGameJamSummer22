using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Managers the InputEvents that are used by external scripts
/// Assigns them all a single instance of the InputSystem
/// and initializes them
/// </summary>
public class InputManager : MonoBehaviour
{
    public List<InputObj> inputs;
    private PlayerInput _controls;
    private void OnEnable()
    {
        _controls = new PlayerInput();
        _controls.CharacterControls.Enable();
        foreach (InputObj i in inputs)
        {
            i.Controls = _controls;
            i.SetAction();
        }
    }

    private void OnDisable()
    {
        _controls.CharacterControls.Disable();
        foreach (InputObj i in inputs)
        {
            i.Clear();
        }
    }

    public void SetPause(bool b)
    {
        if (b)
        {
            _controls.CharacterControls.Enable();
        }
        else
        {
            _controls.CharacterControls.Disable();
        }
    }
}
