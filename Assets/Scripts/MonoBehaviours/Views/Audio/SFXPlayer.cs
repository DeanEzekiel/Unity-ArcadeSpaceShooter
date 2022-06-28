using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    private SFXConfig model;

    AudioSource audioSource;

    public void Init(float value)
    {
        audioSource = GetComponent<AudioSource>();

        UpdateVolume(value);
    }

    public void UpdateVolume(float newValue)
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.fSFXVolume, newValue);
        model.Volume = newValue;
        audioSource.volume = newValue;
    }

    public void Play(SFX type)
    {
        int random = 0;
        switch (type)
        {
            case SFX.Player_Shoot:
                random = Random.Range(0, model.Player_Shoot.Count);
                audioSource.PlayOneShot(model.Player_Shoot[random]);
                break;
            case SFX.Enemy_Shoot:
                random = Random.Range(0, model.Enemy_Shoot.Count);
                audioSource.PlayOneShot(model.Enemy_Shoot[random]);
                break;
            case SFX.Hit_Crash:
                random = Random.Range(0, model.Hit_Crash.Count);
                audioSource.PlayOneShot(model.Hit_Crash[random]);
                break;
            case SFX.Player_Shield_Activate:
                audioSource.PlayOneShot(model.Player_Shield_Activate);
                break;
            case SFX.Player_Shield_Deactivate:
                audioSource.PlayOneShot(model.Player_Shield_Deactivate);
                break;
            case SFX.Player_Rocket_Launch:
                audioSource.PlayOneShot(model.Player_Rocket_Launch);
                break;
            case SFX.Player_Rocket_Impact:
                audioSource.PlayOneShot(model.Player_Rocket_Impact);
                break;
            case SFX.Coin_Plus:
                audioSource.PlayOneShot(model.Coin_Plus);
                break;
            case SFX.Countdown_Counting:
                audioSource.PlayOneShot(model.Countdown_Counting);
                break;
            case SFX.Countdown_End:
                audioSource.PlayOneShot(model.Countdown_End);
                break;
            case SFX.UIClick:
                audioSource.PlayOneShot(model.UIClick);
                break;
            case SFX.UITransition_Open:
                audioSource.PlayOneShot(model.UITransition_Open);
                break;
            case SFX.UITransition_Close:
                audioSource.PlayOneShot(model.UITransition_Close);
                break;
            case SFX.Round_Success:
                audioSource.PlayOneShot(model.Round_Success);
                break;
            case SFX.GameOver:
                audioSource.PlayOneShot(model.GameOver);
                break;
        }
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
