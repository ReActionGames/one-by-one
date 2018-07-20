using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private Transform white;
    [SerializeField] private Transform start, end;

    private float direction = 1;
    private float positionPercentage = 0;
    private bool isMoving = true;

    private void Update()
    {
        if (isMoving == false)
            return;

        Move();
    }

    private void Move()
    {
        positionPercentage += Time.deltaTime * direction;

        white.position = Vector3.Lerp(start.position, end.position, positionPercentage);

        if (positionPercentage >= 1 && direction == 1)
        {
            direction = -1;
        }
        else if (positionPercentage <= 0 && direction == -1)
        {
            direction = 1;
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
