using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : AProjectile
{
    private void Start()
    {
        speed = GameMaster.Instance.enemySettings.alienBulletSpeed;
        lifetime = GameMaster.Instance.enemySettings.alienBulletLifetime;
    }
}
