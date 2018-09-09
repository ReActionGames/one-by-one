using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampFollow : MonoBehaviour {

    //[SerializeField] private bool followParent = false;
    [SerializeField/*, HideIf("followParent", false)*/] private Transform followTarget;
    [SerializeField] private float dampning;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool followBackwards = false;

    private Vector3 targetLastPosition;
    private bool targetHasMovedFlag;
    private Vector3 velocity;

    private void Start()
    {
        //if (followParent)
        //{
        //    transform.localPosition = Vector3.zero;
        //    followTarget = transform.parent;
        //}
        //if (!followParent)
            transform.position = followTarget.position;
        targetHasMovedFlag = false;
        targetLastPosition = followTarget.position;
    }

    private void Update()
    {
        //CheckIfTargetHasMoved();
        //if (targetHasMovedFlag)
        //{
            UpdatePosition();
        //    return;
        //}
        targetLastPosition = followTarget.position;

    }

    private void UpdatePosition()
    {
        Vector3 targetPosition = followTarget.position;
        Quaternion targetRotation = followTarget.rotation;

        if (!followBackwards && HasTargetMovedBackwards())
        {
            transform.position = targetPosition;
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampning);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool HasTargetMovedBackwards()
    {
        Vector3 direction = followTarget.position - targetLastPosition;
        if (direction.y < 0)
            return true;
        return false;
    }

    private bool CheckIfTargetHasMoved()
    {
        targetHasMovedFlag = followTarget.position == targetLastPosition;
        return targetHasMovedFlag;
    }
}
