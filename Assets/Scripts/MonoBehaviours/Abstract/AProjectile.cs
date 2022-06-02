using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AProjectile : ACollidable
{
    protected float speed;
    protected float lifetime;

    protected virtual void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        Destroy(gameObject, lifetime);
    }
}
