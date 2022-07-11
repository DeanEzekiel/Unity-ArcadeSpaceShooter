using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Alien : AEnemy
{
    #region Fields
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private AlienForwardDetector detector;

    [SerializeField]
    private EnemyModel_SO _enemyModel;

    //states - start
    [SerializeField]
    private bool rotating = false;
    [SerializeField]
    private bool moving = false;
    //private bool hasObstacle = false;
    [SerializeField]
    private bool entering = false;

    [SerializeField]
    private bool stuck = false;
    //states - end

    //for logic - start
    private float rotationProgress = 0;
    private Vector3 targetPos = new Vector3(0, 0, 0);
    private Transform currentTransformVal;
    private float shootTimer;

    //private LayerMask Interactables;
    private float thinkingCooldown = 0f;

    private float defaultThinkCooldown = 0f;
    private float defaultBulletCooldown = 0f;
    //for logic - end

    // Sprite Randomize - start
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();
    // Sprite Randomize - end
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        // need its own private _enemyModel for the Welcome Scene
        movementSpeed = _enemyModel.alienSpeed;
        hit = false;
        pointWhenKilled = _enemyModel.alienPoints;
        thinkingCooldown = _enemyModel.alienDetectCooldown;
        shootTimer = _enemyModel.alienBulletCooldown;

        defaultThinkCooldown = _enemyModel.alienDetectCooldown;
        defaultBulletCooldown = _enemyModel.alienBulletCooldown;

        SetScreenBounds();
        Entering();
    }

    public override void OnDisable()
    {
        if (hit)
        {
            Controller.ShowEffect(VFX.AlienHit, transform.position);
        }
        base.OnDisable();
    }

    private void Update()
    {
        if (entering)
        {
            EnterSpace();
            //detector.CanDetect = false;
        }

        if (rotating && !entering)
        {
            Rotate();
            //detector.CanDetect = false;
        }

        if (moving && !entering)
        {
            Move();
            //detector.CanDetect = true;
        }

        ForwardDetection();
    }

    private void LateUpdate()
    {
        if (!entering)
        {
            Vector3 viewPos = transform.position;

            viewPos.x = Mathf.Clamp(viewPos.x, spaceMinX, spaceMaxX);
            viewPos.y = Mathf.Clamp(viewPos.y, spaceMinY, spaceMaxY);
            transform.position = viewPos;
        }

        if ((transform.position.x == spaceMinX || transform.position.x == spaceMaxX ||
            transform.position.y == spaceMinY || transform.position.y == spaceMaxY) && !stuck)
        {
            stuck = true;
            SetRotation();
            moving = false;
        }
    }

    private void RandomizeSprite()
    {
        int random = UnityEngine.Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[random];
    }
    #endregion

    #region Methods
    public override void Activate()
    {
        base.Activate();
        RandomizeSprite();
        movementSpeed = Controller.AlienSpeed;
        hit = false;
        pointWhenKilled = Controller.AlienPoints;
        thinkingCooldown = Controller.AlienDetectCooldown;
        shootTimer = Controller.AlienBulletCooldown;

        defaultThinkCooldown = Controller.AlienDetectCooldown;
        defaultBulletCooldown = Controller.AlienBulletCooldown;

        Entering();
    }
    public override void OnBump(int addScore)
    {
        hit = true;
        base.OnBump(addScore);
    }

    private void Entering()
    {
        Vector3 viewPos = transform.position;
        if (viewPos.x < spaceMinX)
            targetPos.x = spaceMinX + 0.5f;
        else if (viewPos.x > spaceMaxX)
            targetPos.x = spaceMaxX - 0.5f;
        else
            targetPos.x = viewPos.x;

        if (viewPos.y < spaceMinY)
            targetPos.y = spaceMinY + 0.5f;
        else if (viewPos.y > spaceMaxY)
            targetPos.y = spaceMaxY - 0.5f;
        else
            targetPos.y = viewPos.y;

        entering = true;
        detector.DisableTrigger();
        //detector.CanDetect = false;
    }

    private void EnterSpace()
    {
        //rotate and face this direction

        var offset = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
        var angle = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //move towards destination
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 3f * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) <= 0.01f)
        {
            transform.position = targetPos;
            entering = false;

            SetRotation();
        }
    }

    private void SetRotation()
    {
        rotationZ = UnityEngine.Random.Range(0f, 360f);

        currentTransformVal = transform;
        rotating = true;
        rotationProgress = 0;
        //hasObstacle = false;
        thinkingCooldown = defaultThinkCooldown;

        detector.DisableTrigger();
        //detector.CanDetect = false;
    }

    protected override void Move()
    {
        transform.Translate(new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime);
        CheckStuckStatus();
    }

    protected override void Rotate()
    {
        Vector3 targetRotation = new Vector3(0, 0, rotationZ);
        if (Vector3.Distance(transform.eulerAngles, targetRotation) > 0.01f)
        {
            rotationProgress += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(currentTransformVal.rotation.eulerAngles, targetRotation, rotationProgress);
        }
        else
        {
            transform.eulerAngles = targetRotation;
            rotating = false;
            moving = true;


            detector.EnableTrigger();
            //detector.CanDetect = true;
        }
    }

    private void Thinking()
    {
        //DGS if using triggers, this should yield
        thinkingCooldown -= Time.deltaTime;

        if (thinkingCooldown <= 0)
        {
            SetRotation();
            detector.Think = false;
        }
    }
    private void CheckStuckStatus()
    {
        thinkingCooldown -= Time.deltaTime;
        if (thinkingCooldown <= 0)
        {
            stuck = false;
            thinkingCooldown = defaultThinkCooldown;
        }
    }

    private void Shoot()
    {
        //DGS if using triggers, this should yield
        shootTimer -= Time.deltaTime;
        //print("Shoot Wait");

        if (shootTimer <= 0)
        {
            //Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            var bullet = Controller.GetBullet();
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.rotation;
            bullet.SetSpecs(Controller.AlienBulletSpeed, Controller.AlienBulletLifetime);
            bullet.Activate();

            AudioController.Instance.PlaySFX(SFX.Enemy_Shoot);

            shootTimer = defaultBulletCooldown;
            //print("Shoot Now");
        }
    }

    private void ForwardDetection()
    {
        if (!rotating && !entering)
        {
            //moving = false; //stop moving for a moment

            if (detector.Shoot)
            {
                moving = false;
                Shoot();
            }
            else if (detector.Think)
            {
                moving = false;
                Thinking();
            }
        }
    }
    #endregion
}
