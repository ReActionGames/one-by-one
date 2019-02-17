using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class ObjectVisibility : MonoBehaviour
    {
        [SerializeField] private GameObject[] visibleObjects;

        public void Show()
        {
            foreach (GameObject gameObject in visibleObjects)
            {
                gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            foreach (GameObject gameObject in visibleObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }
}