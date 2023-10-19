﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Player Behavior
/// </summary>
/// 
/// <remarks>
/// Instantiated By: Game Scene
/// Attached To: Player Game Object in Hierarchy
/// 
/// </remarks>
/// 

public class Player : MonoBehaviour
{

    /// <summary>
    /// The following variables are populated in the Inspector.
    /// </summary>
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _rightEngineDamage;
    [SerializeField] private GameObject _leftEngineDamage;
    [SerializeField] private GameObject _spawnManager;
    [SerializeField] private GameObject _shields;
    [SerializeField] private AudioClip  _explostion;

    [SerializeField] private float  _playerSpeed;
    [SerializeField] private float  _speed              = 3.5f;
    [SerializeField] private float  _fireRate           = 0.5f;
    [SerializeField] private float  _canFire            = -1f;
    [SerializeField] private int    _lives              = 3;
    [SerializeField] private int    _score              = 0;
    [SerializeField] private bool   _tripleShot         = false;
    [SerializeField] private bool   _shieldsUp          = false;
    [SerializeField] private int    _disableTripleShot  = 5;
    [SerializeField] private float  _speedMultiplier    = 2;
    [SerializeField] private int    _disableSpeedBoost  = 5;

    /// <summary>
    /// Private Variables
    /// </summary>
    private UIManager _uiManager;


    /// <summary>
    /// Initialize Player settings
    /// </summary>
    /// 
    /// <remarks>
    /// Initialize player settings here, rather than just in the Inspector or in the vvariable
    /// section above, so they are reset when a game is restarted.
    /// </remarks>
    /// 

    void Start()
    {

        _shields.SetActive(false);
        _rightEngineDamage.SetActive(false);
        _leftEngineDamage.SetActive(false);

        transform.position = new Vector3(0, 0, 0);
        _playerSpeed = _speed;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (!_uiManager) { Debug.LogError("UI Manager is NULL!"); }
        
    }

    /// <summary>
    /// Update movement every frame and check for firing cool-down.
    /// </summary>
    /// 
    /// <remarks>
    /// Movement calculation is actually contained in the CalculateMovement() method. while the
    /// Laser fire is contained in the FireLaser() method.
    /// </remarks>
    /// 

    void Update()
    {
        CalculateMovement();

        if (  Input.GetKeyDown(KeyCode.Space) 
           && Time.time > _canFire) { FireLaser(); }

    }

    //
    // Player Feature Methods
    //
    //      CalculateMovement()
    //      FireLaser()
    //      Damage()
    //      AddScore()
    //      

    /// <summary>
    /// Determine speed and direction of Player movement
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// Movement direction is determined by Input.GetAxis()
    /// <br/>Player Speed is set to an initial value then set/reset by the PowerUp methods below
    /// <br/>Time.deltaTime() is used to convert movement from units per frame to units per second
    /// <br/>Scrolling off screen teleports the player to the other side
    /// 
    /// </remarks>
    /// 

