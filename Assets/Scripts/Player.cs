using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    // Start is called before the first frame update
    void Start()
    {
 
        transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (  Input.GetKeyDown(KeyCode.Space) 
           && Time.time > _canFire) { FireLaser(); }

    }

    void CalculateMovement()
    {

        // get position information
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // set next position
        transform.Translate(direction * _speed * Time.deltaTime);

        //check screen boundaries
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (Mathf.Abs(transform.position.x) >= 11.5f) { transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0); }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1) { Destroy(this.gameObject); }
    }
}
