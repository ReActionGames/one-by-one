using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField] private List<Projectile> projectiles;
        [SerializeField] private float rotateDuration;
        //[SerializeField] private Projectile prefab;

        private Tween rotationTween;

        private void Start()
        {
            rotationTween = transform.DORotate(new Vector3(0, 0, 360), rotateDuration, RotateMode.FastBeyond360)
                .SetAutoKill(false)
                .SetRecyclable(true)
                .SetLoops(-1)
                .SetEase(Ease.Linear)
                .Pause();
        }

        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
            projectile.transform.parent = transform;


            RearrangeProjectiles();
        }

        /// <summary>
        /// Shoots a projectile at the hit point. Returns true if a projectile was successfully shot. Returns false if no projectiles are available.
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        public bool ShootProjectile(RaycastHit2D hit)
        {
            if (projectiles.Count <= 0)
                return false;
            var projectile = projectiles.PickRandom();
            projectiles.Remove(projectile);

            projectile.Fire(hit.point, hit.collider);

            RearrangeProjectiles();
            return true;
        }

        //[Button]
        //private void DebugAddProjectile()
        //{
        //    var projectile = Instantiate(prefab);

        //    AddProjectile(projectile);
        //}

        //[Button]
        //private void DebugRemoveProjectile()
        //{
        //    if (projectiles.Count <= 0)
        //        return;

        //    Projectile projectile = projectiles[projectiles.Count - 1];
        //    projectiles.Remove(projectile);
        //    Destroy(projectile.gameObject);
        //    RearrangeProjectiles();
        //}

        [Button]
        private void RearrangeProjectiles()
        {
            if (projectiles.Count <= 0)
            {
                rotationTween.Pause();
                return;
            }
            
            if (rotationTween.IsPlaying() == false)
            {
                rotationTween.Play();
            }

            float interval = 360 / projectiles.Count;

            for (int i = 0; i < projectiles.Count; i++)
            {
                float angle = i * interval;
                Vector2 localPosition = VectorExtensions.PolarToCartesian(angle);
                projectiles[i].Reposition(localPosition);
            }
        }
    }
}