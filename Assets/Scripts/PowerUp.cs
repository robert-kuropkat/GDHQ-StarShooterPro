using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private AudioClip _powerUpSound;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 8, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8.0f) { Destroy(this.gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // The initial line below does not work (in any reasonable sense) because by setting the position to be
            // that of the PowerUp, the volume is too low.
            //
            //      AudioSource.PlayClipAtPoint(_powerUpSound, transform.position, 1.0f);
            //
            // The crazy math below on the position argument brings the sound "closer to the listener", thus making it
            // Loueder.
            //
            // There may likely be more sensible locations, or at least more obvious...
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
