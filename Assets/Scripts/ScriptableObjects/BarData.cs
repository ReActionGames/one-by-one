using UnityEngine;

[CreateAssetMenu(fileName = "BarData", menuName = "Scriptable Objects/Bar Data")]
public class BarData : ScriptableObject
{
    [SerializeField] private float startingSize;
    [SerializeField] private float currentAverageSize;
    [SerializeField] private float sizeDistribution;
    [SerializeField] private float sizeDecremationInterval;
    [SerializeField] private float minSize;
    [Space]
    [SerializeField] private float startingSpeed;
    [SerializeField] private float currentAverageSpeed;
    [SerializeField] private float speedDistribution;
    [SerializeField] private float speedDecremationInterval;
    [SerializeField] private float minSpeed;
    
    public float CurrentAverageSize
    {
        get
        {
            return currentAverageSize;
        }

    }

    public float CurrentAverageSpeed
    {
        get
        {
            return currentAverageSpeed;
        }

    }

    
    public float GetPsuedoRandomSize()
    {
        //float offset = Random.Range(-sizeDistribution, sizeDistribution);
        //float rawSize = currentSize + offset;
        float rawSize = RandomExtensions.RandomGaussian(currentAverageSize, sizeDistribution);
        return Mathf.Clamp(rawSize, minSize, int.MaxValue);
    }

    public float GetPsuedoRandomSpeed()
    {
        float rawSpeed = RandomExtensions.RandomGaussian(currentAverageSpeed, speedDistribution);
        return Mathf.Clamp(rawSpeed, minSpeed, int.MaxValue);
    }

    public void DecrementCurrentAverageSize()
    {
        currentAverageSize -= sizeDecremationInterval;
        currentAverageSize = Mathf.Clamp(currentAverageSize, minSize, startingSize);
    }

    public void DecrementCurrentAverageSpeed()
    {
        currentAverageSpeed -= speedDecremationInterval;
        currentAverageSpeed = Mathf.Clamp(currentAverageSpeed, minSpeed, startingSpeed);
    }

    public void ResetSize()
    {
        currentAverageSize = startingSize;
    }

    public void ResetSpeed()
    {
        currentAverageSpeed = startingSpeed;
    }

    public void ResetSizeAndSpeed()
    {
        ResetSize();
        ResetSpeed();
    }
}
