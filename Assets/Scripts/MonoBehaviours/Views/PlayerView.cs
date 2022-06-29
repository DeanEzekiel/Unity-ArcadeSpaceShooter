using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerView : ACollidable
{
    #region Inspector Fields
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private PlayerBullet projectile;
    [SerializeField]
    private PlayerRocket rocket;

    [Space]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();
    #endregion // Inspector Fields

    #region Accessors
    public GameObject Shield => shield;
    #endregion // Accessors

    #region Events
    public static event Action<int> Bumped;
    #endregion // Events

    #region Unity Callbacks
    private void Start()
    {
        Init(); //ensure that shield is inactive at first
    }
    #endregion // Unity Callbacks

    #region ACollidable Implementation
    public override void OnBump(int addScore)
    {
        Bumped?.Invoke(addScore);
    }
    #endregion

    #region Class Implementation
    private void RandomizeSprite()
    {
        int random = UnityEngine.Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[random];
    }
    #endregion // Class Implementation

    #region Public Methods
    public void Init()
    {
        shield.SetActive(false);
        RandomizeSprite();
    }

    public void ResetPosition()
    {
        transform.position = Vector2.zero;
    }

    public void Move(Vector3 translation)
    {
        transform.Translate(translation, Space.World);
    }

    public void Rotate(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    public void Shoot(PlayerController controller)
    {
        //var bullet = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        //bullet.SetSpecs(controller.BulletSpeed, controller.BulletLifetime);

        var bullet = controller.GetPlayerBullet();
        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = spawnPoint.rotation;
        bullet.SetSpecs(controller.BulletSpeed, controller.BulletLifetime);
        bullet.Activate();
    }

    public void ToggleShield(bool value)
    {
        shield.SetActive(value);
    }

    public void ShootRocket(PlayerController controller)
    {
        //var bullet = Instantiate(rocket, spawnPoint.position, spawnPoint.rotation);
        //bullet.SetSpecs(controller.RocketSpeed, controller.RocketLifetime, controller.RocketBlastRadius);

        var rocket = controller.GetPlayerRocket();
        rocket.transform.position = spawnPoint.position;
        rocket.transform.rotation = spawnPoint.rotation;
        rocket.SetSpecs(controller.RocketSpeed, controller.RocketLifetime, controller.RocketBlastRadius);
        rocket.Activate();
    }
    #endregion
}
