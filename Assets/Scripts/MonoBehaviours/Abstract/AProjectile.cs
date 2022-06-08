using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AProjectile : ACollidable
{
    protected float speed;
    protected float lifetime;

    public virtual void SetSpecs(float speedVal, float lifetimeVal)
    {
        speed = speedVal;
        lifetime = lifetimeVal;
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        Invoke(nameof(Deactivate), 5f);
    }

    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
    }
}
