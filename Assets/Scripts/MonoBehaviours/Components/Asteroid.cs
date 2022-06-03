using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : AEnemy
{
    #region Unity Calllbacks
    private void OnEnable()
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
        // random direction
        directionX = UnityEngine.Random.Range(0.2f, 0.8f);
        directionY = 1 - directionX;

        // must move towards the game view -- opposite of the spawn point
        directionX *= FlipSign(transform.position.x);
        directionY *= FlipSign(transform.position.y);
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

    /// <summary>
    /// if the number is exactly 0, randomize the sign
    /// else, flip the sign
    /// </summary>
    /// <param name="coordinate">Transform position X or Y</param>
    /// <returns>An integer that will either be -1 or 1 to hold the sign</returns>
    private int FlipSign(float coordinate)
    {
        int sign = 1;
        if (coordinate == 0)
        {
            if(Random.Range(0,2) > 0)
            {
                sign = -1;
            }
        }
        else if (coordinate > 0)
        {
            sign = -1;
        }
        else
        {
            sign = 1;
        }

        return sign;
    }
    #endregion
}
