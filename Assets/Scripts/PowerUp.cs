using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

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
