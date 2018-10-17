using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreMarker : MonoBehaviour {

    [SerializeField] private Transform bar, left, right;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        ResizeMarkerToFitCamera();
        SetMarkerActive(false);
    }

    [Button]
    private void ResizeMarkerToFitCamera()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        bar.localScale = new Vector3(width, bar.localScale.y);

        float edgePosition = (width / 2) - right.GetComponent<SpriteRenderer>().bounds.extents.x;

        right.localPosition = new Vector3(edgePosition, right.localPosition.y);

        left.localPosition = new Vector3(-edgePosition, left
.localPosition.y);
    }

    private void SetMarkerActive(bool active)
    {
        bar.gameObject.SetActive(active);
        right.gameObject.SetActive(active);
        left.gameObject.SetActive(active);
    }
}
