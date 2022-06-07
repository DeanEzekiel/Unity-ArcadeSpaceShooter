using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : AProjectile
{
    protected EnemyModel_SO EnemyModel => GameController.Instance.Controller.Enemy.Model;
    private void Start()
    {
        SetSpecs(EnemyModel.alienBulletSpeed, EnemyModel.alienBulletLifetime);
    }
}
