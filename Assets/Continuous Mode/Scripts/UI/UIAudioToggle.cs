using DoozyUI;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Continuous
{
    public class UIAudioToggle : MonoBehaviour
    {
        private const string playerPrefsKey = "audio";

        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Image iconImage;
        [SerializeField] private Sprite onImage, offImage;

        private Toggle toggle;
        private UIElement parentElement;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            parentElement = GetComponentInParent<UIElement>();
        }

        private void Start()
        {
            LoadAndApplyAudioState();

            parentElement.OnInAnimationsStart.AddListener(LoadAndApplyAudioState);

            toggle.onValueChanged.AddListener(ToggleAudio);
            toggle.onValueChanged.AddListener(ToggleIcon);
        }

        private void OnValidate()
        {
            toggle = GetComponent<Toggle>();
            LoadAndApplyAudioState();
        }

        private void LoadAndApplyAudioState()
        {
            bool savedAudioState = LoadAudioState();
            toggle.isOn = savedAudioState;
            ToggleAudio(savedAudioState);
            ToggleIcon(savedAudioState);
        }

        private void ToggleIcon(bool on)
        {
            if (on)
            {
                iconImage.sprite = onImage;
            }
            else
            {
                iconImage.sprite = offImage;
            }
        }

        private void ToggleAudio(bool on)
        {
            if (on)
            {
                audioMixer.SetFloat("Volume", 0);
            }
            else
            {
                audioMixer.SetFloat("Volume", -80);
            }

            SaveAudioState(on);
        }

        private bool LoadAudioState()
        {
            int on = PlayerPrefs.GetInt(playerPrefsKey, 1);
            return on == 1;
        }

        private void SaveAudioState(bool on)
        {
            PlayerPrefs.SetInt(playerPrefsKey, on == true ? 1 : 0);
        }
    }
}