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
    private TimerRoundEndView _view_RoundEnd;

    [SerializeField]
    private TimerModel_SO _model;
    #endregion // MVC

    #region Events
    public static event Action StartRound;
    public static event Action TimeEnd;
    public static event Action EndRound;
    #endregion // Events

    #region Private Fields
    private bool hasRoundStarted = false;

    private int _roundCountdown;
    private int _currentRound = 0;
    #endregion

    #region Accessors
    public float TimeLeft => _model.TimeLeft;
    public float TimePerRound => _model.TimePerRound;
    public bool HasRoundStarted => hasRoundStarted;
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
        _currentRound = round;
        _view_RoundStart.SetTextRound(round);
        StartCoroutine(C_RoundStartCountdown());
    }
    #endregion // Public Methods

    #region Implementation
    private void TimerCountdown()
    {
        _model.TimeLeft -= Time.deltaTime;

        if (TimeLeft <= 0 && Controller.Player.Life >= 1)
        {
            StopTimer();
            TimeEnd?.Invoke();

            EndRoundTimer();
        }
    }

    private IEnumerator C_RoundStartCountdown()
    {
        _roundCountdown = _model.RoundStartCountdownSec;
        _view_RoundStart.ShowCountdown(true);
        StopTimer();

        do
        {
            _view_RoundStart.SetTextCountdown(_roundCountdown);

            _roundCountdown--;
            yield return new WaitForSeconds(1);
        } while (_roundCountdown > 0);

        if (_roundCountdown <= 0)
        {
            StartTimer();
            _view_RoundStart.ShowCountdown(false);
            StartRound?.Invoke();
        }
    }

    private void EndRoundTimer()
    {
        _view_RoundEnd.SetTextRound(_currentRound);
        _view_RoundEnd.ShowCountdown(true);
        StartCoroutine(C_RoundEndCountdown());
    }

    private IEnumerator C_RoundEndCountdown()
    {
        yield return new WaitForSeconds(_model.RoundEndCountdownSec);
        _view_RoundEnd.ShowCountdown(false);
        EndRound?.Invoke();
    }
    #endregion // Implementation
}
