using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] private BarData barData;
        
    private BarMovement[] bars;
    private bool active;
    private int currentBarIndex;

    public event Action OnBarsSet;

    private void Awake()
    {
        bars = GetComponentsInChildren<BarMovement>();
        barData.ResetSizeAndSpeed();
    }

    [Button]
    public void StartBarsMoving()
    {
        active = true;
        currentBarIndex = 0;
        bars[0].StartMoving(barData);
    }

    private void StartNextBarMoving()
    {
        currentBarIndex++;

        if(currentBarIndex >= bars.Length)
        {
            BarsSet();
            return;
        }

        bars[currentBarIndex].StartMoving(barData);
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
        barData.DecrementCurrentAverageSize();
        barData.DecrementCurrentAverageSpeed();

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
