using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : ASingleton<AudioController>
{
    [SerializeField]
    private BGMPlayer BGM;
    [SerializeField]
    private SFXPlayer SFX;

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();

        BGM.Init();
        SFX.Init();
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
