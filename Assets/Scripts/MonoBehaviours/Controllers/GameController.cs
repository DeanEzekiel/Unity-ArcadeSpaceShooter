using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : ASingleton<GameController>
{
    #region MVC
    public ControllerList Controller;

    [SerializeField]
    private GameView _view;

    public GameModel_SO GameModel;
    #endregion // MVC

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();

        _view.InitViews();
    }
    private void Start()
    {
        GameModel.CachedTimeInterval = GameModel.MaxSpawnTime;
        Controller.Player.Reset();
        GameModel.ResetRound();
        OnStartRound();
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
        GameView.NextRound += NextRound;
        GameView.RestartGame += RestartGame;
        GameView.PauseGame += PauseGame;
        GameView.ResumeGame += ResumeGame;

        Controller.Enemy.ResetEnemySettings();
    }

    private void OnDisable()
    {

        GameView.NextRound -= NextRound;
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
            Controller.Enemy.StopSpawning();

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
        Controller.Enemy.StopSpawning();
        _view.ShowGameOverUI(true);

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
        _view.ShowShopUI(true);

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
        if (!_view.IsGameOverUIActive && !_view.IsShopUIActive)
        {
            PauseTime();
            DeactivatePlayerControls();
            _view.ShowGamePausedUI(true);
        }
    }

    private void ResumeGame()
    {
        Debug.Log("GameMaster Resume Game");
        PlayTime();
        ActivatePlayerControls();
        _view.ShowGamePausedUI(false);
    }

    private void NextRound()
    {
        Debug.Log("GameMaster Reset Values");
        _view.InitViews();

        OnStartRound();
    }

    private void RestartGame()
    {
        Debug.Log("GameMaster Restart Game");
        // reset player and enemy settings to initial values
        Controller.Player.Reset();

        GameModel.ResetRound();

        Controller.Enemy.ResetEnemySettings();
        GameModel.CachedTimeInterval = GameModel.MaxSpawnTime;

        NextRound();
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
        GameModel.Round++;
    }

    private void SpawnEnemies()
    {
        int currentRound = GameModel.Round;
        int enemiesToSpawn = currentRound; // same as the current round

        var cachedTime = GameModel.CachedTimeInterval;
        if (currentRound > 1)
        {
            cachedTime -= (cachedTime * GameModel.SpawnTimePercentDeduction);
        }
        GameModel.CachedTimeInterval = Mathf.Clamp(
            cachedTime,
            GameModel.MinSpawnTime,
            GameModel.MaxSpawnTime
            );

        // max enemies to spawn at a given time
        // should be the number of spawnpoints available
        if (enemiesToSpawn > Controller.Enemy.CountSpawnPoints)
        {
            enemiesToSpawn = Controller.Enemy.CountSpawnPoints;
        }

        Debug.Log("Spawn Time Interval: " + GameModel.CachedTimeInterval);
        Controller.Enemy.SpawnSettings(GameModel.CachedTimeInterval, enemiesToSpawn);
        Controller.Enemy.StartSpawning();
    }

    #endregion
}