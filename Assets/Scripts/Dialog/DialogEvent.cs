using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : MonoBehaviour
{
    public GameEvent e;
    public void Call(Dialog d)
    {
        e.Call(d);
    }
}
