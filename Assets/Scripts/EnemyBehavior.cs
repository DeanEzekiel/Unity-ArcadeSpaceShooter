using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    #region Public Fields
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
            if (gameObject.CompareTag("Enemy"))
                AlienKilled?.Invoke();
            else if (gameObject.CompareTag("Asteroid"))
                Asteroid1Killed?.Invoke();

            hit = true;
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //when exiting the background, destroy this game object
        if (collision.gameObject.CompareTag("Background"))
            Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Rocket") || other.gameObject.CompareTag("PlayerProjectile") || other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Enemy"))
                AlienKilled?.Invoke();
            else if (gameObject.CompareTag("Asteroid"))
                Asteroid1Killed?.Invoke();
        }

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

    #region
    //When invoked, this will add points
    public static event Action Asteroid1Killed;
    public static event Action AlienKilled;
    #endregion

}
