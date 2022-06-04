using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : ControllerHelper
{
    #region Inspector Fields
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private List<GameObject> enemyPrefabs;
    #endregion // Fields

    #region Private Fields
    private float _spawnTime;
    private int _spawnNumber;
    #endregion // Private Fields

    #region Public API
    public int CountSpawnPoints => spawnPoints.Count;

    public void SpawnSettings(float spawnTime, int spawnNumber)
    {
        _spawnTime = spawnTime;
        _spawnNumber = spawnNumber;
    }

    public void StartSpawning()
    {
        StartCoroutine(C_Spawn());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }
    #endregion // Public API

    #region Class Implementation
    private IEnumerator C_Spawn()
    {
        // TODO Timer MVC related
        while (GameMaster.Instance.Controller.Player.Model.timer > 0)
        {
            List<Transform> validSpawnPoints = GetSpawnPoints();
            yield return null;

            for (int i = 0; i < _spawnNumber; i++)
            {
                // randomize a number to determine which spawn point to use
                int index = Random.Range(0, validSpawnPoints.Count);

                Vector3 pos = validSpawnPoints[index].position;
                // randomize to determine which enemy prefab to spawn
                int iEnemy = Random.Range(0, enemyPrefabs.Count);
                Instantiate(enemyPrefabs[iEnemy], pos, Quaternion.identity);

                // remove from valid spawnpoints
                validSpawnPoints.RemoveAt(index);

            }

            yield return new WaitForSeconds(_spawnTime);
        }
    }

    private List<Transform> GetSpawnPoints()
    {
        List<Transform> validSpawnPoints = new List<Transform>();
        foreach (Transform spawnPoint in spawnPoints)
        {
            validSpawnPoints.Add(spawnPoint);
        }

        return validSpawnPoints;
    }
    #endregion // Class Implementation
}
