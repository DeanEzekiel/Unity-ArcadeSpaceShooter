using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class WelcomeView : MonoBehaviour
{
    #region Fields

    [Header("Panels")]
    [SerializeField]
    private GameObject pnlBanner;
    [SerializeField]
    private GameObject pnlHome;
    [SerializeField]
    private GameObject pnlHelp;

    [Header("Home UI")]
    [SerializeField]
    private Button btnHelp;
    [SerializeField]
    private Button btnPlayNow;
    [SerializeField]
    private Button btnExitGame;
    [Space]
    [SerializeField]
    private Image imgBoard;
    [SerializeField]
    private TextMeshProUGUI txtHighScore;

    [Header("Instructions UI")]
    [SerializeField]
    private Button btnHome;
    [SerializeField]
    private GameObject brdHelp;
    [SerializeField]
    private TextMeshProUGUI txtHelp;

    [SerializeField]
    private GameObject brdDefault;

    [Header("Volume Control")]
    [SerializeField]
    private Button btnSoundSettings;
    [SerializeField]
    private GameObject pnlSoundSettings;
    [SerializeField]
    private Slider sldBGM;
    [SerializeField]
    private Slider sldSFX;


    [Header("Texts")]
    [SerializeField]
    private string NoHighScore = "NO HIGH SCORE AT THE MOMENT.";
    [SerializeField]
    private string WithHighScore = "HIGH SCORE:<br>[Name]: [Score]<br><br>CAN YOU BEAT THIS?";
    [SerializeField]
    private string NamePlaceholder = "[Name]";
    [SerializeField]
    private string ScorePlaceholder = "[Score]";


    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        pnlBanner.SetActive(true);
        pnlHome.SetActive(true);
        pnlHelp.SetActive(false);

        btnHelp.gameObject.SetActive(true);
        btnHome.gameObject.SetActive(false);
    }
    void Start()
    {
        btnHelp.onClick.AddListener(ShowHelp);
        btnHome.onClick.AddListener(ShowHome);
        btnPlayNow.onClick.AddListener(PlayNow);
        btnExitGame.onClick.AddListener(ExitGame);

        btnSoundSettings.onClick.AddListener(ShowSoundSettings);
        sldBGM.onValueChanged.AddListener(delegate { BGMSliderChange(); });
        sldSFX.onValueChanged.AddListener(delegate { SFXSliderChange(); });
        pnlSoundSettings.SetActive(false);

        CheckHighScore();
    }

    private void OnEnable()
    {
        AudioController.ReflectBGMValue += UpdateBGM;
        AudioController.ReflectSFXValue += UpdateSFX;
    }

    private void OnDisable()
    {
        AudioController.ReflectBGMValue -= UpdateBGM;
        AudioController.ReflectSFXValue -= UpdateSFX;
    }

    #endregion // Unity Callbacks

    #region Sound Implementation
    /// <summary>
    /// Called on initialize of the Audio Controller.
    /// </summary>
    /// <param name="value">Value of the Player Prefs BGM Volume</param>
    private void UpdateBGM(float value)
    {
        sldBGM.value = value;
        PlayMenuBGM();
    }

    /// <summary>
    /// Called on initialize of the Audio Controller.
    /// </summary>
    /// <param name="value">Value of the Player Prefs SFX Volume</param>
    private void UpdateSFX(float value)
    {
        sldSFX.value = value;
    }

    private void ShowSoundSettings()
    {
        PlayClickSFX();
        if (pnlSoundSettings.activeInHierarchy)
        {
            pnlSoundSettings.SetActive(false);
        }
        else
        {
            pnlSoundSettings.SetActive(true);
        }
    }

    private void SFXSliderChange()
    {
        AudioController.Instance.UpdateSFXVolume(sldSFX.value);
    }

    private void BGMSliderChange()
    {
        AudioController.Instance.UpdateBGMVolume(sldBGM.value);
    }
    #endregion // Sound Implementation

    #region Implementation
    private void ShowHelp()
    {
        PlayClickSFX();
        pnlHome.SetActive(false);
        pnlHelp.SetActive(true);

        brdDefault.SetActive(false);

        btnHelp.gameObject.SetActive(false);
        btnHome.gameObject.SetActive(true);
    }

    private void ShowHome()
    {
        PlayClickSFX();
        pnlHome.SetActive(true);
        pnlHelp.SetActive(false);

        btnHelp.gameObject.SetActive(true);
        btnHome.gameObject.SetActive(false);
    }

    private void CheckHighScore()
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.iHighScore) > 0)
        {
            var text = WithHighScore;
            text = text.Replace(NamePlaceholder, PlayerPrefs.GetString(PlayerPrefKeys.sHighScoreName));
            text = text.Replace(ScorePlaceholder, PlayerPrefs.GetInt(PlayerPrefKeys.iHighScore).ToString());
            txtHighScore.text = text;
        }
        else
        {
            txtHighScore.text = NoHighScore;
        }
    }

    private void PlayNow()
    {
        PlayClickSFX();
        //SceneManager.LoadScene("GameplayScene");
        LevelLoader.Instance.LoadScene(2, true);
    }

    private void ExitGame()
    {
        PlayClickSFX();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }

    private void PlayMenuBGM()
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlayBGM(BGM.MainMenu, false);
        }
    }
    #endregion // Implementation

    #region Public Methods
    public void PlayClickSFX()
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySFX(SFX.UIClick);
        }
    }
    #endregion // Public Methods
}
