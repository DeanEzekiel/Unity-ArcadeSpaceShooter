using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AlienForwardDetector : MonoBehaviour
{
    public bool HasObstacle { get; private set; } = false;
    public bool Shoot { get; private set; } = false;
    public bool Think = false;
    public bool CanDetect = false;
    private int prevObstacleHash;

    protected EnemyModel_SO EnemyModel => GameController.Instance.Controller.Enemy.Model;

    [SerializeField]
    private EnemyModel_SO _enemyModel;
    //private Action shoot;
    //private Action think;

    BoxCollider2D theTrigger;

    private void Start()
    {
        SetTriggerSize();
    }

    private void OnEnable()
    {
        SetTriggerSize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (CanDetect)
        //{
            if (collision.CompareTag(Tags.Player) ||
                collision.CompareTag(Tags.Asteroid))
            {
                //prevObstacleHash = collision.gameObject.GetHashCode();
                HasObstacle = true;
                Shoot = true;
                //shoot?.Invoke();
            }
            else if (collision.CompareTag(Tags.Alien))
            {
                //prevObstacleHash = collision.gameObject.GetHashCode();
                HasObstacle = true;
                Think = true;
                //think?.Invoke();
            }
        //}
        //else
        //{
        //    HasObstacle = false;
        //    Shoot = false;
        //    Think = false;
        //}
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (CanDetect)
        //{
            if ((collision.CompareTag(Tags.Player) ||
                collision.CompareTag(Tags.Asteroid) ||
                collision.CompareTag(Tags.Alien)) )
            {
                HasObstacle = false;
                Shoot = false;
                Think = true;
                //think?.Invoke();
            }
        //}
        //else
        //{
        //    HasObstacle = false;
        //    Shoot = false;
        //    Think = false;
        //}
    }

    private void SetTriggerSize()
    {
        theTrigger = transform.GetComponent<BoxCollider2D>();

        try
        {
            theTrigger.size = new Vector2(EnemyModel.alienForwardDetection, 1);
        }
        catch (Exception)
        {
            // need its own private _enemyModel for the Welcome Scene
            theTrigger.size = new Vector2(_enemyModel.alienForwardDetection, 1);
        }

        theTrigger.offset = new Vector2(theTrigger.size.x / 2, 0);
    }

    public void EnableTrigger()
    {
        theTrigger.enabled = true;
    }

    public void DisableTrigger()
    {
        theTrigger.enabled = false;
    }

    //public void RegisterShooter(Action method)
    //{
    //    if(method == null)
    //    {
    //        return;
    //    }
    //    shoot += method;
    //}

    //public void RegisterThinker(Action method)
    //{
    //    if (method == null)
    //    {
    //        return;
    //    }
    //    think += method;
    //}

    //public void UnregisterShooter(Action method)
    //{
    //    if (method == null)
    //    {
    //        return;
    //    }
    //    shoot -= method;
    //}

    //public void UnregisterThinker(Action method)
    //{
    //    if (method == null)
    //    {
    //        return;
    //    }
    //    think -= method;
    //}
}
