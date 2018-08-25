using UnityEngine;

[CreateAssetMenu(fileName = "BarData", menuName = "Scriptable Objects/Bar Data")]
public class BarData : ScriptableObject
{
    [SerializeField] private float startingSize;
    [SerializeField] private float currentSize;
    [SerializeField] private float minSize;
    [SerializeField] private float startingSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float minSpeed;

    public float StartingSize
    {
        get
        {
            return startingSize;
        }

    }

    public float CurrentSize
    {
        get
        {
            return currentSize;
        }

    }

    public float MinSize
    {
        get
        {
            return minSize;
        }

    }

    public float StartingSpeed
    {
        get
        {
            return startingSpeed;
        }
    }

    public float CurrentSpeed
    {
        get
        {
            return currentSpeed;
        }

    }

    public float MinSpeed
    {
        get
        {
            return minSpeed;
        }
    }
}
