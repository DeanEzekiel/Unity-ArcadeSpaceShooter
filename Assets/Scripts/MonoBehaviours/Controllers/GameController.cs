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
        //InitGame();
    }

    private void OnEnable()
    {
        GameView.NextRound += NextRound;
        GameView.RestartGame += RestartGame;
        GameView.ResumeGame += ResumeGame;
        GameView.SaveHighScoreName += SaveHighScore;

        GameView.QuitGamePlay += GoToMainMenu;
        GameView.SetGameOver += GameOver;
        PlayerController.NoLives += GameOver;
        PlayerController.PausePressed += PauseGame;

        TimerController.StartRound += OnStartRound;
        TimerController.TimeEnd += OnTimeEnd;
        TimerController.EndRound += ShowShop;

        LevelLoader.MidTransition += MidTransition;
        LevelLoader.TryInitGame += InitGame;

        Controller.Enemy.ResetEnemySettings();
    }

    private void OnDisable()
    {

        GameView.NextRound -= NextRound;
        GameView.RestartGame -= RestartGame;
        GameView.ResumeGame -= ResumeGame;
        GameView.SaveHighScoreName -= SaveHighScore;

        GameView.QuitGamePlay -= GoToMainMenu;
        GameView.SetGameOver -= GameOver;
        PlayerController.NoLives -= GameOver;
        PlayerController.PausePressed -= PauseGame;

        TimerController.StartRound -= OnStartRound;
        TimerController.TimeEnd -= OnTimeEnd;
        TimerController.EndRound -= ShowShop;

        LevelLoader.MidTransition -= MidTransition;
        LevelLoader.TryInitGame -= InitGame;
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

        // Log the Round
        Services.Instance.Analytics.SetCustomEvent(
            AnalyticsKeys.eRoundCleared,
            AnalyticsKeys.pRound,
            Model.Round);

        DeactivateControlsAndEnemies();
        //ShowShop();
        //PauseTime();
    }
    private void DeactivateControlsAndEnemies()
    {
        Controller.Player.ShowOnscreenControls(false);
        //disable player controls while shop is active or just use a bool
        DeactivatePlayerControls();

        //remove all aliens, asteroids and projectiles
        DeactivateObjWithTag(Tags.Asteroid);
        DeactivateObjWithTag(Tags.Alien);
        DeactivateObjWithTag(Tags.PlayerProjectile);
        DeactivateObjWithTag(Tags.Rocket);
        DeactivateObjWithTag(Tags.AlienProjectile);
        DeactivateObjWithTag(Tags.Coin);
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
        AudioController.Instance.StopBGM();
        AudioController.Instance.PlaySFX(SFX.GameOver);
        // Log Game Over
        Services.Instance.Analytics.SetCustomEvent(
            AnalyticsKeys.eGameOver,
            AnalyticsKeys.pRound,
            Model.Round);

        
        _view.InitViews();
        _view.ShowHUD(false);
        Controller.Enemy.StopSpawning();
        _view.ShowGameOverUI(true);

        string playerScoreText = Controller.Player.Score.ToString();
        string playerCoinsText = $"{Controller.Player.Coins} x {Controller.Player.CoinMultiplier}";
        int playerTotalScore = Controller.Player.TotalScore;
        string playerTotalScoreText = playerTotalScore.ToString();

        if (playerTotalScore > PlayerPrefs.GetInt(PlayerPrefKeys.iHighScore))
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
        DeactivateControlsAndEnemies();

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
        _view.ShowHUD(false);
        _view.ShowShopUI(true);
        AudioController.Instance.PlayBGM(BGM.Shop, true);

        Controller.Shop.UpdateViewTexts();
        Controller.Shop.CheckAdItems();
        Controller.Shop.CheckMaxAllowed();
    }
    private void DeactivateObjWithTag(string tag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in destroyObject)
        {
            //Destroy(oneObject);
            obj.SetActive(false);
        }
    }

    private void PauseGame()
    {
        Debug.Log("GameMaster Pause Game");
        if (!_view.IsGameOverUIActive && !_view.IsShopUIActive)
        {
            PauseTime();
            DeactivatePlayerControls();
            Controller.Player.ShowOnscreenControls(false);
            _view.ShowGamePausedUI(true);

            Controller.RandomNote.GetNote();
        }
    }

    private void ResumeGame()
    {
        Debug.Log("GameMaster Resume Game");
        PlayTime();
        ActivatePlayerControls();
        Controller.Player.ShowOnscreenControls(true);
        _view.ShowGamePausedUI(false);

        Controller.RandomNote.Abort();
    }

    private void NextRound()
    {
        Debug.Log("GameMaster Next Round");
        _view.InitViews();
        _view.ShowHUD(true);

        DeactivatePlayerControls();
        Controller.Player.ShowOnscreenControls(true);
        Controller.Player.ResetPosition();
        AddRound();

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
        PlayTime();
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
        //_view.InitViews(); // should wait for the MidTransition before removing the views
        _view.ShowHUD(false);
        PlayTime();
        //SceneManager.LoadScene("WelcomeScene");
        LevelLoader.Instance.LoadScene(0, false);
    }

    private void MidTransition()
    {
        _view.InitViews();
    }

    #endregion
}