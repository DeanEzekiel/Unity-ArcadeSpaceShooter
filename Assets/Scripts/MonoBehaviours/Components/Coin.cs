using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    EnemyModel enemyModel;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, enemyModel.coinLifetime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            GameController.Instance.CoinCollected();
            Destroy(gameObject);
        }
    }
}
