using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Enemy Behavior
/// </summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: Enemy Prefab
/// 
/// </remarks>
/// 

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The following variables are populated in the Inspector.
    /// </summary>
    [SerializeField] private GameObject     _laserPrefab;
    ///
    [SerializeField] private float          _speed = 4.0f;

    /// <summary>
    /// Private Variables
    /// </summary>
    private Player  _player;
    private bool    _enemyDead = false;

    /// <summary>
    /// Create a new Enemy object.
    /// </summary>
    /// 
    /// <remarks>
    /// Choose a random (x) starting point.
    /// <br/>Start Coroutine to randomly fire a laser
    /// </remarks>
    /// 

    void Start()
    {
        transform.position = new Vector3(Random.Range(-10.0f,10.0f), 8, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (!_player) { Debug.Log("Player is NULL"); }

        StartCoroutine(FireLaser());
    }

    /// <summary>
    /// Move Enemy down at predefined pace
    /// </summary>
    /// 
    /// <remarks>
    /// Once enemy is off screen, relocate back to the top to continue attacking.
    /// </remarks>
    /// 

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8.0f) { transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 8, 0);  }
    }

    /// <summary>
    /// Enemy destruction
    /// </summary>
    /// 
    /// <remarks>
    /// Destroy this Enemy if it collides with a Player Laser or the Player.  Ignore collisions with 
    /// other Enemies or Enemy lasers.
    /// </remarks>
    /// 
    /// <param name="other">Colliding Game Object</param>

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (  other.tag == "Laser"
           || other.tag == "Player" ) { EnemyDeathScene(); }

    }

    /// <summary>
    /// Enemy death management
    /// </summary>
    /// 
    /// <remarks>
    /// Mark the Enemy as dead  and destroy its Collider so it no longer takes damage or
    /// fires off lasers.
    /// <br/> set speed to 0 so it does not continue moving on the screen.
    /// <br/> Trigger the destruction animation and sound
    /// <br/> Add to the Player Score.
    /// <br/> Destroy itself
    /// </remarks>
    /// 

    private void EnemyDeathScene()
    {

        _enemyDead = true;
        Destroy(GetComponent<Collider2D>());        // Destroy Collider so it does not keep impacting other Lasers.
        _speed = 0;
        this.gameObject.GetComponent<Animator>().SetTrigger("EnemyDestroyed");
        this.GetComponent<AudioSource>().Play();
        if (_player != null) { _player.AddScore(10); }
        Destroy(this.gameObject, 2.4f);

    }

    /// <summary>
    /// Fire Enemy Laser at random intervals
    /// </summary>
    /// 
    /// <remarks>
    /// Pause 1-2 seconds so Enemy is not shooting from off screen
    /// <br/> Ensure Enemy is not already dead
    /// <br/> Fire laser every 3-7 seconds
    /// </remarks>
    /// 
    /// <returns>WaitForSeconds()</returns>
    /// 

    IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));  // pause so they are not shooting from off screen.
        while (true && !_enemyDead)
        {
            _ = Instantiate(_laserPrefab, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));  // pause between subsequent shots.
        }
    }
}
