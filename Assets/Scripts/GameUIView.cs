using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIView : MonoBehaviour
{
    #region Inspector Variables
    [Header("Buttons")]
    [SerializeField]
    private Button quitGameButton;
    [SerializeField]
    private Button restartGameButton;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        quitGameButton.onClick.AddListener(OnQuitGame);
        restartGameButton.onClick.AddListener(OnRestartGame);
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

    private void OnRestartGame()
    {
        //Restart Scene
        SceneManager.LoadScene("SampleScene");
    }
    #endregion
}
