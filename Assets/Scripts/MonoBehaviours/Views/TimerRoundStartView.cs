using TMPro;
using UnityEngine;

public class TimerRoundStartView : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private GameObject _roundCountdownPanel;
    [SerializeField]
    private TextMeshProUGUI _txtCountdown;
    [SerializeField]
    private TextMeshProUGUI _txtRound;

    [SerializeField]
    private string _textMessage = "Round [x] starts in";
    [SerializeField]
    private string _roundVar = "[x]";
    #endregion // Inspector Fields

    #region Public Methods
    public void ShowCountdown(bool value)
    {
        _roundCountdownPanel.SetActive(value);
    }

    public void SetTextCountdown(int sec)
    {
        _txtCountdown.text = sec.ToString();
    }

    public void SetTextRound(int round)
    {
        _txtRound.text = _textMessage.Replace(_roundVar, round.ToString());
    }
    #endregion // Public Methods
}
