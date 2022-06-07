using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : AProjectile
{
    [SerializeField]
    protected EnemyModel EnemyModel;
    private void Start()
    {
        SetSpecs(EnemyModel.alienBulletSpeed, EnemyModel.alienBulletLifetime);
    }
}
