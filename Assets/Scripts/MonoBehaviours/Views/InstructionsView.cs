using System;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsView : MonoBehaviour
{
    [SerializeField]
    private GameObject _instructionBoard;
    [SerializeField]
    private GameObject _activeHighlight;

    private Button _theButton;

    #region Events
    public static event Action UpdateActive;
    #endregion // Events

    #region Unity Callbacks
    private void Start()
    {
        _theButton = GetComponent<Button>();

        _theButton.onClick.AddListener(ButtonClick);
    }

    private void OnEnable()
    {
        UpdateActive += DeactivateLastItem;
        WelcomeView.OpenInstructions += DeactivateLastItem;
    }

    private void OnDisable()
    {
        UpdateActive -= DeactivateLastItem;
        WelcomeView.OpenInstructions -= DeactivateLastItem;
    }
    #endregion // Unity Callbacks

    #region Implementation
    private void ButtonClick()
    {
        UpdateActive?.Invoke();
        PlayClickSFX();
        ActivateSelectedItem();
    }

    private void PlayClickSFX()
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySFX(SFX.UIClick);
        }
    }

    private void DeactivateLastItem()
    {
        if (_activeHighlight.activeInHierarchy)
        {
            _activeHighlight.SetActive(false);
            _instructionBoard.SetActive(false);
        }
    }

    private void ActivateSelectedItem()
    {
        _instructionBoard.SetActive(true);
        _activeHighlight.SetActive(true);
    }
    #endregion // Implementation
}