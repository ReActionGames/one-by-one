using DoozyUI;
using TMPro;
using UnityEngine;

namespace Continuous
{
    public class UITutorial : MonoBehaviour
    {
        private enum State
        {
            Disabled,
            Active,
            TransitioningToNextMessage
        }

        [SerializeField] private UIElement textElement;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string[] messages;

        private int currentMessage;
        private int barNumber;
        private State state;

        private void Start()
        {
            DisableText();
        }

        private void OnEnable()
        {
            GameManager.GameStartOrRestart += OnGameStartOrRestart;
            GameManager.GameEnd += OnGameEnd;
            PathController.BarPlaced += OnBarPlaced;
            textElement.OnInAnimationsFinish.AddListener(OnMessageShow);
            textElement.OnOutAnimationsFinish.AddListener(OnMessageHide);
        }

        private void OnDisable()
        {
            GameManager.GameStartOrRestart -= OnGameStartOrRestart;
            GameManager.GameEnd -= OnGameEnd;
            PathController.BarPlaced -= OnBarPlaced;
            textElement.OnInAnimationsFinish.RemoveListener(OnMessageShow);
            textElement.OnOutAnimationsFinish.RemoveListener(OnMessageHide);
        }

        private void OnGameStartOrRestart()
        {
            barNumber = 0;
            currentMessage = 0;
            SetMessage(currentMessage);
            textElement.Show(false);
            state = State.TransitioningToNextMessage;
        }

        private void OnGameEnd()
        {
            if (state == State.Disabled) return;
            DisableText();
        }

        private void OnMessageShow()
        {
            state = State.Active;
        }

        private void OnMessageHide()
        {
            if (state == State.TransitioningToNextMessage)
            {
                currentMessage++;
                SetMessage(currentMessage);
                textElement.Show(false);
            }
        }

        private void OnBarPlaced()
        {
            barNumber++;
        }

        private void Update()
        {
            if (state == State.Active)
            {
                if (barNumber > currentMessage)
                {
                    ShowNextMessage();
                }
            }
        }

        private void ShowNextMessage()
        {
            if (currentMessage + 1 >= messages.Length)
            {
                DisableText();
                return;
            }

            textElement.Hide(false);
            state = State.TransitioningToNextMessage;
        }

        private void SetMessage(int index)
        {
            text.SetText(messages[index]);
        }

        private void DisableText()
        {
            state = State.Disabled;
            textElement.Hide(false);
        }
    }
}