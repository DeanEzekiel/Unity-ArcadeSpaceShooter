using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    /// <summary>
    /// Activates the coin in the scene and deactivates it after the set seconds.
    /// </summary>
    /// <param name="sec">Seconds it will take to deactivate the coin.</param>
    public void Activate(int sec)
    {
        gameObject.SetActive(true);
        
        GameController.Instance.Controller.VFX.SpawnVFX(VFX.CoinSpawn,
            transform.position);
        StartCoroutine(C_Deactivate(sec));
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private IEnumerator C_Deactivate(int sec)
    {
        yield return new WaitForSeconds(sec);
        Deactivate();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            GameController.Instance.Controller.VFX.SpawnVFX(VFX.CoinPickup,
                transform.position);
            AudioController.Instance.PlaySFX(SFX.Coin_Plus);
            GameController.Instance.CoinCollected();
            //Destroy(gameObject);
            Deactivate();
        }
    }
}
