using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : ASingleton<PlayerBoundaries>
{

    #region Private Fields
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    #endregion

    #region Unity Callbacks

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        //for Orthographic Camera use below. For Perspective, remove the *-1 from 2nd param and add the *-1 on 3rd param
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
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
