using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFont : MonoBehaviour {

    [SerializeField] private RectTransform[] titles;

    private void Start()
    {
        foreach (var title in titles)
        {
            title.gameObject.SetActive(false);
        }
        titles[0].gameObject.SetActive(true);
    }

    public void ActivateTitle(float i)
    {
        var index = (int)i;
        if (0 > index || index >= titles.Length)
            return;

        foreach (var title in titles)
        {
            title.gameObject.SetActive(false);
        }
        titles[index].gameObject.SetActive(true);
    }
}
