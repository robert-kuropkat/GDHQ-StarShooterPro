using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-10.0f,10.0f), 8, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8.0f) { transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 8, 0);  }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) { player.Damage(); }
            Destroy(this.gameObject);
        }
    }
}
