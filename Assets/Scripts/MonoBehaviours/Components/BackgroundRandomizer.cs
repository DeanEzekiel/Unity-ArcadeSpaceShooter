using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRandomizer : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    List<Sprite> _nebulaBackgrounds = new List<Sprite>();
    [SerializeField]
    List<Sprite> _starBackgrounds = new List<Sprite>();

    [SerializeField]
    SpriteRenderer _nebulaBG;
    [SerializeField]
    SpriteRenderer _starBG;
    #endregion // Inspector Fields

    #region Unity Callbacks
    private void OnEnable()
    {
        GameController.ChangeBackground += RandomizeBG;
    }

    private void OnDisable()
    {
        GameController.ChangeBackground -= RandomizeBG;
    }
    #endregion // Unity Callbacks

    #region Implementation
    private void RandomizeBG()
    {
        int randBG = Random.Range(0, _nebulaBackgrounds.Count);
        int randStar = Random.Range(0, _starBackgrounds.Count);

        _nebulaBG.sprite = _nebulaBackgrounds[randBG];
        _starBG.sprite = _starBackgrounds[randStar];
    }
    #endregion // Implementation
}
