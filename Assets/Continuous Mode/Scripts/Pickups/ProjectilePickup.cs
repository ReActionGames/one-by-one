using UnityEngine;

namespace Continuous
{
    public class ProjectilePickup : MonoBehaviour, ICollectible
    {
        [SerializeField] private string shieldProjectileLayerName = "ShieldProjectile";

        private Projectile projectile;

        private void Awake()
        {
            projectile = GetComponent<Projectile>();
        }

        public void Collect()
        {
            gameObject.layer = LayerMask.NameToLayer(shieldProjectileLayerName);
            FindObjectOfType<ProjectileManager>().AddProjectile(projectile);
        }
    }
}