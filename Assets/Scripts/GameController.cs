using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton Instance

    private static readonly object s_lock = new object();
    private static GameController s_instance;
    public static GameController Instance
    {
        get
        {
            lock (s_lock)
            {
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<GameController>();
                }

                return s_instance;
            }
        }
    }

    #endregion

    #region Private Fields
    [SerializeField]
    private int life = 3;
    [SerializeField]
    private int rocketMax = 3;
    [SerializeField]
    private int rocketCnt = 3;
    [SerializeField]
    private float shieldMax = 100f;
    [SerializeField]
    private float shieldPnt = 100f;
    [SerializeField]
    private float shieldDepleteTime = 0.1f;
    [SerializeField]
    private int shieldDepletePnt = 5;
    [SerializeField]
    private float shieldReplenishTime = 0.1f;
    [SerializeField]
    private int shieldReplenishPnt = 5;

    private int totalCoinsCollected = 0;

    private bool shieldOn = false;

    [SerializeField]
    private GameObject testMenuUI;

    #endregion

    #region Accessors
    public int CoinDropRate = 40;
    public int CoinLifetime = 3; //3 seconds

    public float PlayerShootSpeed = 13f;
    public float PlayerShootLifetime = 1; //1 second
    public float PlayerShootCooldown = 0.5f;

    public float PlayerRocketSpeed = 15f;
    public float PlayerRocketLifetime = 1;
    public float PlayerRocketCooldown = 0.5f;
    public float BlastRadius = 2f;

    public float PlayerSpeed = 6;

    public float AsteroidSpeedMin = 5f;
    public float AsteroidSpeedMax = 12f;

    #endregion

    #region Class Implementation
    private void InitSingleton()
    {
        if (Instance.GetInstanceID() != GetInstanceID())
        {
            Debug.LogWarning($"Cannot have more than 1 GameControllers. Destroying {gameObject.name}", gameObject);
            Destroy(gameObject);
        }
    }

    private void PlayerCollision()
    {
        if (!shieldOn)
        {
            life--;
            print($"Player has been hit! Player Life now at: {life}/3");
        }

        if(life == 0)
        {
            //destroy Player, Asteroid, Enemy
            DestroyWithTag("Player");
            DestroyWithTag("Asteroid");
            DestroyWithTag("Enemy");
            DisplayTestMenu();
        }
    }

    private void CollectCoin()
    {
        totalCoinsCollected++;
        print($"Total Coins Collected: {totalCoinsCollected}");
    }

    private void EnemyKilled()
    {
        print("Bullseye");

        //TO DO plus points here
    }

    private void CheckRockets()
    {
        if (rocketCnt > 0)
        {
            RocketLaunch?.Invoke();
            rocketCnt--;
        }
    }

    private void CheckShield()
    {
        ShieldToggle?.Invoke();
        shieldOn = !shieldOn;
    }

    private void ShieldPointsCalc()
    {
        if(shieldOn && shieldPnt > 0)
        {
            shieldPnt -= (shieldDepletePnt / shieldDepleteTime * Time.deltaTime);
        }
        else if (!shieldOn && shieldPnt < shieldMax)
        {
            shieldPnt += (shieldReplenishPnt / shieldReplenishTime * Time.deltaTime);
        }

        if (shieldPnt > shieldMax)
            shieldPnt = shieldMax;
        else if (shieldPnt <= 0)
        {
            shieldPnt = 0;
            shieldOn = false;
            ShieldToggle?.Invoke();
        }
    }

    private void DestroyWithTag(string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    private void DisplayTestMenu()
    {
        testMenuUI.SetActive(true);
    }

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        InitSingleton();

        testMenuUI.SetActive(false);
        shieldPnt = shieldMax;
        rocketCnt = rocketMax;
    }

    private void Update()
    {
        ShieldPointsCalc();
    }

    private void OnEnable()
    {
        PlayerBoundaries.OnPlayerCollision += PlayerCollision;
        ProjectileBehavior.OnEnemyKill += EnemyKilled;
        RocketProjectile.OnRocketCollide += EnemyKilled;

        PlayerControls.OnRocketLaunch += CheckRockets;
        PlayerControls.OnShieldToggle += CheckShield;

        CoinBehavior.CoinPickedUp += CollectCoin;
    }
    private void OnDisable()
    {
        PlayerBoundaries.OnPlayerCollision -= PlayerCollision;
        ProjectileBehavior.OnEnemyKill -= EnemyKilled;
        RocketProjectile.OnRocketCollide -= EnemyKilled;

        PlayerControls.OnRocketLaunch -= CheckRockets;
        PlayerControls.OnShieldToggle -= CheckShield;

        CoinBehavior.CoinPickedUp -= CollectCoin;
    }
    #endregion

    #region Events

    public static event Action RocketLaunch;
    public static event Action ShieldToggle;

    #endregion
}
