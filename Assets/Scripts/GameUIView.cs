using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIView : MonoBehaviour
{
    #region Singleton Instance

    private static readonly object s_lock = new object();
    private static GameUIView s_instance;
    public static GameUIView Instance
    {
        get
        {
            lock (s_lock)
            {
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<GameUIView>();
                }

                return s_instance;
            }
        }
    }

    #endregion

    #region Singleton Init

    private void InitSingleton()
    {
        if (Instance.GetInstanceID() != GetInstanceID())
        {
            Debug.LogWarning($"Cannot have more than 1 UI Canvas. Destroying {gameObject.name}", gameObject);
            Destroy(gameObject);
        }
    }

    #endregion

    #region Inspector Variables
    [Header("GameOver - Buttons")]
    [SerializeField]
    private Button GOverQuitButton;
    [SerializeField]
    private Button GOverRestartButton;

    [Header("SHOP - Buttons")]
    [SerializeField]
    private Button ShopQuitButton;
    [SerializeField]
    private Button ShopPlayAgainButton;

    [Header("Frozen - Buttons")]
    [SerializeField]
    private Button FrozenResumeButton;

    [Header("HUD")]
    [SerializeField]
    private Button HUDPauseButton;

    [SerializeField]
    private TextMeshProUGUI Lives;
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
    private void Awake()
    {
        InitSingleton();
        DontDestroyOnLoad(gameObject);

        GOverQuitButton.onClick.AddListener(OnQuitGame);
        GOverRestartButton.onClick.AddListener(OnRestartGame);

        ShopQuitButton.onClick.AddListener(OnQuitGame);
        ShopPlayAgainButton.onClick.AddListener(OnPlayAgain);

        FrozenResumeButton.onClick.AddListener(OnResumeGame);

        HUDPauseButton.onClick.AddListener(OnPauseGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            //Pause Game
            OnPauseGame();
        }

        Lives.text = GameController.Instance.Life.ToString();
        Rockets.text = GameController.Instance.RocketCnt.ToString();
        ShieldPoints.text = $"{Mathf.Round(GameController.Instance.ShieldPnt)} / 100";

        ShieldSlider.value = GameController.Instance.ShieldPnt;
        TimerSlider.value = GameController.Instance.Timer;
    }

    #endregion

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
        SceneManager.LoadScene("SampleScene");
    }

    private void OnRestartGame()
    {
        //Restart Game
        RestartGame?.Invoke();
        SceneManager.LoadScene("SampleScene");
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
