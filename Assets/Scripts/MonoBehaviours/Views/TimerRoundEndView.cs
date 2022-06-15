using TMPro;
using UnityEngine;

public class TimerRoundEndView : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private GameObject _roundEndPanel;
    [SerializeField]
    private GameObject _uiBG;
    [SerializeField]
    private TextMeshProUGUI _txtRound;

    [SerializeField]
    private string _textMessage = "Round [x]";
    [SerializeField]
    private string _roundVar = "[x]";
    #endregion // Inspector Fields

    #region Public Methods
    public void ShowCountdown(bool value)
    {
        _roundEndPanel.SetActive(value);
        _uiBG.SetActive(value);
    }

    public void SetTextRound(int round)
    {
        _txtRound.text = _textMessage.Replace(_roundVar, round.ToString());
    }
    #endregion // Public Methods
}
