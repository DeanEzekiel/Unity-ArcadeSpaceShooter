using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    protected EnemyModel EnemyModel => GameController.Instance.Controller.Enemy.Model;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, EnemyModel.coinLifetime);
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
