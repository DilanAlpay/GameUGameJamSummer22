using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventCall : MonoBehaviour
{
    public void CallGameEvent(GameEvent e)
    {
        e.Call();
    }
}
