using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ASingleton<GameController>
{
    #region MVC
    public ControllerList Controller;

    [SerializeField]
    private GameView _view;

    public GameModel_SO Model;
    #endregion // MVC

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();

        //_view.InitViews();
    }
    private void Start()
    {
        InitGame();
    }

    private void OnEnable()
    {
        GameView.NextRound += NextRound;
        GameView.RestartGame += RestartGame;
        GameView.PauseGame += PauseGame;
        GameView.ResumeGame += ResumeGame;
        GameView.SaveHighScoreName += SaveHighScore;

        GameView.QuitGamePlay += GoToMainMenu;
        GameView.SetGameOver += GameOver;
        PlayerController.NoLives += GameOver;

        TimerController.StartRound += OnStartRound;
        TimerController.TimeEnd += OnTimeEnd;

        Controller.Enemy.ResetEnemySettings();
    }

    private void OnDisable()
    {

        GameView.NextRound -= NextRound;
        GameView.RestartGame -= RestartGame;
        GameView.PauseGame -= PauseGame;
        GameView.ResumeGame -= ResumeGame;
        GameView.SaveHighScoreName -= SaveHighScore;

        GameView.QuitGamePlay -= GoToMainMenu;
        GameView.SetGameOver -= GameOver;
        PlayerController.NoLives -= GameOver;

        TimerController.StartRound -= OnStartRound;
        TimerController.TimeEnd -= OnTimeEnd;
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

    public void InitGame()
    {
        _view.ShowHUD(true);
        RestartGame();
    }
    #endregion // Public Methods

    #region Implementation

    private void ResetTimer()
    {
        Controller.Timer.ResetTimer();
        Controller.Timer.StartTimer();
    }
    private void OnTimeEnd()
    {
        Controller.Enemy.StopSpawning();

        ShowShop();
        PauseTime();
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
        _view.InitViews();
        Controller.Enemy.StopSpawning();
        _view.ShowGameOverUI(true);

        string playerScoreText = Controller.Player.Score.ToString();
        string playerCoinsText = $"{Controller.Player.Coins} x {Controller.Player.CoinMultiplier}";
        int playerTotalScore = Controller.Player.TotalScore;
        string playerTotalScoreText = playerTotalScore.ToString();

        if (playerTotalScore >= PlayerPrefs.GetInt(PlayerPrefKeys.iHighScore))
        {
            _view.ShowGameOverPassBoard(true);
            _view.ShowInputHiScoreName(true);
            _view.ShowGameOverFailBoard(false); // hide fail
        }
        else
        {
            _view.ShowGameOverPassBoard(false); // hide pass
            _view.ShowGameOverFailBoard(true);
        }

        // show the player's scores
        _view.SetGameOverScore(playerScoreText, playerCoinsText, playerTotalScoreText);

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

    private void SaveHighScore(string name)
    {
        _view.ShowInputHiScoreName(false); // hide the input

        PlayerPrefs.SetString(PlayerPrefKeys.sHighScoreName, name);
        PlayerPrefs.SetInt(PlayerPrefKeys.iHighScore, Controller.Player.TotalScore);
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
        {
            //Destroy(oneObject);
            oneObject.SetActive(false);
        }
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
        Debug.Log("GameMaster Next Round");
        _view.InitViews();

        OnStartRoundTimer();
    }

    private void RestartGame()
    {
        Debug.Log("GameMaster Restart Game");
        // reset player and enemy settings to initial values
        Controller.Player.Reset();

        Model.ResetRound();

        Controller.Enemy.ResetEnemySettings();
        Model.CachedTimeInterval = Model.MaxSpawnTime;

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

    private void OnStartRoundTimer()
    {
        DeactivatePlayerControls();
        Controller.Player.ResetPosition();
        PlayTime();
        AddRound();
        ResetTimer();
        Controller.Timer.StartRoundTimer(Model.Round);
    }

    private void OnStartRound()
    {
        ActivatePlayerControls();

        GameView.Instance.SetSlidersMax();
        SpawnEnemies();
    }

    private void AddRound()
    {
        Model.Round++;
    }

    private void SpawnEnemies()
    {
        int currentRound = Model.Round;
        int enemiesToSpawn = currentRound; // same as the current round

        var cachedTime = Model.CachedTimeInterval;
        if (currentRound > 1)
        {
            cachedTime -= (cachedTime * Model.SpawnTimePercentDeduction);
        }
        Model.CachedTimeInterval = Mathf.Clamp(
            cachedTime,
            Model.MinSpawnTime,
            Model.MaxSpawnTime
            );

        // max enemies to spawn at a given time
        // should be the number of spawnpoints available
        if (enemiesToSpawn > Controller.Enemy.CountSpawnPoints)
        {
            enemiesToSpawn = Controller.Enemy.CountSpawnPoints;
        }

        Debug.Log("Spawn Time Interval: " + Model.CachedTimeInterval);
        Controller.Enemy.SpawnSettings(Model.CachedTimeInterval, enemiesToSpawn);
        Controller.Enemy.StartSpawning();
    }

    private void GoToMainMenu()
    {
        DeactivatePlayerControls();
        _view.InitViews();
        _view.ShowHUD(false);
        PlayTime();
        SceneManager.LoadScene("WelcomeScene");
    }

    #endregion
}