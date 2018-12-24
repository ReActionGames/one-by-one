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

        private bool active = false;

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
        }

        private void OnDisable()
        {
            PowerupPickup.PowerupCollected -= Activate;
        }

        private void Activate(PowerupType powerupType)
        {
            if (powerupType != PowerupType.Shield)
                return;
            if(active == true)
            {
                autoKillTween.Restart();
                return;
            }

            state = State.Active;

            shieldVisibility.Show();
            shieldTween.Restart();

            autoKillTween.Restart();
            active = true;
        }

        private void Deactivate()
        {
            state = State.Inactive;

            shieldVisibility.Hide(() => shieldTween.Pause());

            autoKillTween.Pause();
            active = false;
        }

        private void DeactivateInstantly()
        {
            state = State.Inactive;

            shieldVisibility.HideInstantly();
            shieldTween.Pause();

            autoKillTween.Pause();
            active = false;
        }
    }
}