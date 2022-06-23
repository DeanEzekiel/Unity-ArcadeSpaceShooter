using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX Config",
    menuName = "ScriptableObjects/New SFX Config")]
public class SFXConfig : ScriptableObject
{
    public float Volume;
    [Header("Individual SFXs")]
    public AudioClip Player_Shield_Activate;
    public AudioClip Player_Shield_Deactivate;
    public AudioClip Player_Rocket_Launch;
    public AudioClip Player_Rocket_Impact;
    [Space]
    public AudioClip Coin_Plus;
    [Space]
    public AudioClip Countdown_Counting;
    public AudioClip Countdown_End;
    [Space]
    public AudioClip UIClick;
    public AudioClip UITransition_Open;
    public AudioClip UITransition_Close;
    [Space]
    public AudioClip Round_Success;
    public AudioClip GameOver;

    [Header("Randomizable SFXs")]
    public List<AudioClip> Player_Shoot;
    public List<AudioClip> Enemy_Shoot;
    public List<AudioClip> Hit_Crash;
}
