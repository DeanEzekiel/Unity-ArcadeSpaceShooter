using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ACharacter
{
    #region Private Fields
    private float shootTimer = 0;
    private float blasterTimer = 0;
    private Vector3 movementDirection;

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject rocket;

    #endregion

    #region Unity Callbacks
    private void Start()
    {
        shield.SetActive(false); //ensure that shield is inactive at first
    }

    private void Update()
    {
        if (GameMaster.Instance.PlayerControlsActive)
        {
            Move();
            Rotate();
            Shoot();

            ShieldAbility();
            BlasterAbility();
        }
    }

    private void OnEnable()
    {
        GameMaster.ShieldToggle += ToggleShield;
    }

    private void OnDisable()
    {
        GameMaster.ShieldToggle -= ToggleShield;
    }
    #endregion

    #region ACharacter Implementation
    protected override void Move()
    {
        //Movement
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        //Vector3 move = new Vector3(horizontalMove, verticalMove, 0);
        movementDirection = new Vector3(horizontalMove, verticalMove, 0);
        transform.Translate(movementDirection *
            GameMaster.Instance.playerSettings.playerSpeed * Time.deltaTime,
            Space.World);
    }

    protected override void Rotate()
    {
        if (GameMaster.Instance.gameSettings.playerControls ==
            ControlOption.MotionFacing)
        {
            //rotate based on movement direction & need to adjust by 90 degrees
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,
                    movementDirection) * Quaternion.Euler(0, 0, 90);//90 deg
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    toRotation,
                    GameMaster.Instance.playerSettings.playerRotationSpeed *
                    Time.deltaTime);
            }
        }
        else if (GameMaster.Instance.gameSettings.playerControls ==
            ControlOption.CursorFacing)
        {
            //no need to adjust 90 degrees
            var mouse = Input.mousePosition;
            var screenPnt = Camera.main.WorldToScreenPoint(transform.position);
            var offset = new Vector2(mouse.x - screenPnt.x,
                mouse.y - screenPnt.y);
            var angle = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    #endregion

    #region ACollidable Implementation
    public override void OnBump(int addScore)
    {
        if (GameMaster.Instance.playerSettings.shieldOn == false)
        {
            GameMaster.Instance.playerSettings.life--;
        }
        //score
        GameMaster.Instance.playerSettings.score += addScore;
    }
    #endregion

    #region Class Implementation
    private void Shoot()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
                shootTimer = GameMaster.Instance.playerSettings.playerBulletCooldown;
            }
        }
    }
    private void ShieldAbility()
    {
        //check if player can toggle the shield
        if (Input.GetButtonDown("Jump"))
        {
            ToggleShield();
        }

    }
    private void ToggleShield()
    {
        bool shieldState = shield.activeInHierarchy;
        shield.SetActive(!shieldState);
        GameMaster.Instance.playerSettings.shieldOn = !shieldState;
    }
    private void BlasterAbility()
    {
        blasterTimer -= Time.deltaTime;

        if (blasterTimer <= 0)
        {
            if (Input.GetButtonUp("Fire2"))
            {
                if (GameMaster.Instance.playerSettings.rocketCount > 0)
                {
                    GameMaster.Instance.playerSettings.rocketCount--;
                    Instantiate(rocket, spawnPoint.position, spawnPoint.rotation);
                    blasterTimer = GameMaster.Instance.playerSettings.rocketCooldown;
                }
            }
        }
    }
    #endregion
}