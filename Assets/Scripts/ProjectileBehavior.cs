using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    #region Unity Callbacks

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!col.gameObject.CompareTag("Player"))
        {
            //TODO let the GameController know that a projectile hit a non-player object
            OnEnemyKill?.Invoke();
        }
        //destroy this projectile only. The asteroid is already being destroyed if it collides with anything.
        Destroy(gameObject);
    }
    #endregion

    #region Events
    public static event Action OnEnemyKill;
    #endregion
}
