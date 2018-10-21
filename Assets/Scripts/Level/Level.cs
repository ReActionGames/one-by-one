using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ShieldPowerUp shieldPowerUp;

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

    private void OnEnable()
    {
        GameManager.Instance.OnExitState += OnExitState;
    }

    private void OnExitState(GameManager.GameState state)
    {
        if (state == GameManager.GameState.End)
        {
            ResetObject();
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnExitState -= OnExitState;
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
        bars[0].AddPowerup(shieldPowerUp);
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
        foreach (BarMovement bar in bars)
        {
            bar.Hide(instant);
        }
    }
}