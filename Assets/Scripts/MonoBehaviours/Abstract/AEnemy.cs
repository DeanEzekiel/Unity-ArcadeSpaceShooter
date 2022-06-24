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

    protected EnemyController Controller;

    public void RegisterController(EnemyController controller)
    {
        if(Controller == null)
        {
            Controller = controller;
        }
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }
    // TODO to Protected
    public virtual void OnDisable()
    {
        if (hit)
        {
            AudioController.Instance.PlaySFX(SFX.Hit_Crash);
            int randomNumber = UnityEngine.Random.Range(1, 101); //from 1 - 100

            if (randomNumber <= Controller.CoinDropRate)
            {
                //Instantiate(coin, transform.position, coin.transform.rotation);
                var coin = Controller.GetCoin();
                coin.transform.position = transform.position;
                coin.Activate(Controller.CoinLifetime);
            }
        }
    }

    protected abstract void Move();
    protected abstract void Rotate();
}
