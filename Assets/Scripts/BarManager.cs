using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour {

    private BarMovement[] bars;
    private int currentBarIndex;

    private void Awake()
    {
        bars = GetComponentsInChildren<BarMovement>();
    }

    [Button]
    private void StartBarsMoving()
    {
        currentBarIndex = 0;
        bars[0].StartMoving();
    }

    private void StartNextBarMoving()
    {
        currentBarIndex++;

        if(currentBarIndex >= bars.Length)
        {
            Debug.Log("Finished All Bars.");
            return;
        }

        bars[currentBarIndex].StartMoving();
    }

    public void StopCurrentBarAndMoveToNextBar()
    {
        bars[currentBarIndex].StopMoving();
        StartNextBarMoving();
    }
}
