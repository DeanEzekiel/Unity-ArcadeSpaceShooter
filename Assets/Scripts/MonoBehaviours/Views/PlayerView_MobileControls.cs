using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView_MobileControls : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private RectTransform _joystick;
    [SerializeField]
    private Image _shootButton;
    [SerializeField]
    private Image _blastButton;
    [SerializeField]
    private Image _guardButton;
    [SerializeField]
    private Image _dashButton;

    [SerializeField]
    private GameObject _shootIcon;
    [SerializeField]
    private GameObject _blastIcon;
    [SerializeField]
    private GameObject _guardIcon;
    [SerializeField]
    private GameObject _dashIcon;

    [SerializeField]
    private GameObject _shootKey;
    [SerializeField]
    private GameObject _blastKey;
    [SerializeField]
    private GameObject _guardKey;
    [SerializeField]
    private GameObject _dashKey;
    #endregion // Inspector Fields

    #region Private Fields
    private float _alphaFull = 1f;
    private float _alphaHalf = 0.5f;
    #endregion // Private Fields

    #region Public Methods
    public void ShowKeyTexts(bool value)
    {
        _shootKey.SetActive(value);
        _blastKey.SetActive(value);
        _guardKey.SetActive(value);
        _dashKey.SetActive(value);
    }

    public void ResetJoystickPos()
    {
        //_joystick.position = Vector2.zero;
        _joystick.localPosition = Vector2.zero;
    }

    public void BlastInteractable(bool value)
    {
        Color tempColor = _blastButton.color;
        if (value)
        {
            tempColor.a = _alphaFull; // FULL ALPHA
            _blastButton.color = tempColor;

            _blastIcon.SetActive(true);
        }
        else
        {
            tempColor.a = _alphaHalf; // HALF ALPHA
            _blastButton.color = tempColor;

            _blastIcon.SetActive(false);
        }
    }

    public void GuardInteractable(bool value)
    {
        Color tempColor = _guardButton.color;
        if (value)
        {
            tempColor.a = _alphaFull; // FULL ALPHA
            _guardButton.color = tempColor;

            _guardIcon.SetActive(true);
        }
        else
        {
            tempColor.a = _alphaHalf; // HALF ALPHA
            _guardButton.color = tempColor;

            _guardIcon.SetActive(false);
        }
    }
    #endregion // Public Methods
}
