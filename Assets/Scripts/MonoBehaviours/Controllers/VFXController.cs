using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : ControllerHelper
{
    #region MVC
    [SerializeField]
    private VFXPoolModel _poolModel;
    #endregion // MVC

    #region Unity Callbacks
    private void Start()
    {
        _poolModel.PoolAllVFXs();
    }
    #endregion // Unity Callbacks

    #region Public Methods
    public void SpawnVFX(VFX vfxType, Vector3 position)
    {
        GameObject vfx = _poolModel.GetFromPool(vfxType);
        vfx.transform.position = position;
        vfx.SetActive(true);
    }

    public void DisableActiveVFXs()
    {
        _poolModel.DisableActiveVFXs();
    }
    #endregion // Public Methods
}
