using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : ControllerHelper
{
    #region MVC
    [SerializeField]
    private EnemyModel_SO _model;
    [SerializeField]
    private EnemyModel_SO _modelInit;

    [SerializeField]
    private EnemyPoolModel _modelPool;
    #endregion // MVC

    #region Inspector Fields
    [SerializeField]
    private List<Transform> spawnPoints;
    #endregion // Fields

    #region Private Fields
    private float _spawnTime;
    private int _spawnNumber;
    #endregion // Private Fields

    #region Accessors
    public int CountSpawnPoints => spawnPoints.Count;

    public int CoinDropRate => _model.coinDropRate;
    public int CoinLifetime => _model.coinLifetime;
    public int AsteroidPoints => _model.asteroidPoints;
    public float AsteroidSpeedMin => _model.asteroidSpeedMin;
    public float AsteroidSpeedMax => _model.asteroidSpeedMax;
    public float AlienSpeed => _model.alienSpeed;
    public int AlienPoints => _model.alienPoints;
    public float AlienDetectCooldown => _model.alienDetectCooldown;
    public float AlienBulletCooldown => _model.alienBulletCooldown;
    public float AlienBulletSpeed => _model.alienBulletSpeed;
    public float AlienBulletLifetime => _model.alienBulletLifetime;

    public EnemyModel_SO Model => _model;
    #endregion // Accessors

    #region Unity Callbacks
    private void Start()
    {
        _modelPool.PoolObjects(this);
    }
    #endregion // Unity Callbacks

    #region Methods for Pooling
    public AlienBullet GetBullet()
    {
        return _modelPool.GetBulletFromPool();
    }

    public Coin GetCoin()
    {
        return _modelPool.GetCoinFromPool();
    }

    private Alien GetAlien()
    {
        return _modelPool.GetAlienFromPool();
    }

    private Asteroid GetAsteroid()
    {
        return _modelPool.GetAsteroidFromPool();
    }
    #endregion // Public Methods for Pooling

    #region Public API
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

    public void ResetEnemySettings()
    {
        _model = _modelInit.DeepClone(_model);
    }
    #endregion // Public API

    #region Class Implementation
    private IEnumerator C_Spawn()
    {
        while (Controller.Timer.TimeLeft > 0)
        {
            List<Transform> validSpawnPoints = GetSpawnPoints();
            yield return null;

            for (int i = 0; i < _spawnNumber; i++)
            {
                // randomize a number to determine which spawn point to use
                int index = Random.Range(0, validSpawnPoints.Count);

                Vector3 pos = validSpawnPoints[index].position;
                // randomize to determine which enemy prefab to spawn
                var enemyType = (EnemyType)Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);

                //Instantiate(enemyPrefabs[iEnemy], pos, Quaternion.identity);
                if (enemyType == EnemyType.Alien)
                {
                    var enemy = GetAlien();
                    enemy.transform.position = pos;
                    enemy.Activate();
                }
                else if (enemyType == EnemyType.Asteroid)
                {
                    var enemy = GetAsteroid();
                    enemy.transform.position = pos;
                    enemy.Activate();
                }

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
