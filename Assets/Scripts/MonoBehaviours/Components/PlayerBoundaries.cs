using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{

    #region Private Fields
    private float minX, maxX, minY, maxY;
    #endregion

    #region Unity Callbacks

    void Start()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(
            new Vector3
            (
                Screen.width,
                Screen.height,
                Camera.main.transform.position.z
            )
        );

        float objectWidth =
            transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        float objectHeight =
            transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        minX = screenBounds.x * -1 + objectWidth;
        maxX = screenBounds.x - objectWidth;
        minY = screenBounds.y * -1 + objectHeight;
        maxY = screenBounds.y - objectHeight;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        //for Orthographic Camera use below. For Perspective, remove the *-1 from 2nd param and add the *-1 on 3rd param
        viewPos.x = Mathf.Clamp(viewPos.x, minX, maxX);
        viewPos.y = Mathf.Clamp(viewPos.y, minY, maxY);
        transform.position = viewPos;
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        OnPlayerCollision?.Invoke();
    }
    #endregion

    #region Events
    public static event Action OnPlayerCollision;
    #endregion
}
