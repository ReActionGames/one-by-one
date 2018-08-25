using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    private static float startingSize = 9.0f;
    private static float MinStartingSize = 4.0f;

    private BarMovement[] bars;
    private bool active;
    private int currentBarIndex;

    public event Action OnBarsSet;

    private void Awake()
    {
        bars = GetComponentsInChildren<BarMovement>();
    }

    [Button]
    public void StartBarsMoving()
    {
        active = true;
        currentBarIndex = 0;
        bars[0].StartMoving(startingSize);
    }

    private void StartNextBarMoving()
    {
        currentBarIndex++;

        if(currentBarIndex >= bars.Length)
        {
            BarsSet();
            return;
        }

        bars[currentBarIndex].StartMoving(startingSize - (currentBarIndex * 0.1f));
    }

    public void StopCurrentBarAndMoveToNextBar()
    {
        if (!active)
            return;
        bars[currentBarIndex].StopMoving();
        StartNextBarMoving();
    }

    private void BarsSet()
    {
        active = false;
        //startingSize--;
        startingSize = Mathf.Clamp(startingSize - 1, MinStartingSize, startingSize);
        OnBarsSet?.Invoke();
    }

    public void Reset()
    {
        foreach (var bar in bars)
        {
            bar.Reset();
        }
    }
}
