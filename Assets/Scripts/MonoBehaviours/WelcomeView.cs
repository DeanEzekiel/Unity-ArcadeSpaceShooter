using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    private void PlayNow()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
