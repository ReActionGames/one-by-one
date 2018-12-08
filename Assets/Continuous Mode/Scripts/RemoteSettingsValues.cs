using System;
using UnityEngine;

public static class RemoteSettingsValues
{
    public static event Action ValuesUpdated;

    public static float BackgroundSpeed
    {
        get
        {
            InitializeValues();
            return backgroundSpeed;
        }
    }

    private static float backgroundSpeed = 0.5f;

    private static bool initialized = false;

    private static void InitializeValues()
    {
        if (initialized)
            return;

        backgroundSpeed = RemoteSettings.GetFloat("background-speed", 0.5f);

        RemoteSettings.Completed += HandleRemoteSettingsCompleted;
    }

    //static RemoteSettingsValues()
    //{
    //    BackgroundSpeed = RemoteSettings.GetFloat("background-speed", 0.5f);

    //    RemoteSettings.Completed += HandleRemoteSettingsCompleted;
    //}

    private static void HandleRemoteSettingsCompleted(bool wasUpdatedFromServer, bool settingsChanged, int serverResponse)
    {
        backgroundSpeed = RemoteSettings.GetFloat("background-speed", 0.5f);

        ValuesUpdated?.Invoke();
    }
}