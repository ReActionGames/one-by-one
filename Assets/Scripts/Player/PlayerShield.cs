using DG.Tweening;
using ReActionGames.Events;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private ShieldProperties properties;
    [SerializeField] private GameObject shieldIndicator;
    [SerializeField] private SpriteRenderer movingSprite;

    private Tween loopTween;
    private Tween activateTween;
    private bool activated;

    private void Start()
    {
        Deactivate();
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventNames.PowerUpCollected, OnPowerUpCollected);
        EventManager.StartListening(EventNames.LevelStarted, ReevaluateLifeSpan);

        GameManager.Instance.OnEnterState += OnEnterState;
        GameManager.Instance.OnExitState += OnExitState;
    }

    private void OnEnterState(GameManager.GameState state)
    {
        if (state != GameManager.GameState.Active)
            return;

        Deactivate();
    }

    private void OnExitState(GameManager.GameState state)
    {
        if (state != GameManager.GameState.Active)
            return;

        Deactivate();
    }

    private void ReevaluateLifeSpan(Message message)
    {
        if (!activated)
            return;
        properties.DecrementLifeSpan();
        if (properties.CurrentLifeSpan < 0)
        {
            Deactivate();
        }
    }

    private void OnPowerUpCollected(Message message)
    {
        ShieldPowerUp powerUp = (ShieldPowerUp)message.Data;
        if (powerUp == null)
            return;
        Activate();
    }

    [Button]
    public void Activate()
    {
        shieldIndicator.SetActive(true);
        SetAnimationActive(true);
        properties.ResetLifeSpan();
        activated = true;
    }

    private void SetAnimationActive(bool active)
    {
        if (loopTween == null)
            InitiateLoopTween();
        if (activateTween == null)
            InitiateActivateTween();
        if (active)
        {
            loopTween.Restart();
            activateTween.Restart();
        }
        else
        {
            loopTween.Pause();
            activateTween.PlayBackwards();
        }
    }

    private void InitiateActivateTween()
    {
        Tweener scaleTween = shieldIndicator.transform.DOScale(1, properties.ActivateDuration)
            .SetEase(properties.ActivateScaleEase);

        activateTween = scaleTween;
        activateTween.SetAutoKill(false);

    }

    private void InitiateLoopTween()
    {
        Tweener scaleTween = movingSprite.transform.DOScale(0, properties.Duration)
            .From()
            .SetEase(properties.ScaleEase);
        Tweener fadeTween = movingSprite.DOFade(0, properties.Duration)
            .SetEase(properties.FadeEase);

        var loopTweenSequence = DOTween.Sequence();
        loopTweenSequence.Append(scaleTween)
            .Join(fadeTween)
            .AppendInterval(properties.PulseDelay)
            .SetLoops(-1);

        loopTween = loopTweenSequence;
        loopTween.SetAutoKill(false);
    }

    [Button]
    public void Deactivate()
    {
        SetAnimationActive(false);
        activated = false;
    }

    public bool IsActive()
    {
        return activated;
    }
}