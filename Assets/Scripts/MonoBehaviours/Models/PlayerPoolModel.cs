using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoolModel : MonoBehaviour
{
    #region Pool
    private List<PlayerBullet> _listProjectile = new List<PlayerBullet>();
    private List<PlayerRocket> _listRocket = new List<PlayerRocket>();

    [Header("Pool Objects")]
    [SerializeField]
    private Transform _poolProjectileParent;
    [SerializeField]
    private Transform _poolRocketParent;

    [SerializeField]
    private PlayerBullet _projectilePrefab;
    [SerializeField]
    private PlayerRocket _rocketPrefab;
    #endregion // Pool

    #region Pool Details
    [Header("Pool Size")]
    public int ProjectileMax = 20;
    public int RocketMax = 5;
    #endregion // Pool Details

    #region Accessors
    public int ProjectileCount => _listProjectile.Count;
    public int RocketCount => _listRocket.Count;
    #endregion // Accessors

    #region Public Methods
    public void PoolObjects()
    {
        if (ProjectileCount != ProjectileMax)
        {
            PoolProjectiles();
        }

        if (RocketCount != RocketMax)
        {
            PoolRockets();
        }
    }

    public PlayerBullet GetPlayerBullet()
    {
        for (int i = 0; i < ProjectileMax; i++)
        {
            if (!_listProjectile[i].gameObject.activeInHierarchy)
            {
                return _listProjectile[i];
            }
        }

        return null;
    }

    public PlayerRocket GetPlayerRocket()
    {
        for (int i = 0; i < RocketCount; i++)
        {
            if (!_listRocket[i].gameObject.activeInHierarchy)
            {
                return _listRocket[i];
            }
        }

        return null;
    }
    #endregion

    #region Implementation
    private void PoolProjectiles()
    {
        PlayerBullet tmp;
        for (int i = 0; i < ProjectileMax; i++)
        {
            tmp = Instantiate(_projectilePrefab);
            tmp.transform.SetParent(_poolProjectileParent);
            tmp.gameObject.SetActive(false);

            _listProjectile.Add(tmp);
        }
    }

    private void PoolRockets()
    {
        PlayerRocket tmp;
        for (int i = 0; i < RocketMax; i++)
        {
            tmp = Instantiate(_rocketPrefab);
            tmp.transform.SetParent(_poolRocketParent);
            tmp.gameObject.SetActive(false);

            _listRocket.Add(tmp);
        }
    }
    #endregion
}
