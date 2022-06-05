using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : ControllerHelper
{
    #region MVC
    [SerializeField]
    private TimerRoundStartView _view_RoundStart;

    [SerializeField]
    private TimerModel_SO _model;
    #endregion // MVC

    #region Private Fields
    private bool hasStarted = false;
    #endregion

    #region Accessors
    public float TimeLeft => _model.TimeLeft;
    public float TimePerRound => _model.TimePerRound;
    #endregion // Accessors

    #region Unity Callbacks
    private void Update()
    {
        if (hasStarted)
        {
            TimerCountdown();
        }
    }
    #endregion // Unity Callbacks

    #region Public Methods
    public void ResetTimer()
    {
        _model.TimeLeft = _model.TimePerRound;
    }

    public void StartTimer()
    {
        hasStarted = true;
    }

    public void StopTimer()
    {
        hasStarted = false;
    }
    #endregion // Public Methods

    #region Implementation
    private void TimerCountdown()
    {
        _model.TimeLeft -= Time.deltaTime;
    }
    #endregion // Implementation
}
