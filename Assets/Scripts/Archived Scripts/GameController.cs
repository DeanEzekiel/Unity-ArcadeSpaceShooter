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
    private int score = 0;
    [SerializeField]
    private float timePerRound = 15f;
    //[SerializeField]
    //private float timer;
    //[SerializeField]
    //private int life = 3;
    [SerializeField]
    private int rocketMax = 3;
    //[SerializeField]
    //private int rocketCnt = 3;
    [SerializeField]
    private float shieldMax = 100f;
    //[SerializeField]
    //private float shieldPnt = 100f;
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
    private int alienPoints = 100;
    [SerializeField]
    private int asteroidPoints = 50;

    [SerializeField]
    private GameObject GameOverUI;
    [SerializeField]
    private GameObject GamePausedUI;
    [SerializeField]
    private GameObject ShopUI;

    #endregion

    #region Accessors
    public int Life { get; private set; } = 3;
    public int RocketCnt { get; private set; } = 3;
    public float ShieldPnt { get; private set; } = 100f;
    public float Timer { get; private set; }

    //planning on making these public get and private set
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
    public float PlayerRotationSpeed = 720;

    public float AsteroidSpeedMin = 1f;
    public float AsteroidSpeedMax = 2f;

    public float AlienForwardDetection = 5f; //this is the scale x; position x to be adjusted to half of this value
    public float AlienSpeed = 2f;
    public float AlienDetectCooldown = 0.5f;

    public float AlienShootSpeed = 13f;
    public float AlienShootLifetime = 1.2f;
    public float AlienShootCooldown = 1;

    public bool PlayerControlsActive { get; private set; } = true;
    public int PlayerControlSetting = 1; //1 = rotate per direction; 2 = rotate look at mouse

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
            Life--;
            print($"Player has been hit! Player Life now at: {Life}/3");
        }

        if(Life == 0)
        {
            GameOver();
        }
    }

    private void CollectCoin()
    {
        totalCoinsCollected++;
        print($"Total Coins Collected: {totalCoinsCollected}");
    }

    private void AlienKilled()
    {
        //TO DO plus points here
        AddPoints(alienPoints);
    }

    private void AsteroidKilled()
    {
        AddPoints(asteroidPoints);
    }

    private void AddPoints(int points)
    {
        score += points;
        print($"Current Score: {score}");
    }

    private void CheckRockets()
    {
        if (RocketCnt > 0)
        {
            RocketLaunch?.Invoke();
            RocketCnt--;
        }
    }

    private void CheckShield()
    {
        ShieldToggle?.Invoke();
        shieldOn = !shieldOn;
    }

    private void ShieldPointsCalc()
    {
        if(shieldOn && ShieldPnt > 0)
        {
            ShieldPnt -= (shieldDepletePnt / shieldDepleteTime * Time.deltaTime);
        }
        else if (!shieldOn && ShieldPnt < shieldMax)
        {
            ShieldPnt += (shieldReplenishPnt / shieldReplenishTime * Time.deltaTime);
        }

        if (ShieldPnt > shieldMax)
            ShieldPnt = shieldMax;
        else if (ShieldPnt <= 0)
        {
            ShieldPnt = 0;
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

    private void GameOver()
    {
        GameOverUI.SetActive(true);

        //destroy Player, Asteroid, Enemy, Projectiles
        DestroyWithTag(Tags.Player);
        DestroyWithTag(Tags.Asteroid);
        DestroyWithTag(Tags.Alien);
        DestroyWithTag(Tags.PlayerProjectile);
        DestroyWithTag(Tags.Rocket);
        DestroyWithTag(Tags.AlienProjectile);
        DestroyWithTag(Tags.Coin);

        //Time Stops so that timer won't tick
        PauseTime();
    }

    private void ShowShop()
    {
        ShopUI.SetActive(true);

        //disable player controls while shop is active or just use a bool
        DeactivatePlayerControls();

        //remove all aliens, asteroids and projectiles
        DestroyWithTag(Tags.Asteroid);
        DestroyWithTag(Tags.Alien);
        DestroyWithTag(Tags.PlayerProjectile);
        DestroyWithTag(Tags.Rocket);
        DestroyWithTag(Tags.AlienProjectile);
        DestroyWithTag(Tags.Coin);
    }

    private void ActivatePlayerControls()
    {
        PlayerControlsActive = true;
    }
    private void DeactivatePlayerControls()
    {
        PlayerControlsActive = false;
    }

    private void TimerCountdown()
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0 && Life >= 1)
        {
            //show SHOP
            ShowShop();
            PauseTime();
        }
    }

    private void ResetTimer()
    {
        Timer = timePerRound;
    }

    private void ResetValues()
    {
        GameOverUI.SetActive(false);
        GamePausedUI.SetActive(false);
        ShopUI.SetActive(false);

        ResetTimer();
        ActivatePlayerControls();

        PlayTime();
    }

    private void RestartGame()
    {
        GameOverUI.SetActive(false);
        GamePausedUI.SetActive(false);
        ShopUI.SetActive(false);

        ResetTimer();
        ActivatePlayerControls();

        //reset life, coins earned, score, rocket count, mana
        RocketCnt = rocketMax;
        ShieldPnt = shieldMax;
        shieldOn = false;

        totalCoinsCollected = 0;
        score = 0;
        Life = 3;

        PlayTime();
    }

    private void PauseGame()
    {
        if (!GameOverUI.activeInHierarchy && !ShopUI.activeInHierarchy)
        {
            PauseTime();
            DeactivatePlayerControls();
            GamePausedUI.SetActive(true);
        }
    }

    private void ResumeGame()
    {
        PlayTime();
        ActivatePlayerControls();
        GamePausedUI.SetActive(false);
    }

    private void PlayTime()
    {
        Time.timeScale = 1;
    }

    private void PauseTime()
    {
        Time.timeScale = 0;
    }


    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        InitSingleton();
        DontDestroyOnLoad(gameObject);

        GameOverUI.SetActive(false);
        GamePausedUI.SetActive(false);
        ShopUI.SetActive(false);
        ShieldPnt = shieldMax;
        RocketCnt = rocketMax;
    }

    private void Start()
    {
        ResetTimer();
        ActivatePlayerControls();
    }

    private void Update()
    {
        ShieldPointsCalc();
        TimerCountdown();
    }

    private void OnEnable()
    {
        PlayerBoundaries.OnPlayerCollision += PlayerCollision;

        PlayerControls.OnRocketLaunch += CheckRockets;
        PlayerControls.OnShieldToggle += CheckShield;

        CoinBehavior.CoinPickedUp += CollectCoin;

        EnemyBehavior.AlienKilled += AlienKilled;
        EnemyBehavior.AsteroidKilled += AsteroidKilled;
        //PlayerProjectile.AlienKilled += AlienKilled;
        //PlayerProjectile.AsteroidKilled += AsteroidKilled;
        //RocketProjectile.AlienKilled += AlienKilled;
        //RocketProjectile.Asteroid1Killed += AsteroidKilled;

        GameUIView.RestartScene += ResetValues;
        GameUIView.RestartGame += RestartGame;
        GameUIView.PauseGame += PauseGame;
        GameUIView.ResumeGame += ResumeGame;
    }
    private void OnDisable()
    {
        PlayerBoundaries.OnPlayerCollision -= PlayerCollision;

        PlayerControls.OnRocketLaunch -= CheckRockets;
        PlayerControls.OnShieldToggle -= CheckShield;

        CoinBehavior.CoinPickedUp -= CollectCoin;

        EnemyBehavior.AlienKilled -= AlienKilled;
        EnemyBehavior.AsteroidKilled -= AsteroidKilled;
        //PlayerProjectile.AlienKilled -= AlienKilled;
        //PlayerProjectile.AsteroidKilled -= AsteroidKilled;
        //RocketProjectile.AlienKilled -= AlienKilled;
        //RocketProjectile.Asteroid1Killed -= AsteroidKilled;

        GameUIView.RestartScene -= ResetValues;
        GameUIView.RestartGame -= RestartGame;
        GameUIView.PauseGame -= PauseGame;
        GameUIView.ResumeGame -= ResumeGame;
    }
    #endregion

    #region Events

    public static event Action RocketLaunch;
    public static event Action ShieldToggle;

    #endregion
}
