using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerHelper
{
    #region MVC
    [SerializeField]
    private PlayerView _view;

    [SerializeField]
    private PlayerModel_SO _model;

    [SerializeField]
    private PlayerModel_SO _modelDefault;
    #endregion // MVC

    #region Private Fields
    private float shootTimer = 0;
    private float blasterTimer = 0;
    private Vector3 movementDirection;
    #endregion

    #region Accessors
    public bool PlayerControlsActive { get; private set; } = true;
    // TODO - remove the Model Accessor when the MVC Refactor is done
    public PlayerModel_SO Model => _model;
    public int Life => _model.life;
    public int LifeMax => _model.lifeMax;
    public int Coins => _model.coins;
    public int RocketCount => _model.rocketCount;
    public int RocketMax => _model.rocketMax;
    public float ShieldPoint => _model.shieldPoint;
    public float ShieldMax => _model.shieldMax;
    public float ShieldReplenishSec => _model.shieldReplenishSec;

    public float BulletSpeed => _model.playerBulletSpeed;
    public float BulletLifetime => _model.playerBulletLifetime;
    public float RocketSpeed => _model.rocketSpeed;
    public float RocketLifetime => _model.rocketLifetime;
    public float RocketBlastRadius => _model.rocketBlastRadius;
    #endregion // Accessors

    #region Unity Callbacks
    private void Update()
    {
        if (PlayerControlsActive)
        {
            Move();
            Rotate();
            Shoot();

            ShieldAbility();
            BlasterAbility();
        }

        ShieldPointsCalc();
    }

    private void OnEnable()
    {
        PlayerView.Bumped += OnBump;
    }

    private void OnDisable()
    {
        PlayerView.Bumped -= OnBump;
    }
    #endregion

    #region Public Method
    public void AddScore(int value)
    {
        _model.score += value;
    }

    public void Reset()
    {
        _model = _modelDefault.DeepClone(_model);
        ShowView();
        _view.Init();
    }

    public void ResetPosition()
    {
        _view.ResetPosition();
    }

    public void AllowPlayerControls(bool value)
    {
        PlayerControlsActive = value;
    }

    public void ShowView()
    {
        _view.gameObject.SetActive(true);
    }

    public void HideView()
    {
        _view.gameObject.SetActive(false);
    }

    public void CoinCollected()
    {
        _model.coins++;
    }

    public void SpendCoins(int cost)
    {
        _model.coins -= cost;
    }

    public void AddLife(int value)
    {
        _model.life = Mathf.Clamp(
            _model.life + (int)value,
            _model.life,
            _model.lifeMax
            );
    }
    public void AddRocketMax(int value, int shopMax)
    {
        _model.rocketMax = Mathf.Clamp(
            _model.rocketMax + (int)value,
            _model.rocketMax,
            shopMax
            );
    }
    public void RefillRockets()
    {
        _model.rocketCount =
            _model.rocketMax;
    }
    public void AddShieldPoints(float value)
    {
        _model.shieldPoint = Mathf.Clamp(
            _model.shieldPoint + value,
            _model.shieldPoint,
            _model.shieldMax
            );
    }
    public void ShortenShieldRegen(float value, float shopMin)
    {
        _model.shieldReplenishSec = Mathf.Clamp(
            _model.shieldReplenishSec - value,
            shopMin,
            _model.shieldReplenishSec
            );
    }
    #endregion

    #region Class Implementation
    private void Move()
    {
        //Movement
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        //Vector3 move = new Vector3(horizontalMove, verticalMove, 0);
        movementDirection = new Vector3(horizontalMove, verticalMove, 0);
        var translation = movementDirection *
            _model.playerSpeed * Time.deltaTime;

        _view.Move(translation);
    }

    private void Rotate()
    {
        if (GameController.Instance.GameModel.playerControls ==
            ControlOption.MotionFacing)
        {
            //rotate based on movement direction & need to adjust by 90 degrees
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,
                    movementDirection) * Quaternion.Euler(0, 0, 90);//90 deg
                var quaternion = Quaternion.RotateTowards(_view.transform.rotation,
                    toRotation,
                    _model.playerRotationSpeed *
                    Time.deltaTime);

                _view.Rotate(quaternion);
            }
        }
        else if (GameController.Instance.GameModel.playerControls ==
            ControlOption.CursorFacing)
        {
            //no need to adjust 90 degrees
            var mouse = Input.mousePosition;
            var screenPnt = Camera.main.WorldToScreenPoint(_view.transform.position);
            var offset = new Vector2(mouse.x - screenPnt.x,
                mouse.y - screenPnt.y);
            var angle = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
            var quaternion = Quaternion.Euler(0, 0, angle);

            _view.Rotate(quaternion);
        }
    }

    private void Shoot()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                _view.Shoot(this);
                shootTimer = _model.playerBulletCooldown;
            }
        }
    }
    private void ShieldAbility()
    {
        //check if player can toggle the shield
        if (Input.GetButtonDown("Jump"))
        {
            ToggleShield();
        }

    }
    private void ToggleShield()
    {
        // flip to toggle
        bool newValue = !_view.Shield.activeInHierarchy;
        _view.ToggleShield(newValue);
        _model.shieldOn = newValue;
    }
    private void BlasterAbility()
    {
        blasterTimer -= Time.deltaTime;

        if (blasterTimer <= 0)
        {
            if (Input.GetButtonUp("Fire2"))
            {
                if (_model.rocketCount > 0)
                {
                    _model.rocketCount--;
                    _view.ShootRocket(this);
                    blasterTimer = _model.rocketCooldown;
                }
            }
        }
    }

    private void ShieldPointsCalc()
    {
        if (_model.shieldOn && _model.shieldPoint > 0)
        {
            _model.shieldPoint -= (_model.shieldMax /
                _model.shieldDepleteSec * Time.deltaTime);
        }
        else if (!_model.shieldOn && _model.shieldPoint <
            _model.shieldMax)
        {
            _model.shieldPoint += (_model.shieldMax /
                _model.shieldReplenishSec * Time.deltaTime);
        }

        if (_model.shieldPoint > _model.shieldMax)
        {
            _model.shieldPoint = _model.shieldMax;
        }
        else if (_model.shieldPoint <= 0)
        {
            _model.shieldPoint = 0;
            ToggleShield(); //this will activate/deactivate the shield
        }
    }

    private void OnBump(int addScore)
    {
        if (_model.shieldOn == false)
        {
            _model.life--;
        }
        //score
        _model.score += addScore;
    }
    #endregion
}
