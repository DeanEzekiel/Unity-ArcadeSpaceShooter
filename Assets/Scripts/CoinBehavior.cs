using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    #region Untiy Callbacks

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, GameController.Instance.CoinLifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinPickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
    #endregion

    #region Events

    public static event Action CoinPickedUp;

    #endregion
}
