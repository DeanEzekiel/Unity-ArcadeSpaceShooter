using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : AProjectile
{
    private bool hit = false;

    [SerializeField]
    private GameObject blast;

    private void Start()
    {
        speed = GameMaster.Instance.playerSettings.rocketSpeed;
        lifetime = GameMaster.Instance.playerSettings.rocketLifetime;

        blast.SetActive(false);
    }

    protected override void Update()
    {
        if(!hit)
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }
        Destroy(gameObject, lifetime);
    }

    public override void OnBump(int addScore)
    {
        //add score - points from the directly hit
        GameMaster.Instance.playerSettings.score += addScore;
        //don't destroy it yet

        hit = true; //once hit, it will stop moving
        blast.SetActive(true); //blast becomes visible

        //rocket's sprite and collider becomes invisible & untouchable
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
        CapsuleCollider2D capsule = gameObject.GetComponent<CapsuleCollider2D>();
        capsule.enabled = false;

        Explode();
    }

    private void Explode()
    {
        LayerMask Interactables = LayerMask.GetMask(Layers.Interactables);
        RaycastHit2D[] radiusHit = Physics2D.CircleCastAll(transform.position,
            GameMaster.Instance.playerSettings.rocketBlastRadius,
            Vector3.zero, 0f, Interactables);

        foreach (var obj in radiusHit)
        {
            var isEnemy = obj.transform.TryGetComponent<AEnemy>(out var enemy);
            if (isEnemy)
            {
                //add points
                GameMaster.Instance.playerSettings.score += enemy.pointWhenKilled;
                enemy.hit = true; //enemy is hit because of the explosion
                Destroy(obj.collider.gameObject);//destroy that object
                //if the hit is true, on destroy > generate a coin randomly
            }
        }
    }
}
