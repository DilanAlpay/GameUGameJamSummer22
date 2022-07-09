using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Util
{
    public class EZELogger : MonoBehaviour
    {
        public string loggerName;
        public LoggerTag loggerTag;
        public Color textColor;
        public bool isDiplayingMessages;

        public void Log(string message)
        {
            if (isDiplayingMessages)
            {
                string logmessage = ColorizeText(loggerName + ": ", textColor) + message;
                Debug.Log(logmessage);
            }
        }

        public static string ColorizeText(string message, Color color)
        {    
            return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + message + "</color>";
        }

    }
}