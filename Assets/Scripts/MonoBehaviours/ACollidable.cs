using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACollidable : MonoBehaviour
{
    public int pointWhenKilled = 0;
    public virtual void OnBump(int addScore)
    {
        //no additional score normally (e.g. bullet hits another bullet)
        //add score only in Player, PlayerBullet, PlayerRocket

        Destroy(gameObject);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision detected");
        var hasCollidable =
            collision.transform.TryGetComponent<ACollidable>(out var other);
        if (hasCollidable)
        {
            other.OnBump(pointWhenKilled);
        }
    }
}
