using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : AProjectile
{
    public void SetSpecs(float speedVal, float lifetimeVal)
    {
        speed = speedVal;
        lifetime = lifetimeVal;
    }

    public override void OnBump(int addScore)
    {
        //add score
        GameMaster.Instance.AddScore(addScore);
        base.OnBump(addScore); //destroys gameobject
    }
}
