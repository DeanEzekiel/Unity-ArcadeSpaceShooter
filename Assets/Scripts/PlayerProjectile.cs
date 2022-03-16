using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : ProjectileBehavior
{
    
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * GameController.Instance.PlayerShootSpeed * Time.deltaTime);
        Destroy(gameObject, GameController.Instance.PlayerShootLifetime);
    }
}
