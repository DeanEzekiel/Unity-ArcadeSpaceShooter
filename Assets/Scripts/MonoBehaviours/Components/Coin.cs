using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, GameMaster.Instance.enemySettings.coinLifetime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            GameMaster.Instance.CoinCollected();
            Destroy(gameObject);
        }
    }
}
