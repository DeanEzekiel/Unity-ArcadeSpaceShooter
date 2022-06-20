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
    // Start is called before the first frame update
    void Start()
    {
        pnlBanner.SetActive(true);
        pnlHome.SetActive(true);
        pnlHelp.SetActive(false);

        btnHelp.onClick.AddListener(ShowHelp);
        btnHome.onClick.AddListener(ShowHome);
        btnPlayNow.onClick.AddListener(PlayNow);
        btnExitGame.onClick.AddListener(ExitGame);

        CheckHighScore();
    }

    private void ShowHelp()
    {
        pnlHome.SetActive(false);
        pnlHelp.SetActive(true);

        brdDefault.SetActive(false);
    }

    private void ShowHome()
    {
        pnlHome.SetActive(true);
        pnlHelp.SetActive(false);
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
        //SceneManager.LoadScene("GameplayScene");
        LevelLoader.Instance.LoadScene(1, true);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}
