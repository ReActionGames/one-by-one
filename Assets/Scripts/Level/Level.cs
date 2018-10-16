using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Level : MonoBehaviour, IResetable
{
    private BarData barData;

    private BarMovement[] bars;
    private FirstBar firstBar;
    private bool active;
    private int currentBarIndex;

    public event Action OnBarsSet;

    private void Awake()
    {
        bars = GetComponentsInChildren<BarMovement>();
        firstBar = GetComponentInChildren<FirstBar>();
        firstBar.SetData(barData);
        HideBars(true);
    }

    public void PrepareLevel()
    {
        HideBars(true);
        firstBar.SetUp();
    }

    public void StartBarsMoving(BarData barData)
    {
        active = true;
        currentBarIndex = 0;
        this.barData = barData;
        firstBar.SetData(barData);
        bars[0].StartMoving(barData);
    }

    private void StartNextBarMoving()
    {
        currentBarIndex++;

        if (currentBarIndex >= bars.Length)
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
        barData.DecrementCurrentAverageSize();
        barData.DecrementCurrentAverageSpeed();

        OnBarsSet?.Invoke();
    }

    public void ResetObject()
    {
        HideBars(false);
    }

    public void HideBars(bool instant = false)
    {
        foreach (var bar in bars)
        {
            bar.Hide(instant);
        }
    }
}