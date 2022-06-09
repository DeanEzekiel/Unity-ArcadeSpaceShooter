using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolModel : MonoBehaviour
{
    #region Pool
    private List<Alien> _listAliens = new List<Alien>();
    private List<Asteroid> _listAsteroids = new List<Asteroid>();
    private List<AlienBullet> _listBullets = new List<AlienBullet>();
    private List<Coin> _listCoins = new List<Coin>();

    [Header("Pool Objects")]
    [SerializeField]
    private Transform _poolAlienParent;
    [SerializeField]
    private Transform _poolAsteroidParent;
    [SerializeField]
    private Transform _poolBulletParent;
    [SerializeField]
    private Transform _poolCoinParent;

    [SerializeField]
    private Alien _alienPrefab;
    [SerializeField]
    private Asteroid _asteroidPrefab;
    [SerializeField]
    private AlienBullet _bulletPrefab;
    [SerializeField]
    private Coin _coinPrefab;
    #endregion // Pool

    #region Pool Details
    [Header("Pool Size")]
    public int AlienMax = 50;
    public int AsteroidMax = 50;
    public int BulletMax = 50;
    public int CoinMax = 50;
    #endregion // Pool Details

    #region Public Methods
    public void PoolObjects(EnemyController controller)
    {
        print("Enemies Pooling");
        if (_listAliens.Count != AlienMax)
        {
            PoolAliens(controller);
        }

        if (_listAsteroids.Count != AsteroidMax)
        {
            PoolAsteroids(controller);
        }

        if (_listBullets.Count != BulletMax)
        {
            PoolBullets();
        }

        if (_listCoins.Count != CoinMax)
        {
            PoolCoins();
        }
    }

    public Alien GetAlienFromPool()
    {
        for (int i = 0; i < AlienMax; i++)
        {
            if (!_listAliens[i].gameObject.activeInHierarchy)
            {
                return _listAliens[i];
            }
        }

        return null;
    }

    public Asteroid GetAsteroidFromPool()
    {
        for (int i = 0; i < AsteroidMax; i++)
        {
            if (!_listAsteroids[i].gameObject.activeInHierarchy)
            {
                return _listAsteroids[i];
            }
        }

        return null;
    }

    public AlienBullet GetBulletFromPool()
    {
        for (int i = 0; i < BulletMax; i++)
        {
            if (!_listBullets[i].gameObject.activeInHierarchy)
            {
                return _listBullets[i];
            }
        }

        return null;
    }

    public Coin GetCoinFromPool()
    {
        for (int i = 0; i < CoinMax; i++)
        {
            if (!_listCoins[i].gameObject.activeInHierarchy)
            {
                return _listCoins[i];
            }
        }

        return null;
    }
    #endregion // Public Methods

    #region Implementation
    private void PoolAliens(EnemyController controller)
    {
        Alien tmp;
        for (int i = 0; i < AlienMax; i++)
        {
            tmp = Instantiate(_alienPrefab);
            tmp.transform.SetParent(_poolAlienParent);
            tmp.RegisterController(controller);
            tmp.gameObject.SetActive(false);

            _listAliens.Add(tmp);
        }
    }

    private void PoolAsteroids(EnemyController controller)
    {
        Asteroid tmp;
        for (int i = 0; i < AsteroidMax; i++)
        {
            tmp = Instantiate(_asteroidPrefab);
            tmp.transform.SetParent(_poolAsteroidParent);
            tmp.RegisterController(controller);
            tmp.gameObject.SetActive(false);

            _listAsteroids.Add(tmp);
        }
    }

    private void PoolBullets()
    {
        AlienBullet tmp;
        for (int i = 0; i < BulletMax; i++)
        {
            tmp = Instantiate(_bulletPrefab);
            tmp.transform.SetParent(_poolBulletParent);
            tmp.gameObject.SetActive(false);

            _listBullets.Add(tmp);
        }
    }

    private void PoolCoins()
    {
        Coin tmp;
        for (int i = 0; i < CoinMax; i++)
        {
            tmp = Instantiate(_coinPrefab);
            tmp.transform.SetParent(_poolCoinParent);
            tmp.gameObject.SetActive(false);

            _listCoins.Add(tmp);
        }
    }
    #endregion // Implementation
}
