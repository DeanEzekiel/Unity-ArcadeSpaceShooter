using System;
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

    #region Events
    public static event Action StartRound;
    #endregion // Events

    #region Private Fields
    private bool hasRoundStarted = false;

    private int _roundCountdown;
    #endregion

    #region Accessors
    public float TimeLeft => _model.TimeLeft;
    public float TimePerRound => _model.TimePerRound;
    #endregion // Accessors

    #region Unity Callbacks
    private void Update()
    {
        if (hasRoundStarted)
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
        hasRoundStarted = true;
    }

    public void StopTimer()
    {
        hasRoundStarted = false;
    }

    public void StartRoundTimer(int round)
    {
        _view_RoundStart.SetTextRound(round);
        StartCoroutine(C_RoundStartCountdown());
    }
    #endregion // Public Methods

    #region Implementation
    private void TimerCountdown()
    {
        _model.TimeLeft -= Time.deltaTime;
    }

    private IEnumerator C_RoundStartCountdown()
    {
        _roundCountdown = _model.RoundCountdownSec;
        _view_RoundStart.ShowCountdown(true);
        hasRoundStarted = false;

        do
        {
            _view_RoundStart.SetTextCountdown(_roundCountdown);

            _roundCountdown--;
            yield return new WaitForSeconds(1);
        } while (_roundCountdown > 0);

        if (_roundCountdown <= 0)
        {
            hasRoundStarted = true;
            _view_RoundStart.ShowCountdown(false);
            StartRound?.Invoke();
        }
    }
    #endregion // Implementation
}
