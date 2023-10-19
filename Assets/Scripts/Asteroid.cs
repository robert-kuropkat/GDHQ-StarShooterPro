using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aswteroid Class.
/// </summary>
/// <remarks>
/// Instantiated By: the Game Scene
/// Attached To: Asteroid Game Object
/// 
/// This class maanges the Asteroid game object.  This object, when destoryed by 
/// laser fire, will initiate the game play.
/// </remarks>
public class Asteroid : MonoBehaviour
{
    /// <summary>
    /// The following variables are populated in the Inspector.
    /// </summary>
    [SerializeField] private GameObject     _explosionPrefab;
    //
    [SerializeField] private float          _rotationSpeed      = 20f;

    /// <summary>
    /// Private Variables
    /// </summary>
    private SpawnManager _spawnManager;

    /// <summary>
    /// On start, grab the Spawn Manager game object and ensure it is not null.
    /// </summary>
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (!_spawnManager) { Debug.LogError("Spawn Manager is NULL."); }
    }

    /// <summary>
    /// Rotate the asteroid until it is destroyed and starts the game play.
    /// </summary>
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// After being shot by a laser, initiate game play using the Spawn Manager, then
    /// cleanup the laser and the asteroid itself.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _spawnManager.StartSpawning();
            _ = Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.25f);
        }

    }
}
