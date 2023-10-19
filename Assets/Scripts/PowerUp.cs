using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create all Player PowerUps
/// </summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: Each PowerUp Prefab
/// 
/// Set initial position of PowerUp and enable the Player PowerUp on collision.
/// </remarks>
/// 

public class PowerUp : MonoBehaviour
{

    /// <summary>
    /// The following variables are populated in the Inspector.
    /// </summary>
    [SerializeField] private AudioClip  _powerUpSound;
    ///
    [SerializeField] private float      _speed          = 3.0f;

    /// <summary>
    /// Set initial PowerUp position
    /// </summary>
    /// 

    void Start()
    {
        transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 8, 0);
    }


    /// <summary>
    /// Set direction and speed of PowerUp movement
    /// </summary>
    /// 
    /// <remarks>
    /// Once the PowerUp falls off the screen, it is destroyed.
    /// </remarks>

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8.0f) { Destroy(this.gameObject); }
    }

    /// <summary>
    /// Enable Player PowerUp when PowerUp collides with Player.
    /// </summary>
    /// 
    /// <param name="other">Object the PowerrUp collided with</param>
    /// <remarks>
    /// The PowerUp collider ignores all collisions expect with the Player game object.  Once the PowerUp collides with the
    /// Player game object, the PowerUp is destroyed.
    /// </remarks>
    /// 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // HACK: Audio to "far away" (soft).
            //
            //      The initial line below does not work (in any reasonable sense) because by setting the position to be
            //      that of the PowerUp, the volume is too low.
            //
            //          AudioSource.PlayClipAtPoint(_powerUpSound, transform.position, 1.0f);
            //
            //      The crazy math below on the position argument brings the sound "closer to the listener", thus making it
            //      Loueder.
            //
            //      There may likely be more sensible locations, or at least more obvious...
            //
            AudioSource.PlayClipAtPoint(_powerUpSound, 0.9f * Camera.main.transform.position + 0.1f * transform.position, 1.0f);
            Player player = other.transform.GetComponent<Player>();
            if (player != null) 
            { 
                switch (this.tag) 
                {
                    case "TripleShotPowerUp":
                        player.TripleShotPowerUp();
                        break;
                    case "SpeedPowerUp":
                        player.SpeedBoost();
                        break;
                    case "ShieldPowerUp":
                        player.ShieldUp();
                        break;

                }
            }
            Destroy(this.gameObject);
        }
    }
}
