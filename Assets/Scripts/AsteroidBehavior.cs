using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : EnemyBehavior
{

    #region Unity Callbacks

    private void Awake()
    {
        SetRotation();
        SetDirection();
        SetMovementSpeed();

        //print($"Asteroid Details: Rotation = {rotationZ}, Movement Speed = {movementSpeed}, Direction X = {directionX}, Direction Y = {directionY}");

        //Note: could be better if I can plan on the logic if the asteroid is in quadrant 2, it should move to the other quadrants
    }

    private void Update()
    {
        transform.Translate(new Vector3(directionX, directionY, 0) * movementSpeed * Time.deltaTime, Space.World);
    }

    #endregion



    #region Methods

    private void SetRotation()
    {
        rotationZ = UnityEngine.Random.Range(0f, 360f);
        //transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        transform.Rotate(0, 0, rotationZ);
    }

    private void SetDirection()
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

    private void SetMovementSpeed()
    {
        movementSpeed = UnityEngine.Random.Range(GameController.Instance.AsteroidSpeedMin, GameController.Instance.AsteroidSpeedMax);
    }

    #endregion

}
