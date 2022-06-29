using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RocketTargetChecker : MonoBehaviour
{
    [SerializeField]
    PlayerRocket _theRocket;
    [SerializeField]
    CircleCollider2D theTrigger;
    bool _canDetect = false;

    private void EnableTrigger()
    {
        theTrigger.enabled = true;
        _canDetect = true;
    }

    private void DisableTrigger()
    {
        theTrigger.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canDetect &&
            (collision.CompareTag(Tags.Asteroid) ||
            collision.CompareTag(Tags.Alien)))
        {
            Debug.Log($"Found target {collision.gameObject.name}");
            _theRocket.RegisterTarget(collision.gameObject);
            _canDetect = false;
            DisableTrigger();
        }
    }

    private void OnEnable()
    {
        PlayerRocket.EnableTargetChecker += EnableTrigger;
    }

    private void OnDisable()
    {
        PlayerRocket.EnableTargetChecker -= EnableTrigger;
    }
}
