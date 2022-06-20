using UnityEngine;

public class RandomNoteController : ControllerHelper
{
    #region MVC
    [SerializeField]
    private RandomNoteView _view;
    #endregion // MVC

    #region Private Fields
    private readonly string _jokeUrl = "https://icanhazdadjoke.com/";
    private readonly string _jokeBanner = "Here's a joke:";
    private readonly string _failJokeMsg = "Couldn't load a joke.";
    private readonly string _loadMsg = "Loading...";

    private readonly string _adviceUrl = "https://api.adviceslip.com/advice";
    private readonly string _adviceBanner = "Here's an advice:";
    private readonly string _failAdviceMsg = "Couldn't load an advice.";

    //private bool _test;
    #endregion // Private Fields

    //private void OnEnable()
    //{
    //    KornerGames.HttpClient.HttpClientSystem.ShowMessage += ShowMessage;
    //}
    //private void OnDisable()
    //{
    //    KornerGames.HttpClient.HttpClientSystem.ShowMessage -= ShowMessage;
    //}

    #region Public Methods
    public void GetNote()
    {
        //_test = false;
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            GetJoke();
        }
        else
        {
            GetAdvice();
        }
    }

    public void Abort()
    {
        if (Services.Instance.HttpClient != null)
        {
            Services.Instance.HttpClient.Abort();
        }
    }
    #endregion // Public Methods

    #region Implementation
    [ContextMenu("Get Joke")]
    private async void GetJoke()
    {
        _view.SetBanner(_jokeBanner);
        _view.ShowMessage(_loadMsg); // Show Loading Message

        var result = await Services.Instance.HttpClient.Get<JokeInfo>(_jokeUrl);

        if (result != null && !string.IsNullOrEmpty(result.joke))
        {
            ShowMessage(result.joke); // Show the Joke
        }
        else
        {
            ShowMessage(_failJokeMsg); // Show Failed Message
        }
    }

    [ContextMenu("Get Advice")]
    private async void GetAdvice()
    {
        _view.SetBanner(_adviceBanner);
        _view.ShowMessage(_loadMsg); // Show Loading Message

        var result = await Services.Instance.HttpClient.Get<AdviceSlip>(_adviceUrl);

        if (result != null && !string.IsNullOrEmpty(result.slip.advice))
        {
            ShowMessage(result.slip.advice); // Show the Advice
        }
        else
        {
            ShowMessage(_failAdviceMsg); // Show Failed Message
        }
    }

    private void ShowMessage(string message)
    {
        //if (!_test)
        //{
            _view.ShowMessage(message);
        //    _test = true;
        //}
    }
    #endregion // Implementation
}
