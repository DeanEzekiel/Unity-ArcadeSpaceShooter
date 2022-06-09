using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : AProjectile
{
    public override void OnBump(int addScore)
    {
        //add score
        GameController.Instance.AddScore(addScore);
        base.OnBump(addScore); //destroys gameobject
    }
}
