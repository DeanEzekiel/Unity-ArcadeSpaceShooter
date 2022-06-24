using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Crosstales.BWF.Manager;

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
    [SerializeField]
    private GameObject uiHUD;
    [SerializeField]
    private GameObject uiBG;
    [Space]
    [SerializeField]
    private GameObject uiGOverNewHiBoard;
    [SerializeField]
    private GameObject uiGOverFailedBoard;
    [SerializeField]
    private GameObject uiFXNewHighScore;
    [SerializeField]
    private GameObject uiFXFailed;

    [Header("Gamer Over - Player Score")]
    [SerializeField]
    private TextMeshProUGUI playerScore;
    [SerializeField]
    private TextMeshProUGUI playerCoins;
    [SerializeField]
    private TextMeshProUGUI playerTotalScore;

    [Header("Gamer Over - Player High Score")]
    [SerializeField]
    private TMP_InputField playerNameInput;
    [SerializeField]
    private TextMeshProUGUI playerNameHolder;

    [Header("Gamer Over - Player Failed")]
    [SerializeField]
    private TextMeshProUGUI currentHiScoreName;
    [SerializeField]
    private TextMeshProUGUI currentHiScore;

    [Header("Game Over - Buttons")]
    [SerializeField]
    private Button GOverQuitButton;
    [SerializeField]
    private Button GOverRestartButton;
    [SerializeField]
    private Button GOverSaveHighScore;

    [Header("SHOP - Buttons")]
    [SerializeField]
    private Button ShopQuitButton;
    [SerializeField]
    private Button ShopNextRoundButton;

    [Header("Pause Panel - Buttons")]
    [SerializeField]
    private Button FrozenResumeButton;

    [Header("HUD")]
    [SerializeField]
    private TextMeshProUGUI Lives;
    [SerializeField]
    private TextMeshProUGUI Coins;
    [SerializeField]
    private TextMeshProUGUI Rockets;
    [SerializeField]
    private TextMeshProUGUI ShieldPoints;

    [SerializeField]
    private TextMeshProUGUI Score;

    [SerializeField]
    private Slider ShieldSlider;
    [SerializeField]
    private Slider TimerSlider;

    [Header("Score Texts")]
    [SerializeField]
    private string _txtScore = "Current Score: ";
    [SerializeField]
    private string _txtCoins = "Coins Left: ";
    [SerializeField]
    private string _txtTotal = "Total Score: ";

    [Header("Bad Word Filter")]
    [SerializeField]
    private List<string> languages = new List<string>();

    #endregion

    #region Accessors
    public bool IsGameOverUIActive => uiGameOver.activeInHierarchy;
    public bool IsGamePausedUIActive => uiGamePaused.activeInHierarchy;
    public bool IsShopUIActive => uiShop.activeInHierarchy;
    #endregion

    #region Events

    public static event Action NextRound;
    public static event Action RestartGame;
    public static event Action ResumeGame;

    public static event Action SetGameOver;
    public static event Action QuitGamePlay;

    public static event Action<string> SaveHighScoreName;

    #endregion

    #region Unity Callbacks
    private void Start()
    {
        GOverQuitButton.onClick.AddListener(OnQuitGame);
        GOverRestartButton.onClick.AddListener(OnRestartGame);

        GOverSaveHighScore.onClick.AddListener(OnSaveHighScore);

        ShopQuitButton.onClick.AddListener(OnQuitGame);
        ShopNextRoundButton.onClick.AddListener(OnNextRound);

        FrozenResumeButton.onClick.AddListener(OnResumeGame);

        SetSlidersMax();
    }

    private void Update()
    {
        if (uiGameOver.activeInHierarchy)
        {
            if (Time.frameCount % 5 == 0)
            {
                if (BadWordManager.Contains(playerNameInput.text, languages.ToArray())
                    || string.IsNullOrEmpty(playerNameInput.text))
                {
                    GOverSaveHighScore.interactable = false;
                }
                else
                {
                    GOverSaveHighScore.interactable = true;
                }
            }
        }

        // TODO separate this from Update
        Lives.text = GameController.Instance.Controller.Player.Life.ToString();
        Coins.text = GameController.Instance.Controller.Player.Coins.ToString();
        Rockets.text = GameController.Instance.Controller.Player.RocketCount.ToString();
        ShieldPoints.text = $"{Mathf.Round(GameController.Instance.Controller.Player.ShieldPoint)}" +
            $" / {GameController.Instance.Controller.Player.ShieldMax}";
        Score.text = GameController.Instance.Controller.Player.Score.ToString();

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
        uiBG.SetActive(false);
    }

    public void SetSlidersMax()
    {
        TimerSlider.maxValue = GameController.Instance.Controller.Timer.TimePerRound;
        ShieldSlider.maxValue = GameController.Instance.Controller.Player.ShieldMax;
    }

    public void ShowGameOverUI(bool value)
    {
        uiGameOver.SetActive(value);
        uiBG.SetActive(value);
    }

    public void ShowGameOverPassBoard(bool value)
    {
        uiGOverNewHiBoard.SetActive(value);
        uiFXNewHighScore.SetActive(value);
    }

    public void ShowInputHiScoreName(bool value)
    {
        print($"Show Inpur for High Score: {value}");
        playerNameInput.gameObject.SetActive(value);
        GOverSaveHighScore.gameObject.SetActive(value);

        playerNameHolder.gameObject.SetActive(!value); // flip value
    }

    public void SetGameOverScore(string scoreEarned, string coinsLeft, string total)
    {
        currentHiScoreName.text = PlayerPrefs.GetString(PlayerPrefKeys.sHighScoreName);
        currentHiScore.text = PlayerPrefs.GetInt(PlayerPrefKeys.iHighScore).ToString();

        playerScore.text = _txtScore + scoreEarned;
        playerCoins.text = _txtCoins + coinsLeft;
        playerTotalScore.text = _txtTotal + total;
    }

    public void ShowGameOverFailBoard(bool value)
    {
        uiGOverFailedBoard.SetActive(value);
        uiFXFailed.SetActive(value);
    }

    public void ShowGamePausedUI(bool value)
    {
        uiGamePaused.SetActive(value);
        uiBG.SetActive(value);
    }

    public void ShowShopUI(bool value)
    {
        uiShop.SetActive(value);
        uiBG.SetActive(value);
    }

    public void ShowHUD(bool value)
    {
        uiHUD.SetActive(value);
    }
    #endregion // Public Methods

    #region Button Actions
    private void OnQuitGame()
    {
        AudioController.Instance.PlaySFX(SFX.UIClick);
        if (IsGameOverUIActive)
        {
            // go to Main Menu
            QuitGamePlay?.Invoke();
        }
        else
        {
            // set to Game Over
            SetGameOver?.Invoke();
        }
    }

    private void OnNextRound()
    {
        AudioController.Instance.PlaySFX(SFX.UIClick);
        //Restart Scene
        NextRound?.Invoke();
        // TODO LoadScene should be managed by the Controller
        //SceneManager.LoadScene("GameplayScene");
    }

    private void OnRestartGame()
    {
        AudioController.Instance.PlaySFX(SFX.UIClick);
        //Restart Game
        RestartGame?.Invoke();
        // TODO LoadScene should be managed by the Controller
        //SceneManager.LoadScene("GameplayScene");
    }

    private void OnSaveHighScore()
    {
        AudioController.Instance.PlaySFX(SFX.UIClick);
        if (!string.IsNullOrEmpty(playerNameInput.text))
        {
            playerNameHolder.text = playerNameInput.text;
            SaveHighScoreName?.Invoke(playerNameInput.text);
        }
    }

    private void OnResumeGame()
    {
        AudioController.Instance.PlaySFX(SFX.UIClick);
        ResumeGame?.Invoke();
    }

    #endregion
}
