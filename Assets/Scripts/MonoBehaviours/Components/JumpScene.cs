using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScene : MonoBehaviour
{
    public int SceneIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(C_JumpToScene());
    }

    private IEnumerator C_JumpToScene()
    {
        yield return null;
        LevelLoader.Instance.LoadScene(SceneIndex, false);
    }
}
