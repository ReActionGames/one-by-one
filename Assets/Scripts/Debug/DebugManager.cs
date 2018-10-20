using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviourSingleton<DebugManager> {

    [SerializeField] private bool showDebugLogs, showGameTime;

    private void _Log(string message, object sender)
    {
        if (!showDebugLogs)
            return;
        if(sender != null)
        {
            message = $"[{sender.GetType()}] {message}";
        }
        if (showGameTime)
        {
            message = $"[{Time.time}] {message}";
        }

        Debug.Log(message);
    }

    public static void Log(string message)
    {
        Log(message, null);
    }

    public static void Log(string message, object sender)
    {
        Instance._Log(message, sender);
    }

}
