using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameView : ASingleton<GameView>
{
    #region Inspector Variables
    [Header("Panels")]
    [SerializeField]
    private GameObject uiGameOver;
    [SerializeField]
    private GameObject uiGamePaused;
    [SerializeField]
    private GameObject uiShop;

    [Header("Game Over - Buttons")]
    [SerializeField]
    private Button GOverQuitButton;
    [SerializeField]
    private Button GOverRestartButton;

    [Header("SHOP - Buttons")]
    [SerializeField]
    private Button ShopQuitButton;
    [SerializeField]
    private Button ShopPlayAgainButton;

    [Header("Pause Panel - Buttons")]
    [SerializeField]
    private Button FrozenResumeButton;

    [Header("HUD")]
    [SerializeField]
    private Button HUDPauseButton;

    [SerializeField]
    private TextMeshProUGUI Lives;
    [SerializeField]
    private TextMeshProUGUI Coins;
    [SerializeField]
    private TextMeshProUGUI Rockets;
    [SerializeField]
    private TextMeshProUGUI ShieldPoints;

    [SerializeField]
    private Slider ShieldSlider;
    [SerializeField]
    private Slider TimerSlider;

    #endregion

    #region Accessors
    public bool IsGameOverUIActive => uiGameOver.activeInHierarchy;
    public bool IsGamePausedUIActive => uiGamePaused.activeInHierarchy;
    public bool IsShopUIActive => uiShop.activeInHierarchy;
    #endregion

    #region Events

    public static event Action RestartScene;
    public static event Action RestartGame;
    public static event Action PauseGame;
    public static event Action ResumeGame;

    #endregion

    #region Unity Callbacks
    private void Start()
    {
        GOverQuitButton.onClick.AddListener(OnQuitGame);
        GOverRestartButton.onClick.AddListener(OnRestartGame);

        ShopQuitButton.onClick.AddListener(OnQuitGame);
        ShopPlayAgainButton.onClick.AddListener(OnPlayAgain);

        FrozenResumeButton.onClick.AddListener(OnResumeGame);

        HUDPauseButton.onClick.AddListener(OnPauseGame);

        SetSlidersMax();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            //Pause Game
            OnPauseGame();
        }

        // TODO separate this from Update
        Lives.text = GameController.Instance.Controller.Player.Life.ToString();
        Coins.text = GameController.Instance.Controller.Player.Coins.ToString();
        Rockets.text = GameController.Instance.Controller.Player.RocketCount.ToString();
        ShieldPoints.text = $"{Mathf.Round(GameController.Instance.Controller.Player.ShieldPoint)}" +
            $" / {GameController.Instance.Controller.Player.ShieldMax}";

        ShieldSlider.value = GameController.Instance.Controller.Player.ShieldPoint;
        TimerSlider.value = GameController.Instance.Controller.Timer.TimeLeft;
    }

    #endregion

    #region Public Methods
    public void InitViews()
    {
        uiGameOver.SetActive(false);
        uiGamePaused.SetActive(false);
        uiShop.SetActive(false);
    }

    public void SetSlidersMax()
    {
        TimerSlider.maxValue = GameController.Instance.Controller.Timer.TimePerRound;
        ShieldSlider.maxValue = GameController.Instance.Controller.Player.ShieldMax;
    }

    public void ShowGameOverUI(bool value)
    {
        uiGameOver.SetActive(value);
    }

    public void ShowGamePausedUI(bool value)
    {
        uiGamePaused.SetActive(value);
    }

    public void ShowShopUI(bool value)
    {
        uiShop.SetActive(value);
    }
    #endregion // Public Methods

    #region Button Actions
    private void OnQuitGame()
    {
        //End Game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }

    private void OnPlayAgain()
    {
        //Restart Scene
        RestartScene?.Invoke();
        // TODO LoadScene should be managed by the Controller
        SceneManager.LoadScene("GameplayScene");
    }

    private void OnRestartGame()
    {
        //Restart Game
        RestartGame?.Invoke();
        // TODO LoadScene should be managed by the Controller
        SceneManager.LoadScene("GameplayScene");
    }

    private void OnPauseGame()
    {
        PauseGame?.Invoke();
    }

    private void OnResumeGame()
    {
        ResumeGame?.Invoke();
    }

    #endregion
}
