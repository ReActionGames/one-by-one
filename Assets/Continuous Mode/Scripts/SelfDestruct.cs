using UnityEngine;

namespace Continuous
{
    public class SelfDestruct : MonoBehaviour
    {
        public float delay = 5;

        private void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}