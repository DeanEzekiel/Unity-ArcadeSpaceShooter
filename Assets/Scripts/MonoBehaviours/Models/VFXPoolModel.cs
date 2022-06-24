using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPoolModel : MonoBehaviour
{
    #region Pool
    private List<GameObject> _listVFXPlayerHit = new List<GameObject>();
    private List<GameObject> _listVFXAlienHit = new List<GameObject>();
    private List<GameObject> _listVFXAsteroidHit = new List<GameObject>();
    private List<GameObject> _listVFXShieldToggle = new List<GameObject>();
    private List<GameObject> _listVFXRocketImpact = new List<GameObject>();
    private List<GameObject> _listVFXCoinSpawn = new List<GameObject>();
    private List<GameObject> _listVFXCoinPickup = new List<GameObject>();

    [SerializeField]
    private Transform _poolParent;
    [SerializeField]
    private Transform _shieldParent;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject _vfxPlayerHit;
    [SerializeField]
    private GameObject _vfxAlienHit;
    [SerializeField]
    private GameObject _vfxAsteroidHit;
    [SerializeField]
    private GameObject _vfxShieldToggle;
    [SerializeField]
    private GameObject _vfxRocketImpact;
    [SerializeField]
    private GameObject _vfxCoinSpawn;
    [SerializeField]
    private GameObject _vfxCoinPickup;
    #endregion // Pool

    #region Pool Details
    [Header("Pool Size")]
    public int VFXPlayerHitCount = 5;
    public int VFXAlienHitCount = 20;
    public int VFXAsteroidHitCount = 20;
    public int VFXShieldToggleCount = 10;
    public int VFXRocketImpactCount = 5;
    public int VFXCoinSpawnCount = 20;
    public int VFXCoinPickupCount = 20;
    #endregion // Pool Details

    #region Public Methods
    public void PoolAllVFXs()
    {
        if (_listVFXAlienHit.Count != VFXAlienHitCount)
        {
            PoolVFXs(VFX.AlienHit);
        }
        if (_listVFXAsteroidHit.Count != VFXAsteroidHitCount)
        {
            PoolVFXs(VFX.AsteroidHit);
        }
        if (_listVFXCoinPickup.Count != VFXCoinPickupCount)
        {
            PoolVFXs(VFX.CoinPickup);
        }
        if (_listVFXCoinSpawn.Count != VFXCoinSpawnCount)
        {
            PoolVFXs(VFX.CoinSpawn);
        }
        if (_listVFXPlayerHit.Count != VFXPlayerHitCount)
        {
            PoolVFXs(VFX.PlayerHit);
        }
        if (_listVFXRocketImpact.Count != VFXRocketImpactCount)
        {
            PoolVFXs(VFX.RocketImpact);
        }
        if (_listVFXShieldToggle.Count != VFXShieldToggleCount)
        {
            PoolVFXs(VFX.ShieldToggle);
        }
    }

    public GameObject GetFromPool(VFX vfxType)
    {
        int maxCount = GetPoolCount(vfxType);

        for (int i = 0; i < maxCount; i++)
        {
            if (!CheckActiveVFX(vfxType, i))
            {
                return GetFromVFXList(vfxType, i);
            }
        }

        return null;
    }

    public void DisableActiveVFXs()
    {
        DisableActiveVFXType(VFX.AlienHit);
        DisableActiveVFXType(VFX.AsteroidHit);
        DisableActiveVFXType(VFX.CoinPickup);
        DisableActiveVFXType(VFX.CoinSpawn);
        DisableActiveVFXType(VFX.PlayerHit);
        DisableActiveVFXType(VFX.RocketImpact);
        DisableActiveVFXType(VFX.ShieldToggle);
    }
    #endregion // Public Methods

    #region Implementation
    private void PoolVFXs(VFX vfxType)
    {
        GameObject tempVFX;
        GameObject prefab = GetPrefab(vfxType);
        int maxCount = GetPoolCount(vfxType);

        for (int i = 0; i < maxCount; i++)
        {
            if (prefab != null)
            {
                tempVFX = Instantiate(prefab);
                if (vfxType == VFX.ShieldToggle)
                {
                    tempVFX.transform.SetParent(_shieldParent);
                }
                else
                {
                    tempVFX.transform.SetParent(_poolParent);
                }

                tempVFX.gameObject.SetActive(false);
                AddToList(vfxType, tempVFX);
            }
        }
    }

    private int GetPoolCount(VFX type)
    {
        switch (type)
        {
            case VFX.AlienHit:
                return VFXAlienHitCount;
            case VFX.AsteroidHit:
                return VFXAsteroidHitCount;
            case VFX.CoinPickup:
                return VFXCoinPickupCount;
            case VFX.CoinSpawn:
                return VFXCoinSpawnCount;
            case VFX.PlayerHit:
                return VFXPlayerHitCount;
            case VFX.RocketImpact:
                return VFXRocketImpactCount;
            case VFX.ShieldToggle:
                return VFXShieldToggleCount;
            default:
                return 0;
        }
    }

    private GameObject GetPrefab(VFX type)
    {
        switch (type)
        {
            case VFX.AlienHit:
                return _vfxAlienHit;
            case VFX.AsteroidHit:
                return _vfxAsteroidHit;
            case VFX.CoinPickup:
                return _vfxCoinPickup;
            case VFX.CoinSpawn:
                return _vfxCoinSpawn;
            case VFX.PlayerHit:
                return _vfxPlayerHit;
            case VFX.RocketImpact:
                return _vfxRocketImpact;
            case VFX.ShieldToggle:
                return _vfxShieldToggle;
            default:
                return null;
        }
    }

    private void AddToList(VFX type, GameObject tmp)
    {
        switch (type)
        {
            case VFX.AlienHit:
                _listVFXAlienHit.Add(tmp);
                break;
            case VFX.AsteroidHit:
                _listVFXAsteroidHit.Add(tmp);
                break;
            case VFX.CoinPickup:
                _listVFXCoinPickup.Add(tmp);
                break;
            case VFX.CoinSpawn:
                _listVFXCoinSpawn.Add(tmp);
                break;
            case VFX.PlayerHit:
                _listVFXPlayerHit.Add(tmp);
                break;
            case VFX.RocketImpact:
                _listVFXRocketImpact.Add(tmp);
                break;
            case VFX.ShieldToggle:
                _listVFXShieldToggle.Add(tmp);
                break;
        }
    }

    private GameObject GetFromVFXList(VFX vfxType, int index)
    {
        switch (vfxType)
        {
            case VFX.AlienHit:
                return _listVFXAlienHit[index];
            case VFX.AsteroidHit:
                return _listVFXAsteroidHit[index];
            case VFX.CoinPickup:
                return _listVFXCoinPickup[index];
            case VFX.CoinSpawn:
                return _listVFXCoinSpawn[index];
            case VFX.PlayerHit:
                return _listVFXPlayerHit[index];
            case VFX.RocketImpact:
                return _listVFXRocketImpact[index];
            case VFX.ShieldToggle:
                return _listVFXShieldToggle[index];
            default:
                return null;
        }
    }

    private bool CheckActiveVFX(VFX vfxType, int index)
    {
        switch (vfxType)
        {
            case VFX.AlienHit:
                return _listVFXAlienHit[index].gameObject.activeInHierarchy;
            case VFX.AsteroidHit:
                return _listVFXAsteroidHit[index].gameObject.activeInHierarchy;
            case VFX.CoinPickup:
                return _listVFXCoinPickup[index].gameObject.activeInHierarchy;
            case VFX.CoinSpawn:
                return _listVFXCoinSpawn[index].gameObject.activeInHierarchy;
            case VFX.PlayerHit:
                return _listVFXPlayerHit[index].gameObject.activeInHierarchy;
            case VFX.RocketImpact:
                return _listVFXRocketImpact[index].gameObject.activeInHierarchy;
            case VFX.ShieldToggle:
                return _listVFXShieldToggle[index].gameObject.activeInHierarchy;
            default:
                return false;
        }
    }

    private void DisableActiveVFXType(VFX vfxType)
    {
        int maxCount = GetPoolCount(vfxType);

        for (int i = 0; i < maxCount; i++)
        {
            if (CheckActiveVFX(vfxType, i))
            {
                DisableVFX(vfxType, i);
            }
        }
    }

    private void DisableVFX(VFX vfxType, int index)
    {
        switch (vfxType)
        {
            case VFX.AlienHit:
                _listVFXAlienHit[index].SetActive(false);
                break;
            case VFX.AsteroidHit:
                _listVFXAsteroidHit[index].SetActive(false);
                break;
            case VFX.CoinPickup:
                _listVFXCoinPickup[index].SetActive(false);
                break;
            case VFX.CoinSpawn:
                _listVFXCoinSpawn[index].SetActive(false);
                break;
            case VFX.PlayerHit:
                _listVFXPlayerHit[index].SetActive(false);
                break;
            case VFX.RocketImpact:
                _listVFXRocketImpact[index].SetActive(false);
                break;
            case VFX.ShieldToggle:
                _listVFXShieldToggle[index].SetActive(false);
                break;
        }
    }
    #endregion // Implementation
}
