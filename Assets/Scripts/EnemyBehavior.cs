using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    #region Private Fields
    public float rotationZ;
    public float directionX;
    public float directionY;
    public float movementSpeed;
    public int randomDirection;
    public bool hit = false;

    public GameObject coin;
    #endregion

    #region Unity Callbacks

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rocket"))
        {
            hit = true;
            print("blasted");
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        //if it bumps with other game objects, destroy this
        hit = true;
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (hit)
        {
            int randomNumber = UnityEngine.Random.Range(1, 101); //from 1 - 100

            if (randomNumber <= GameController.Instance.CoinDropRate)
            {
                Instantiate(coin, transform.position, coin.transform.rotation);
            }
        }
    }

    #endregion

    #region Methods

    public void SetRotation()
    {
        rotationZ = UnityEngine.Random.Range(0f, 360f);
        //transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        transform.Rotate(0, 0, rotationZ);
    }

    public void SetDirection()
    {
        directionX = UnityEngine.Random.Range(0f, 1f);
        directionY = 1 - directionX;

        //to randomly set the direction X and/or Y to negative
        randomDirection = UnityEngine.Random.Range(1, 5);
        switch (randomDirection)
        {
            case 1:
                break;
            case 2:
                directionX *= -1;
                break;
            case 3:
                directionX *= -1;
                directionY *= -1;
                break;
            case 4:
                directionY *= -1;
                break;
            default:
                break;
        }
    }

    public void SetMovementSpeed()
    {
        movementSpeed = UnityEngine.Random.Range(GameController.Instance.AsteroidSpeedMin, GameController.Instance.AsteroidSpeedMax);
    }

    #endregion


}
