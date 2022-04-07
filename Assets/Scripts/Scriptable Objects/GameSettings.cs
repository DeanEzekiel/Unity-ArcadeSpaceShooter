using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings",
    menuName = "ScriptableObjects/New GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Control Settings")]
    public ControlOption playerControls = ControlOption.MotionFacing;

    [Header("Mode")]
    public ModeOption mode = ModeOption.Normal;
}
