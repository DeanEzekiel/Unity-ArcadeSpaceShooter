using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    #region Unity Callbacks

    void OnCollisionEnter2D(Collision2D col)
    {
        
        //destroy this projectile only. The asteroid is already being destroyed if it collides with anything.
        Destroy(gameObject);
    }
    #endregion
}
