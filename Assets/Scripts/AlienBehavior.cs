using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBehavior : EnemyBehavior
{
    #region Fields

    //States

    [SerializeField]
    private bool rotating = false;
    [SerializeField]
    private bool moving = false;
    private bool hasObstacle = false;
    [SerializeField]
    private bool entering = false;

    private bool stuck = false;

    private float rotationProgress = 0;

    private Transform currentTransformVal;

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject projectile;
    private float shootTimer;

    public LayerMask Interactables;
    private float thinkingCooldown = 0f;


    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private float spaceMinX = 0f;
    private float spaceMaxX = 0f;
    private float spaceMinY = 0f;
    private float spaceMaxY = 0f;

    private Vector3 targetPos = new Vector3(0,0,0);

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        //SetRotation();
        movementSpeed = GameController.Instance.AlienSpeed;
        thinkingCooldown = GameController.Instance.AlienDetectCooldown;
        shootTimer = GameController.Instance.AlienShootCooldown;
    }

    void Start()
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
    }

    private void Update()
    {
        if (entering)
            EnterSpace();

        if (rotating && !entering)
            Rotate();

        if (moving && !entering)
            Move();

        ScanPath();
    }

    void LateUpdate()
    {
        if (!entering)
        {
            Vector3 viewPos = transform.position;

            viewPos.x = Mathf.Clamp(viewPos.x, spaceMinX, spaceMaxX);
            viewPos.y = Mathf.Clamp(viewPos.y, spaceMinY, spaceMaxY);
            transform.position = viewPos;
        }

        if ((transform.position.x == spaceMinX || transform.position.x == spaceMaxX ||
            transform.position.y == spaceMinY || transform.position.y == spaceMaxY ) && !stuck)
        {
            stuck = true;
            SetRotation();
            moving = false;
        }
    }

    #endregion

    #region Methods

    private void EnterSpace()
    {
        //rotate and face this direction

        var offset = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
        var angle = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //move towards destination
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 3f * Time.deltaTime);
        if(Vector3.Distance(transform.position, targetPos) <= 0.01f)
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
        hasObstacle = false;
        thinkingCooldown = GameController.Instance.AlienDetectCooldown;
    }

    private void Rotate()
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
            //ScanPath();
        }
    }

    private void Move()
    {
        transform.Translate(new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime);
        CheckStuckStatus();
    }

    private void ScanPath()
    {
        if (!rotating && !entering)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(spawnPoint.position, transform.right, GameController.Instance.AlienForwardDetection, Interactables);

            //stop if there is a blocker
            if (rayHit)
            {
                moving = false;
                hasObstacle = true;
                //print(hit.collider.tag);

                if (rayHit.collider.CompareTag("Player") || rayHit.collider.CompareTag("Asteroid"))
                    Shoot();
                else if (rayHit.collider.CompareTag("Enemy"))
                    Thinking();
            }
            else
            {
                //if there are no more obstacles, cooldown then rotate
                if (hasObstacle)
                {
                    Thinking();
                }
            }
        }
    }

    private void Thinking()
    {
        thinkingCooldown -= Time.deltaTime;

        if (thinkingCooldown <= 0)
        {
            SetRotation();
        }
    }
    private void CheckStuckStatus()
    {
        thinkingCooldown -= Time.deltaTime;
        if (thinkingCooldown <= 0)
        {
            stuck = false;
            thinkingCooldown = GameController.Instance.AlienDetectCooldown;
        }
    }

    private void Shoot()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            shootTimer = GameController.Instance.AlienShootCooldown;
        }
    }

    #endregion
}
