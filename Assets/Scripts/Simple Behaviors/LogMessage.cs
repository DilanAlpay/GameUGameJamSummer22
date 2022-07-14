using GameJam.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMessage : MonoBehaviour
{
    [SerializeField] string message;
    [SerializeField] LoggerTag logTag = LoggerTag.General;

    public void Log()
    {
        Debug.Log("OW!");
    }
}
