using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : ProjectileBehavior
{
    private bool hit = false;

    [SerializeField]
    private GameObject blast;

    #region Unity Callbacks
    void Update()
    {
        if(!hit)
            transform.Translate(new Vector3(1, 0, 0) * GameController.Instance.PlayerRocketSpeed * Time.deltaTime);
        Destroy(gameObject, GameController.Instance.PlayerRocketLifetime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
        {
            OnRocketCollide?.Invoke();
            hit = true;
            blast.SetActive(true);

            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.enabled = false;

            CapsuleCollider2D capc = gameObject.GetComponent<CapsuleCollider2D>();
            capc.enabled = false;

            CircleCollider2D cc = gameObject.AddComponent<CircleCollider2D>();
            cc.radius = GameController.Instance.BlastRadius;
            cc.isTrigger = true;
        }

    }
    #endregion

    #region Events
    public static event Action OnRocketCollide;
    #endregion
}
