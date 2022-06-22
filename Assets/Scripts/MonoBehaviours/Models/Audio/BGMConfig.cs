using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGM Config",
    menuName = "ScriptableObjects/New BGM Config")]
public class BGMConfig : ScriptableObject
{
    public float Volume;
    public List<AudioClip> MainMenu;
    public List<AudioClip> Shop;
    public List<AudioClip> Gameplay;
}
