using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Model",
    menuName = "ScriptableObjects/New Enemy Model")]
public class EnemyModel_SO : ScriptableObject
{
    [Header("Enemy Specs")]
    public int life = 1;
    public float asteroidSpeedMin = 1f;
    public float asteroidSpeedMax = 2f;
    [Space]
    public float alienSpeed = 2f;
    public float alienForwardDetection = 5f;
    public float alienDetectCooldown = 0.5f;
    [Space]
    public float alienBulletSpeed = 13f;
    public float alienBulletLifetime = 1.2f;
    public float alienBulletCooldown = 1;

    [Header("Points and Coins")]
    public int alienPoints = 100;
    public int asteroidPoints = 50;
    public int coinDropRate = 40;
    public int coinLifetime = 3;

    public EnemyModel_SO Clone()
    {
        return this;
    }

    public EnemyModel_SO DeepClone(EnemyModel_SO other)
    {
        other.life = this.life;
        other.asteroidSpeedMin = this.asteroidSpeedMin;
        other.asteroidSpeedMax = this.asteroidSpeedMax;
        other.alienSpeed = this.alienSpeed;
        other.alienForwardDetection = this.alienForwardDetection;
        other.alienDetectCooldown = this.alienDetectCooldown;
        other.alienBulletSpeed = this.alienBulletSpeed;
        other.alienBulletLifetime = this.alienBulletLifetime;
        other.alienBulletCooldown = this.alienBulletCooldown;
        other.alienPoints = this.alienPoints;
        other.asteroidPoints = this.asteroidPoints;
        other.coinDropRate = this.coinDropRate;
        other.coinLifetime = this.coinLifetime;

        return other;
    }
}
