using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : AProjectile
{
    private bool hit = false;
    private float blastRadius;
    [SerializeField]
    private GameObject trailVFX;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;

    private GameObject target;

    public static event Action EnableTargetChecker;

    public void SetSpecs(float speedVal, float lifetimeVal, float radiusVal)
    {
        speed = speedVal;
        lifetime = lifetimeVal;
        blastRadius = radiusVal;
    }

    public void Initialize()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    public override void Activate()
    {
        hit = false;
        spriteRenderer.enabled = true;
        capsuleCollider.enabled = true;
        trailVFX.SetActive(true);
        base.Activate();

        target = null;
        EnableTargetChecker?.Invoke();
    }

    protected override void Update()
    {
        if(!hit)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right);
        }

        if (target != null)
        {
            if (target.activeInHierarchy)
            {
                PursueTarget();
                Debug.Log("Pursuing Target");
            }
        }
    }

    public void RegisterTarget(GameObject gameObject)
    {
        target = gameObject;
    }

    public override void OnBump(int addScore)
    {
        //add score - points from the directly hit
        GameController.Instance.AddScore(addScore);
        //don't destroy it yet

        hit = true; //once hit, it will stop moving

        //rocket's sprite and collider becomes invisible & untouchable
        spriteRenderer.enabled = false;
        capsuleCollider.enabled = false;
        trailVFX.SetActive(false);

        Explode();
    }

    private void Explode()
    {
        GameController.Instance.Controller.VFX.SpawnVFX(VFX.RocketImpact,
            transform.position);
        AudioController.Instance.PlaySFX(SFX.Player_Rocket_Impact);
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

    private void PursueTarget()
    {
        // !! Solution requires the sprite to be facing right
        // !! IF NOT, then subtract 90 to the value of the angle
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
}
