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
        Lives.text = GameMaster.Instance.Controller.Player.Life.ToString();
        Coins.text = GameMaster.Instance.Controller.Player.Coins.ToString();
        Rockets.text = GameMaster.Instance.Controller.Player.RocketCount.ToString();
        ShieldPoints.text = $"{Mathf.Round(GameMaster.Instance.Controller.Player.ShieldPoint)}" +
            $" / {GameMaster.Instance.Controller.Player.ShieldMax}";

        ShieldSlider.value = GameMaster.Instance.Controller.Player.ShieldPoint;
        TimerSlider.value = GameMaster.Instance.Controller.Timer.TimeLeft;
    }

    #endregion

    public void SetSlidersMax()
    {
        TimerSlider.maxValue = GameMaster.Instance.Controller.Timer.TimePerRound;
        ShieldSlider.maxValue = GameMaster.Instance.Controller.Player.ShieldMax;
    }

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

    #region Events

    public static event Action RestartScene;
    public static event Action RestartGame;
    public static event Action PauseGame;
    public static event Action ResumeGame;

    #endregion
}
