using TMPro;
using UnityEngine;

public class RandomNoteView : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private TextMeshProUGUI _banner;
    [SerializeField]
    private TMP_InputField _inputField;
    #endregion // Fields

    #region Public Methods
    public void SetBanner(string banner)
    {
        _banner.text = banner;
    }
    public void ShowMessage(string message)
    {
        _inputField.text = message;
    }
    #endregion // Public Methods
}
