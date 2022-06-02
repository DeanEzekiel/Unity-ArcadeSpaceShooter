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

    public bool PlayerControlsActive { get; private set; } = true;

    public GameSettings gameSettings;
    public PlayerSettings playerSettings;
    public EnemySettings enemySettings;

    [SerializeField]
    private PlayerSettings initialPlayerSettings;
    [SerializeField]
    private EnemySettings initialEnemySettings;
    #endregion // Fields

    #region Events

    public static event Action ShieldToggle;
    public static event Action ResetPlayerPosition;

    #endregion //Events

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();

        GameOverUI.SetActive(false);
        GamePausedUI.SetActive(false);
        ShopUI.SetActive(false);

        //playerSettings = initialPlayerSettings.DeepClone(playerSettings);
        //enemySettings = initialEnemySettings.DeepClone(enemySettings);
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

        if(playerSettings.life == 0)
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

        playerSettings = initialPlayerSettings.DeepClone(playerSettings);
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

    #region Implementation
    private void ShieldPointsCalc()
    {
        if (playerSettings.shieldOn && playerSettings.shieldPoint > 0)
        {
            playerSettings.shieldPoint -= (playerSettings.shieldDepletePnt /
                playerSettings.shieldDepleteTime * Time.deltaTime);
        }
        else if (!playerSettings.shieldOn && playerSettings.shieldPoint <
            playerSettings.shieldMax)
        {
            playerSettings.shieldPoint += (playerSettings.shieldReplenishPnt /
                playerSettings.shieldReplenishTime * Time.deltaTime);
        }

        if (playerSettings.shieldPoint > playerSettings.shieldMax)
            playerSettings.shieldPoint = playerSettings.shieldMax;
        else if (playerSettings.shieldPoint <= 0)
        {
            playerSettings.shieldPoint = 0;
            ShieldToggle?.Invoke(); //this will activate/deactivate the shield
        }
    }

    private void ResetTimer()
    {
        playerSettings.timer = playerSettings.timePerRound;
    }
    private void TimerCountdown()
    {
        playerSettings.timer -= Time.deltaTime;

        if (playerSettings.timer <= 0 && playerSettings.life >= 1)
        {
            ShowShop();
            PauseTime();
        }
    }
    private void ActivatePlayerControls()
    {
        PlayerControlsActive = true;
    }
    private void DeactivatePlayerControls()
    {
        PlayerControlsActive = false;
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

        ResetTimer();
        ActivatePlayerControls();
        ResetPlayerPosition?.Invoke();

        PlayTime();
        GameView.Instance.SetSlidersMax();
    }

    private void RestartGame()
    {
        Debug.Log("GameMaster Restart Game");
        // reset player and enemy settings to initial values
        playerSettings = initialPlayerSettings.DeepClone(playerSettings);
        enemySettings = initialEnemySettings.DeepClone(enemySettings);

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

    #endregion
}