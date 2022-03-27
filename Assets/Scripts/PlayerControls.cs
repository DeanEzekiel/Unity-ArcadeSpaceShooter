using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    #region Private Fields
    private float shootTimer = 0;
    private float blasterTimer = 0;

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
        if (GameController.Instance.PlayerControlsActive)
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
        GameController.RocketLaunch += LaunchRocket;
        GameController.ShieldToggle += ToggleShield;
    }

    private void OnDisable()
    {
        GameController.RocketLaunch -= LaunchRocket;
        GameController.ShieldToggle -= ToggleShield;
    }

    #endregion

    #region Methods

    private void Move()
    {
        //Movement
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontalMove, verticalMove, 0);
        transform.Translate(move * GameController.Instance.PlayerSpeed * Time.deltaTime, Space.World);
    }

    private void Rotate()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shoot()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
                shootTimer = GameController.Instance.PlayerShootCooldown;
            }
        }
    }

    private void ShieldAbility()
    {
        //check if player can toggle the shield
        if(Input.GetButtonDown("Jump"))
            OnShieldToggle?.Invoke();
    }

    private void BlasterAbility()
    {
        blasterTimer -= Time.deltaTime;

        if (blasterTimer <= 0)
        {
            if (Input.GetButtonUp("Fire2"))
            {
                OnRocketLaunch?.Invoke();
            }
        }
    }

    private void LaunchRocket()
    {
        Instantiate(rocket, spawnPoint.position, spawnPoint.rotation);
        blasterTimer = GameController.Instance.PlayerRocketCooldown;
    }

    private void ToggleShield()
    {
        bool shieldState = shield.activeInHierarchy;
        shield.SetActive(!shieldState); //activate / deactivate the shield for effect
    }

    #endregion

    #region Events

    public static event Action OnRocketLaunch;
    public static event Action OnShieldToggle;

    #endregion
}
