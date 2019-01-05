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
    public static int MaxShields
    {
        get
        {
            InitializeValues();
            return maxShields;
        }
    }
    public static float ShieldProbability
    {
        get
        {
            InitializeValues();
            return shieldProbability;
        }
    }

    private static float backgroundSpeed = 0.5f;
    private static int maxShields = 3;
    private static float shieldProbability = 0.1f;

    private static bool initialized = false;

    private static void InitializeValues()
    {
        if (initialized)
            return;

        backgroundSpeed = RemoteSettings.GetFloat("background-speed", 0.5f);
        maxShields = RemoteSettings.GetInt("max-shields", 3);
        shieldProbability = RemoteSettings.GetFloat("shield-probability", 0.1f);

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
        maxShields = RemoteSettings.GetInt("max-shields", 3);
        shieldProbability = RemoteSettings.GetFloat("shield-probability", 0.1f);

        ValuesUpdated?.Invoke();
    }
}