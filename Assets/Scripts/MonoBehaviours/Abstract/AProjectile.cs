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
        StartCoroutine(C_DeactivateAfterSec());
    }

    protected IEnumerator C_DeactivateAfterSec()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
    }
}
