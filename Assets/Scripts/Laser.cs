using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private AudioClip _explostion;
    // [SerializeField] public bool _enemyFire;
    public bool EnemyFire { get; set; }


    // Start is called before the first frame update
    void Start()
    {

        if ( this.transform.parent && this.transform.parent.CompareTag("Enemy") )
        {
            EnemyFire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyFire)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        if (transform.position.y > 8f || transform.position.y < -8f) 
        { 
            Destroy(this.gameObject); 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && this.EnemyFire)
        {
            Destroy(this.transform.parent.GetChild(this.transform.GetSiblingIndex()).gameObject);
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_explostion, 0.9f * Camera.main.transform.position + 0.1f * transform.position, 1.0f);
            //this.GetComponent<AudioSource>().Play();
            if (player != null) { player.Damage(); }
            //this.gameObject.GetComponent<Animator>().SetTrigger("EnemyDestroyed");
            Destroy(this.gameObject);
        }

        //if (other.tag == "Enemy" && !this.EnemyFire)
        //{
        //    AudioSource.PlayClipAtPoint(_explostion, 0.9f * Camera.main.transform.position + 0.1f * transform.position, 1.0f);
        //    Destroy(other.gameObject);
        //    Destroy(this.gameObject);
        //}

    }

}
