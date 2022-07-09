using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Util {
    public class LoggerManager : MonoBehaviour
    {
        Dictionary<LoggerTag, EZELogger> loggers;

        #region Singleton
        public static LoggerManager i;
        bool isInitialized;

        private void Awake()
        {
            if(i == null)
            {
                i = this;
                loggers = new Dictionary<LoggerTag, EZELogger>();
                Initialize();
            }
        }

  
        #endregion

        public void Log(string message, LoggerTag tag = LoggerTag.General)
        {
            if (!isInitialized) Initialize();

            if (loggers.ContainsKey(tag))
            {
                loggers[tag].Log(message);
            }
            else
            {
                loggers[LoggerTag.General].Log(message);
            }
        }



        private void Initialize()
        {
            if (isInitialized) return;

            EZELogger[] logArray = GetComponentsInChildren<EZELogger>();
            foreach (EZELogger logger in logArray)
            {
                if (!loggers.ContainsKey(logger.loggerTag))
                {
                    loggers.Add(logger.loggerTag, logger);
                }
                else
                {
                    Debug.LogWarning($"Loggers already has a logger tagged {logger.loggerTag}");
                }
            }
            isInitialized = true;


        }


    }
}