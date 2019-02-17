using DG.Tweening;
using UnityEngine;

namespace Continuous
{
    public class ShieldPowerupComponent : PowerupComponent
    {
        [SerializeField] private float lifeSpan = 5;
        [SerializeField] private SpriteVisibility shieldVisibility;
        [SerializeField] private Transform shieldBall;
        [SerializeField] private float rotateDuration = 1.5f;
        [SerializeField] private ShieldProjectile projectile;

        public bool Active => state == State.Active;

        private Tween autoKillTween;
        private Tween shieldTween;

        private void Start()
        {
            shieldVisibility.HideInstantly();
            autoKillTween = DOVirtual.DelayedCall(lifeSpan, Deactivate)
                .SetAutoKill(false)
                .SetRecyclable(true)
                .Pause();
            shieldTween = shieldBall.DORotate(new Vector3(0, 0, 360), rotateDuration, RotateMode.FastBeyond360)
                .SetAutoKill(false)
                .SetRecyclable(true)
                .SetLoops(-1)
                .SetEase(Ease.Linear)
                .Pause();
        }

        private void OnEnable()
        {
            PowerupPickup.PowerupCollected += Activate;
            ShieldProjectile.HitBar += DeactivateInstantly;

            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
        }

        private void OnDisable()
        {
            PowerupPickup.PowerupCollected -= Activate;
            ShieldProjectile.HitBar -= DeactivateInstantly;

            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
        }

        private void OnGameStartOrRestart()
        {
            DeactivateInstantly();
        }

        private void Activate(PickupType powerupType)
        {
            if (powerupType != PickupType.Shield)
                return;
            if (state == State.Active)
            {
                autoKillTween.Restart();
                return;
            }

            state = State.Active;

            shieldVisibility.Show();
            shieldTween.Restart();

            autoKillTween.Restart();
        }

        public void Use(RaycastHit2D hit)
        {
            projectile.Fire(hit.point, hit.collider);
        }

        private void Deactivate()
        {
            state = State.Inactive;

            shieldVisibility.Hide(() => shieldTween.Pause());

            autoKillTween.Pause();
        }

        private void DeactivateInstantly()
        {
            state = State.Inactive;

            shieldVisibility.HideInstantly();
            shieldTween.Pause();

            autoKillTween.Pause();
        }
    }
}