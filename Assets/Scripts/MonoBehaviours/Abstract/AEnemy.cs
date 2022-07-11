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

    //screenbounds - start
    protected Vector2 screenBounds;
    protected float objectWidth;
    protected float objectHeight;

    protected float spaceMinX = 0f;
    protected float spaceMaxX = 0f;
    protected float spaceMinY = 0f;
    protected float spaceMaxY = 0f;
    //screenbounds - end

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
        SetScreenBounds();
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
                coin.transform.position = CoinPosition();
                coin.Activate(Controller.CoinLifetime);
            }
        }
    }

    protected virtual void SetScreenBounds()
    {
        //Start [Bounds] get Screen Bounds and movable space
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        spaceMinX = screenBounds.x * -1 + objectWidth;
        spaceMaxX = screenBounds.x - objectWidth;
        spaceMinY = screenBounds.y * -1 + objectHeight;
        spaceMaxY = screenBounds.y - objectHeight;
        //End [Bounds]
    }

    private Vector3 CoinPosition()
    {
        Vector3 transformPos = transform.position;

        if (transformPos.x < spaceMinX)
        {
            transformPos.x = spaceMinX;
        }
        else if (transformPos.x > spaceMaxX)
        {
            transformPos.x = spaceMaxX;
        }

        if (transformPos.y < spaceMinY)
        {
            transformPos.y = spaceMinY;
        }
        else if (transformPos.y > spaceMaxY)
        {
            transformPos.y = spaceMaxY;
        }

        return transformPos;
    }

    protected abstract void Move();
    protected abstract void Rotate();
}
