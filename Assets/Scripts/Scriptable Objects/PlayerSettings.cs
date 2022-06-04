using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings",
    menuName = "ScriptableObjects/New PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Game Values")]
    public int life = 3;
    public int score = 0;
    public int round = 0;
    public int coins = 0;

    [Space]
    [Range(0f, 200f)]
    [Tooltip("The number of seconds per round.")]
    public float timePerRound = 15f;
    public float timer = 15f;
    [Space]
    public int rocketCount = 3;
    public float shieldPoint = 100f;

    [Header("Player Specs")]
    public int lifeMax = 3;
    public float playerSpeed = 6;
    public float playerRotationSpeed = 720;
    [Space]
    public int rocketMax = 3;
    public float rocketSpeed = 15f;
    public float rocketLifetime = 1;
    public float rocketCooldown = 0.5f;
    public float rocketBlastRadius = 2f;
    [Space]
    public float shieldMax = 100f;
    public float shieldDepleteSec = 3f;
    public float shieldReplenishSec = 5f;
    public bool shieldOn = false;
    [Space]
    public float playerBulletSpeed = 13f;
    public float playerBulletLifetime = 1;
    public float playerBulletCooldown = 0.5f;

    public PlayerSettings Clone()
    {
        return this;
    }

    //deep clone can help in restart game situation
    public PlayerSettings DeepClone(PlayerSettings other)
    {
        other.life = this.life;
        other.score = this.score;
        other.round = this.round;
        other.coins = this.coins;
        other.timePerRound = this.timePerRound;
        other.timer = this.timer;
        other.rocketCount = this.rocketCount;
        other.shieldPoint = this.shieldPoint;
        other.playerSpeed = this.playerSpeed;
        other.playerRotationSpeed = this.playerRotationSpeed;
        other.rocketMax = this.rocketMax;
        other.rocketSpeed = this.rocketSpeed;
        other.rocketLifetime = this.rocketLifetime;
        other.rocketCooldown = this.rocketCooldown;
        other.rocketBlastRadius = this.rocketBlastRadius;
        other.shieldMax = this.shieldMax;
        other.shieldDepleteSec = this.shieldDepleteSec;
        other.shieldReplenishSec = this.shieldReplenishSec;
        other.shieldOn = this.shieldOn;
        other.playerBulletSpeed = this.playerBulletSpeed;
        other.playerBulletLifetime = this.playerBulletLifetime;
        other.playerBulletCooldown = this.playerBulletCooldown;

        return other;
    }
}