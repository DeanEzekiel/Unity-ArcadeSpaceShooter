using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    private BGMConfig model;
    [SerializeField]
    AudioSource audioSource;

    private BGM currentBGM = BGM.None;

    public void Init(float value)
    {
        audioSource = GetComponent<AudioSource>();

        UpdateVolume(value);
    }

    public void UpdateVolume(float newValue)
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.fBGMVolume, newValue);
        model.Volume = newValue;
        audioSource.volume = newValue;
    }

    public void Play(BGM type, bool switchClipRegardless)
    {
        if (switchClipRegardless || (currentBGM != type))
        {
            Stop();
            currentBGM = type;
            SetAudioClip(type);
            audioSource.Play();
        }
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            currentBGM = BGM.None;
        }
    }

    private void SetAudioClip(BGM type)
    {
        int random = 0;

        switch (type)
        {
            case BGM.MainMenu:
                random = Random.Range(0, model.MainMenu.Count);
                audioSource.clip = model.MainMenu[random];
                break;
            case BGM.Gameplay:
                random = Random.Range(0, model.Gameplay.Count);
                audioSource.clip = model.Gameplay[random];
                break;
            case BGM.Shop:
                random = Random.Range(0, model.Shop.Count);
                audioSource.clip = model.Shop[random];
                break;
        }
    }
}
