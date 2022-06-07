using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : AEnemy
{
    #region Fields
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private AlienForwardDetector detector;

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

    //screenbounds - start
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private float spaceMinX = 0f;
    private float spaceMaxX = 0f;
    private float spaceMinY = 0f;
    private float spaceMaxY = 0f;
    //screenbounds - end
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        movementSpeed = EnemyModel.alienSpeed;
        hit = false;
        pointWhenKilled = EnemyModel.alienPoints;
        thinkingCooldown = EnemyModel.alienDetectCooldown;
        shootTimer = EnemyModel.alienBulletCooldown;

        defaultThinkCooldown = EnemyModel.alienDetectCooldown;
        defaultBulletCooldown = EnemyModel.alienBulletCooldown;

        SetScreenBounds();
        Entering();
    }

    //private void OnEnable()
    //{
    //    detector.RegisterShooter(Shoot);
    //    detector.RegisterThinker(Thinking);
    //}

    //private void OnDisable()
    //{
    //    detector.UnregisterShooter(Shoot);
    //    detector.UnregisterThinker(Thinking);
    //}

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
    #endregion

    #region Methods
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

    private void SetScreenBounds()
    {
        //Start [Bounds] get Screen Bounds and movable space
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        spaceMinX = screenBounds.x * -1 + objectWidth;
        spaceMaxX = screenBounds.x - objectWidth;
        spaceMinY = screenBounds.y * -1 + objectHeight;
        spaceMaxY = screenBounds.y - objectHeight;
        //End [Bounds]
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
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
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
