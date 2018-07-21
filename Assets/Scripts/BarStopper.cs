using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarStopper : MonoBehaviour {
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopBar();
        }
    }

    private void StopBar()
    {
        //GetComponent<BarMovement>().StopMoving();
        GetComponent<BarManager>().StopCurrentBarAndMoveToNextBar();
    }
}
