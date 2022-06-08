using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : AProjectile
{
    private bool hit = false;
    private float blastRadius;

    [SerializeField]
    private GameObject blast;

    public void SetSpecs(float speedVal, float lifetimeVal, float radiusVal)
    {
        speed = speedVal;
        lifetime = lifetimeVal;
        blastRadius = radiusVal;

        blast.SetActive(false);
    }

    protected override void Update()
    {
        if(!hit)
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }
    }

    public override void OnBump(int addScore)
    {
        //add score - points from the directly hit
        GameController.Instance.AddScore(addScore);
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
            blastRadius,
            Vector3.zero, 0f, Interactables);

        foreach (var obj in radiusHit)
        {
            var isEnemy = obj.transform.TryGetComponent<AEnemy>(out var enemy);
            if (isEnemy)
            {
                //add points
                GameController.Instance.AddScore(enemy.pointWhenKilled);
                enemy.hit = true; //enemy is hit because of the explosion
                //Destroy(obj.collider.gameObject);//destroy that object
                obj.collider.gameObject.SetActive(false);
                //if the hit is true, on destroy > generate a coin randomly
            }
        }
    }
}
