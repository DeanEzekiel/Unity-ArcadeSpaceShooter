using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : AEnemy
{
    #region Unity Calllbacks
    private void Start()
    {
        hit = false;
        pointWhenKilled = GameMaster.Instance.enemySettings.asteroidPoints;
        Move();
        Rotate();
        SetMovementSpeed();
    }

    private void Update()
    {
        transform.Translate(new Vector3(directionX, directionY, 0)
            * movementSpeed * Time.deltaTime, Space.World);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //when exiting the background, destroy this game object
        if (collision.gameObject.CompareTag(Tags.Background))
            Destroy(gameObject);
    }
    #endregion

    #region Implementation
    protected override void Move()
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

    public override void OnBump(int addScore)
    {
        hit = true;
        base.OnBump(addScore);
    }

    protected override void Rotate()
    {
        rotationZ = UnityEngine.Random.Range(0f, 360f);
        //transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        transform.Rotate(0, 0, rotationZ);
    }

    private void SetMovementSpeed()
    {
        movementSpeed = UnityEngine.Random.Range(
            GameMaster.Instance.enemySettings.asteroidSpeedMin,
            GameMaster.Instance.enemySettings.asteroidSpeedMax);
    }
    #endregion
}
