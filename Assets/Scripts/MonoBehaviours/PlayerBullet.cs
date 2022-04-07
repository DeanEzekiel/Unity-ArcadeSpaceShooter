using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : AProjectile
{
    private void Start()
    {
        speed = GameMaster.Instance.playerSettings.playerBulletSpeed;
        lifetime = GameMaster.Instance.playerSettings.playerBulletLifetime;
    }

    public override void OnBump(int addScore)
    {
        //add score
        GameMaster.Instance.playerSettings.score += addScore;
        base.OnBump(addScore); //destroys gameobject
    }
}
