using DoozyUI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {

    [SerializeField] private AudioClip soundEffect;

    private Soundy soundy;

    private void Awake()
    {
        soundy = FindObjectOfType<Soundy>();
    }

    [Button]
    public void PlaySoundEffect()
    {
        soundy.PlaySound(soundEffect);
    }
}
