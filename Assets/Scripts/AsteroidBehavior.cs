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
    }

    private void Update()
    {
        transform.Translate(new Vector3(directionX, directionY, 0) * movementSpeed * Time.deltaTime);
    }

    //destroy the asteroid if it goes out of bounds
    void OnTriggerExit2D(Collider2D bgCollider)
    {
        //when exiting the background, destroy the asteroid
        Destroy(gameObject);
    }

    #endregion


}
