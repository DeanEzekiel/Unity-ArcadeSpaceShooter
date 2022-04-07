using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienProjectile : ProjectileBehavior
{

    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * GameController.Instance.AlienShootSpeed * Time.deltaTime);
        Destroy(gameObject, GameController.Instance.AlienShootLifetime);
    }
}
