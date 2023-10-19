using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage Laser Behavior
/// </summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: Laser Prefab
/// 
/// Used by Player, TripleShot and Enemy Laser.
/// </remarks>
/// 

public class Laser : MonoBehaviour
{
    /// <summary>
    /// The following variables are populated in the Inspector.
    /// </summary>
    [SerializeField] private float _speed = 8.0f;

    /// <summary>
    /// Properties (getter/setter methods)
    /// </summary>
    //public bool EnemyFire { get; set; }

    /// <summary>
    /// Mark as Enemy Fire if needed
    /// </summary>
    void Start()
    {
        if (  this.transform.parent
           && this.transform.parent.CompareTag("Enemy")) { this.tag = "EnemyLaser"; }
//           && this.transform.parent.CompareTag("Enemy") ) { EnemyFire = true; this.tag = "EnemyLaser"; }
    }

    /// <summary>
    /// Manager Laser direction
    /// </summary>
    /// 
    /// <remarks>
    /// Down if Enemy fire, up if Player fire.  Once off screen, destroy itself.
    /// </remarks>
    /// 

    void Update()
    {
        if (this.tag == "EnemyLaser")  
             { transform.Translate(Vector3.down * _speed * Time.deltaTime); }
        else { transform.Translate(Vector3.up   * _speed * Time.deltaTime); }

        if (Mathf.Abs(transform.position.y) <= 8f) { return; }
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Laser Collision handeler
    /// </summary>
    /// 
    /// <param name="other">Colliding Object</param>
    /// 
    /// <remarks>
    /// If striking Player, destroy sibling laser, play explosion, invoke player.Damage() then destroy itself.
    /// </remarks>
    /// 

    private void OnTriggerEnter2D(Collider2D other)
    {

        //
        // HACK:  Laser always collides with game object firing it.
        //
        //    Laser should probably originate outside of Player/Enemy collider or have some
        //    other way of knowning to ignore the player.  That's why we have to check
        //    the collision here.  Seems strained...

        //
        // HACK:  Delay on laser destruction
        //
        //    There is a delay on the Laser destruction due to the Enemy having two 
        //    Laser game objects.  Since I don't want a "double tap" I need just a
        //    moment for the lasers to coordinate thier destruction
        //

        if (  ( other.tag == "Player" && this.tag != "Laser" )
           || ( other.tag == "Enemy"  && this.tag != "EnemyLaser" ) 
           ||   other.tag == "Asteroid" )
        {
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 0.1f);
        }

    }

}
