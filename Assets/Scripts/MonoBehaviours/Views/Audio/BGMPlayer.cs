using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    private BGMConfig model;

    AudioSource audioSource;

    private BGM currentBGM = BGM.None;

    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateVolume(PlayerPrefs.GetFloat(PlayerPrefKeys.fBGMVolume, 1f));
    }

    public void UpdateVolume(float newValue)
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.fBGMVolume, newValue);
        model.Volume = newValue;
        audioSource.volume = newValue;
    }

    public void Play(BGM type)
    {
        if(currentBGM != type)
        {
            Stop();
            currentBGM = type;

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
}
