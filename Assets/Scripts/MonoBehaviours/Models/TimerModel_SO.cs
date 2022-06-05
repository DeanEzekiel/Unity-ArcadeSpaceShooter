using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Timer Model",
    menuName = "ScriptableObjects/New Timer Model")]
public class TimerModel_SO : ScriptableObject
{
    [Header("Timer on Gameplay")]
    public float TimeLeft = 15f;
    [Range(0f, 200f)]
    [Tooltip("The number of seconds per round.")]
    public float TimePerRound = 15f;

}