    void CalculateMovement()
    {

        // get position information
        float horizontalInput   = Input.GetAxis("Horizontal");
        float verticalInput     = Input.GetAxis("Vertical");
        Vector3 direction       = new Vector3(horizontalInput, verticalInput, 0);

        // set next position
        transform.Translate(direction * _playerSpeed * Time.deltaTime);

        // check screen boundaries
        //
        // TODO: Player gets stuck off screen.
        //      Have a bug in here, probably the boundaries being a bit too much (left/right)
        //      during one game play I managed to get the player stuck off screen...
        //

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (Mathf.Abs(transform.position.x) >= 11.5f) { transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0); }

    }

    /// <summary>
    /// Instantiate Player Laser fire.
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// _tripleShot is set and reset in the PowerUp methods below.  Cool-down feature to limit rate of fire is set by the 
    /// _canFire and _fireRate private class variables.  _canFire is simply set to a future time.
    /// 
    /// </remarks>
    /// 

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_tripleShot) 
        {
            _ = Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1.015f, 0), Quaternion.identity);
        } else {
            _ = Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.015f, 0), Quaternion.identity);
        }

        this.GetComponents<AudioSource>()[0].Play();
    }

    /// <summary>
    /// Manage lives counter and damage visualization
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// Short circuit method if Shields PowerUp is active
    /// <br/> Maintain count of current lives
    /// <br/>End game when no more lives remaining
    /// <br/>Visualize Player Damage
    /// 
    /// </remarks>
    /// 

    private void Damage()
    {
        if (_shieldsUp) 
        {
            this.DisableShields();
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives < 1) {
            _spawnManager.GetComponent<SpawnManager>().PlayerDead();
            _uiManager.PlayerDead();
            Destroy(this.gameObject); 
        }

        switch (_lives)
        {
            case 3:
                _rightEngineDamage.SetActive(false);
                _leftEngineDamage.SetActive(false);
                break;
            case 2:
                _rightEngineDamage.SetActive(true);
                break;
            case 1:
                _leftEngineDamage.SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "Enemy" )
        {
            this.Damage();
        }

        if (   other.tag == "EnemyLaser" 
           && !other.transform.parent.GetComponent<EnemyLaser>().HasHit )
        {
            other.transform.parent.GetComponent<EnemyLaser>().HasHit = true;
            AudioSource.PlayClipAtPoint(_explostion, 0.9f * Camera.main.transform.position + 0.1f * transform.position, 1.0f);
            this.Damage();
        }
    }

    /// <summary>
    /// Maintain Current Score
    /// </summary>
    /// 
    /// <param name="points">Points to add</param>
    /// 

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    //
    // Power Ups
    //
    //      TripleShot()
    //      SpeedBoost()
    //      ShieldsUp()
    //
    //      DisableTripleShot()
    //      DisableSpeedBoost()
    //      DisableShields()

    /// <summary>
    /// Enable TripleShot PowerUp
    /// </summary>
    /// 
    /// <remarks>
    /// Enable _tripleShot boolean and initiate a coroutine to disable the powerup after
    /// a preset amount of time.
    /// </remarks>
    /// 

    public void TripleShotPowerUp()
    {
        _tripleShot = true;
        StartCoroutine(DisableTripleShot());
    }

    /// <summary>
    /// Enable SpeedBoost PowerUp
    /// </summary>
    /// 
    /// <remarks>
    /// Set new _playerSpeed and initiate a coroutine to disable the powerup after
    /// a preset amount of time.
    /// </remarks>
    /// 

    public void SpeedBoost()
    {
        _playerSpeed =  _speed * _speedMultiplier;
        StartCoroutine(DisableSpeedBoost());
    }

    /// <summary>
    /// Enable Shields PowerUp
    /// </summary>
    /// 
    /// <remarks>
    /// 
    /// This PowerUp does not require a coroutine timer to disable it as it is
    /// disabled after the next impact with either an Enemy ship or Enemy Laser 
    /// Fire.
    /// 
    /// </remarks>
    /// 

    public void ShieldUp()
    {
        _shields.SetActive(true);
        _shieldsUp = true;
    }

    /// <summary>
    /// Disable Triple Shot PowerUp
    /// </summary>
    /// 
    /// <returns>WaitForSeconds()</returns>
    /// 

    IEnumerator DisableTripleShot()
    {
        yield return new WaitForSeconds(_disableTripleShot);
        _tripleShot = false;
    }

    /// <summary>
    /// Disable Speed Boost PowerUp
    /// </summary>
    /// 
    /// <returns>WaitForSeconds()</returns>
    /// 

    IEnumerator DisableSpeedBoost()
    {
        yield return new WaitForSeconds(_disableSpeedBoost);
        _playerSpeed = _speed;
    }

    /// <summary>
    /// Disable Sheilds Power Up.
    /// </summary>
    /// 
    /// <remarks>
    /// This is invoked on first collision by this.Damage().
    /// </remarks>
    /// 

    private void DisableShields()
    {
        _shields.SetActive(false);
        _shieldsUp = false;
    }

}
