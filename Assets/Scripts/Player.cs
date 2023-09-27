using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _spawnManager;
    [SerializeField]
    private bool _tripleShot = false;
    [SerializeField]
    private bool _shieldsUp = false;
    [SerializeField]
    private int _disableTripleShot = 5;
    [SerializeField]
    private int _disableSpeedBoost = 5;
    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    private GameObject _shields;

    //private Animator _shieldAnimation;

    //
    //  Standard Methods
    //
    //      Start()     Start is called before the first frame update
    //      Update()    Update is called once per frame

    void Start()
    {

        //this.transform.GetChild(0).gameObject.SetActive(false);
        _shields.SetActive(false);
        transform.position = new Vector3(0, 0, 0);
        _playerSpeed = _speed;
        
    }
    void Update()
    {
        CalculateMovement();

        if (  Input.GetKeyDown(KeyCode.Space) 
           && Time.time > _canFire) { FireLaser(); }

    }

    //
    // Player Features
    //
    //      CalculateMovement()
    //      FireLaser()
    //      Damage()

    void CalculateMovement()
    {

        //
        // Note:  Have a bug in here, probably the boundaries being a bit too much (left/right)
        //  during one game play I managed to get the player stuck off screen...
        //
        

        // get position information
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // set next position
        transform.Translate(direction * _playerSpeed * Time.deltaTime);

        //check screen boundaries
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (Mathf.Abs(transform.position.x) >= 11.5f) { transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0); }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_tripleShot) 
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1.015f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.015f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_shieldsUp) 
        {
            this.DisableShields();
            return;
        }

        _lives--;

        if (_lives < 1) {
            _spawnManager.GetComponent<SpawnManager>().PlayerDead();
            Destroy(this.gameObject); 
        }
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

    public void TripleShotPowerUp()
    {
        _tripleShot = true;
        StartCoroutine(DisableTripleShot());
    }

    public void SpeedBoost()
    {
        _playerSpeed =  _speed * _speedMultiplier;
        StartCoroutine(DisableSpeedBoost());
    }

    public void ShieldUp()
    {
        //this.transform.GetChild(0).gameObject.SetActive(true);
        _shields.SetActive(true);
        _shieldsUp = true;
    }

    IEnumerator DisableTripleShot()
    {
        yield return new WaitForSeconds(_disableTripleShot);
        _tripleShot = false;
    }

    IEnumerator DisableSpeedBoost()
    {
        yield return new WaitForSeconds(_disableSpeedBoost);
        _playerSpeed = _speed;
    }

    private void DisableShields()
    {
        //this.transform.GetChild(0).gameObject.SetActive(false);
        _shields.SetActive(false);
        _shieldsUp = false;
    }

}
