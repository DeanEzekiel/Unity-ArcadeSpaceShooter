using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : ASingleton<AudioController>
{
    [SerializeField]
    private BGMPlayer BGM;
    [SerializeField]
    private SFXPlayer SFX;

    #region Events
    public static event Action<float> ReflectBGMValue;
    public static event Action<float> ReflectSFXValue;
    #endregion // Events

    #region Unity Callbacks
    private void Start()
    {
        float bgmValue = PlayerPrefs.GetFloat(PlayerPrefKeys.fBGMVolume, 1f);
        float sfxValue = PlayerPrefs.GetFloat(PlayerPrefKeys.fSFXVolume, 1f);

        BGM.Init(bgmValue);
        SFX.Init(sfxValue);

        ReflectBGMValue?.Invoke(bgmValue);
        ReflectSFXValue?.Invoke(sfxValue);
    }
    #endregion // Unity

    #region Public Methods
    public void UpdateSFXVolume(float newValue)
    {
        SFX.UpdateVolume(newValue);
    }

    public void UpdateBGMVolume(float newValue)
    {
        BGM.UpdateVolume(newValue);
    }

    public void PlaySFX(SFX type)
    {
        SFX.Play(type);
    }

    public void StopSFX()
    {
        SFX.Stop();
    }

    public void PlayBGM(BGM type, bool switchClipRegardless)
    {
        BGM.Play(type, switchClipRegardless);
    }

    public void StopBGM()
    {
        BGM.Stop();
    }
    #endregion
}
