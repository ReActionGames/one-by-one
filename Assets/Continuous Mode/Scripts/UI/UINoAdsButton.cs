using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Continuous
{
    public class UINoAdsButton : MonoBehaviour
    {
        public void NoAdsPurchaseComplete(Product product)
        {
            FindObjectOfType<IntervalAds>().NoAdsPurchaseComplete(product);
        }

        public void UpdateActivation(bool noAds)
        {
            StartCoroutine(SetActiveAfterDelay(!noAds));
        }

        private IEnumerator SetActiveAfterDelay(bool active)
        {
            yield return null;
            gameObject.SetActive(active);
        }
    }
}