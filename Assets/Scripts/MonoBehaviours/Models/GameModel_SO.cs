using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Model",
    menuName = "ScriptableObjects/New Game Model")]
public class GameModel_SO : ScriptableObject
{
    [Header("Control Settings")]
    public ControlOption playerControls = ControlOption.MotionFacing;

    [Header("Mode")]
    public ModeOption mode = ModeOption.Normal;

    [Header("Game Data")]
    public float MaxSpawnTime = 5f;
    public float MinSpawnTime = 2f;
    public float SpawnTimePercentDeduction = 0.10f;
    public float CachedTimeInterval = 0f;
    public int Round;

    public void ResetRound()
    {
        Round = 0;
    }
}
