using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-10.0f,10.0f), 8, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (!_player) { Debug.Log("Player is NULL"); }
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
            this.gameObject.GetComponent<Animator>().SetTrigger("EnemyDestroyed");
            this.GetComponent<AudioSource>().Play();
            if (_player != null) { _player.AddScore(10); }
            _speed = 0;
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.4f);
        }

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            this.GetComponent<AudioSource>().Play();
            if (player != null) { player.Damage(); }
            this.gameObject.GetComponent<Animator>().SetTrigger("EnemyDestroyed");
            _speed = 0;
            Destroy(this.gameObject, 2.4f);
        }
    }
}
