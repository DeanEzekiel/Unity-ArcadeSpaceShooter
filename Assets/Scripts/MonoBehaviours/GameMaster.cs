using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : ASingleton<GameMaster>
{
    #region Fields
    [SerializeField]
    private GameObject GameOverUI;
    [SerializeField]
    private GameObject GamePausedUI;
    [SerializeField]
    private GameObject ShopUI;

    public GameSettings gameSettings;
    public EnemySettings enemySettings;

    [SerializeField]
    private EnemySettings initialEnemySettings;

    public ControllerList Controller;

    [SerializeField]
    private float maxSpawnTime = 5f;
    [SerializeField]
    private float minSpawnTime = 2f;
    [SerializeField]
    private float spawnTimePercentDeduction = 0.10f;

    private float cachedTimeInterval = 0f;

    private int round;
    #endregion // Fields

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();

        GameOverUI.SetActive(false);
        GamePausedUI.SetActive(false);
        ShopUI.SetActive(false);
    }
    private void Start()
    {
        cachedTimeInterval = maxSpawnTime;
        OnStartRound();
        Controller.Player.Reset();
    }

    private void Update()
    {
        CheckTimeEnd();
        //TODO move to Player Controller
        if(Controller.Player.Life == 0)
        {
            GameOver();
        }
    }

    private void OnEnable()
    {
        GameView.RestartScene += ResetValues;
        GameView.RestartGame += RestartGame;
        GameView.PauseGame += PauseGame;
        GameView.ResumeGame += ResumeGame;

        enemySettings = initialEnemySettings.DeepClone(enemySettings);
    }

    private void OnDisable()
    {

        GameView.RestartScene -= ResetValues;
        GameView.RestartGame -= RestartGame;
        GameView.PauseGame -= PauseGame;
        GameView.ResumeGame -= ResumeGame;
    }
    #endregion

    #region Public Methods
    public void CoinCollected()
    {
        Controller.Player.CoinCollected();
    }

    public void AddScore(int value)
    {
        Controller.Player.AddScore(value);
    }
    #endregion // Public Methods

    #region Implementation

    private void ResetTimer()
    {
        Controller.Timer.ResetTimer();
        Controller.Timer.StartTimer();
    }
    //TODO refactor this to Timer and Game Controller
    private void CheckTimeEnd()
    {
        if (Controller.Timer.TimeLeft <= 0 && Controller.Player.Life >= 1)
        {
            Controller.EnemySpawn.StopSpawning();

            ShowShop();
            PauseTime();
        }
    }
    private void ActivatePlayerControls()
    {
        Controller.Player.AllowPlayerControls(true);
    }
    private void DeactivatePlayerControls()
    {
        Controller.Player.AllowPlayerControls(false);
    }
    private void GameOver()
    {
        Controller.EnemySpawn.StopSpawning();
        GameOverUI.SetActive(true);

        //destroy Player, Asteroid, Enemy, Projectiles
        Controller.Player.HideView();
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

        Controller.Shop.UpdateViewTexts();
        Controller.Shop.CheckMaxAllowed();

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
    private void DestroyWithTag(string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    private void PauseGame()
    {
        Debug.Log("GameMaster Pause Game");
        if (!GameOverUI.activeInHierarchy && !ShopUI.activeInHierarchy)
        {
            PauseTime();
            DeactivatePlayerControls();
            GamePausedUI.SetActive(true);
        }
    }

    private void ResumeGame()
    {
        Debug.Log("GameMaster Resume Game");
        PlayTime();
        ActivatePlayerControls();
        GamePausedUI.SetActive(false);
    }

    private void ResetValues()
    {
        Debug.Log("GameMaster Reset Values");
        GameOverUI.SetActive(false);
        GamePausedUI.SetActive(false);
        ShopUI.SetActive(false);

        OnStartRound();
    }

    private void RestartGame()
    {
        Debug.Log("GameMaster Restart Game");
        // reset player and enemy settings to initial values
        Controller.Player.Reset();

        round = 0;

        enemySettings = initialEnemySettings.DeepClone(enemySettings);
        cachedTimeInterval = maxSpawnTime;

        ResetValues();
        GameView.Instance.SetSlidersMax();
    }

    private void PlayTime()
    {
        Time.timeScale = 1;
    }

    private void PauseTime()
    {
        Time.timeScale = 0;
    }

    private void OnStartRound()
    {
        ResetTimer();
        ActivatePlayerControls();

        Controller.Player.ResetPosition();

        PlayTime();
        GameView.Instance.SetSlidersMax();

        AddRound();

        SpawnEnemies();
    }

    private void AddRound()
    {
        round++;
    }

    private void SpawnEnemies()
    {
        int currentRound = round;
        int enemiesToSpawn = currentRound; // same as the current round

        if (currentRound > 1)
        {
            cachedTimeInterval -= (cachedTimeInterval * spawnTimePercentDeduction);
        }
        cachedTimeInterval = Mathf.Clamp(cachedTimeInterval, minSpawnTime, maxSpawnTime);

        // max enemies to spawn at a given time
        // should be the number of spawnpoints available
        if (enemiesToSpawn > Controller.EnemySpawn.CountSpawnPoints)
        {
            enemiesToSpawn = Controller.EnemySpawn.CountSpawnPoints;
        }

        Debug.Log("Spawn Time Interval: " + cachedTimeInterval);
        Controller.EnemySpawn.SpawnSettings(cachedTimeInterval, enemiesToSpawn);
        Controller.EnemySpawn.StartSpawning();
    }

    #endregion
}