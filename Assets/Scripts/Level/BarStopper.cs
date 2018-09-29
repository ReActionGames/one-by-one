using UnityEngine;

public class BarStopper : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopBar();
        }
    }

    private void StopBar()
    {
        GetComponent<Level>().StopCurrentBarAndMoveToNextBar();
    }
}