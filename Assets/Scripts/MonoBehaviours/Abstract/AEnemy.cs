using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : ACollidable
{
    public float rotationZ;
    public float directionX;
    public float directionY;
    public float movementSpeed;
    public int randomDirection;
    public bool hit;

    public Coin coin;

    protected EnemyModel_SO EnemyModel => GameController.Instance.Controller.Enemy.Model;

    public virtual void OnDestroy()
    {
        if (hit)
        {
            int randomNumber = UnityEngine.Random.Range(1, 101); //from 1 - 100

            if (randomNumber <= EnemyModel.coinDropRate)
            {
                Instantiate(coin, transform.position, coin.transform.rotation);
            }
        }
    }

    protected abstract void Move();
    protected abstract void Rotate();
}
