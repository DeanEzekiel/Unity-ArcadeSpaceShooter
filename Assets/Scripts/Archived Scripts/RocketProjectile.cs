using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : ProjectileBehavior
{
    private bool hit = false;

    [SerializeField]
    private GameObject blast;

    //private RaycastHit2D[] radiusHit;
    //private LayerMask Interactables;

    #region Unity Callbacks
    void Update()
    {
        if(!hit)
            transform.Translate(new Vector3(1, 0, 0) * GameController.Instance.PlayerRocketSpeed * Time.deltaTime);
        Destroy(gameObject, GameController.Instance.PlayerRocketLifetime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag(Tags.Player) && !col.gameObject.CompareTag(Tags.Coin))
        {
            hit = true;
            blast.SetActive(true);

            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.enabled = false;

            CapsuleCollider2D capc = gameObject.GetComponent<CapsuleCollider2D>();
            capc.enabled = false;

            //DGS - when using Circle Collider, comment the CircleCast
            CircleCollider2D cc = gameObject.AddComponent<CircleCollider2D>();
            cc.radius = GameController.Instance.BlastRadius;
            cc.isTrigger = true;

            ////DGS - CircleCastAll -- coin generation won't work so if using this, need to optimize that as well
            //Interactables = LayerMask.GetMask(Layers.Interactables);
            //radiusHit = Physics2D.CircleCastAll(transform.position, GameController.Instance.BlastRadius, Vector3.zero, 0f, Interactables);

            //foreach(var obj in radiusHit)
            //{
            //    if(obj.collider.CompareTag(Tags.Alien) || obj.collider.CompareTag(Tags.Asteroid))
            //    {
            //        if (obj.collider.CompareTag(Tags.Alien))
            //            AlienKilled?.Invoke();
            //        else if (obj.collider.CompareTag(Tags.Asteroid))
            //            Asteroid1Killed?.Invoke();

            //        print($"Hit {obj.collider.tag}");
            //        Destroy(obj.collider.gameObject); //destroy that object
            //    }
            //}
        }

    }
    #endregion

    //#region Events
    ////When invoked, this will add points
    //public static event Action Asteroid1Killed;
    //public static event Action AlienKilled;
    //#endregion
}
