using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : ASingleton<LevelLoader>
{
    public bool InitialTransition = true;
    [SerializeField]
    private Animator _transition;
    [SerializeField]
    private float _transitionTime = 1f;

    [SerializeField]
    private GameObject _loadingGroup;
    [SerializeField]
    private TextMeshProUGUI _loadingPercent;
    [SerializeField]
    private Slider _loadingSlider;

    public static event Action MidTransition;
    public static event Action TryInitGame;

    private void Start()
    {
        if (!InitialTransition)
        {
            _loadingGroup.SetActive(false);
            TryInitGame?.Invoke();
        }
    }

    public void LoadScene(int sceneIndex, bool initGameController)
    {
        StartCoroutine(C_TransitionToScene(sceneIndex, initGameController));
    }

    private IEnumerator C_TransitionToScene(int sceneIndex, bool initGameController)
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySFX(SFX.UITransition_Close);
        }

        // play the start transition (or the closing anim)
        // only if InitialTransition is false
        if (!InitialTransition)
        {
            _transition.SetTrigger("Start");


            // wait to complete
            yield return new WaitForSeconds(_transitionTime);
            MidTransition?.Invoke();
        }

        // load scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        _loadingGroup.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            int percentage = Mathf.RoundToInt(progress * 100f);

            _loadingSlider.value = progress;
            _loadingPercent.text = percentage + "%";
            yield return null;
        }

        if (operation.isDone)
        {
            _loadingGroup.SetActive(false);
            if (initGameController)
            {
                TryInitGame?.Invoke();
            }

            _transition.SetTrigger("End");
            if (AudioController.Instance != null)
            {
                AudioController.Instance.PlaySFX(SFX.UITransition_Open);
            }
        }

        InitialTransition = false;
    }
}
