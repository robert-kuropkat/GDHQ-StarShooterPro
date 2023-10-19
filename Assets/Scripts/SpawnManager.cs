using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn manager for all Enemy and PowerUp game objects
/// </summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: Spawn Manager Game Object in Game Scene
/// 
/// </remarks>
/// 

public class SpawnManager : MonoBehaviour
{

    /// <summary>
    /// The following variables are populated in the Inspector.
    /// </summary>
    [SerializeField] private GameObject     _enemyPrefab;
    [SerializeField] private GameObject     _enemyContainer;
    [SerializeField] private GameObject     _powerUpContainer;
    [SerializeField] private GameObject[]   _powerUps;
    ///
    [SerializeField] private int            _spawnEnemyWaitTime = 5;

    /// <summary>
    /// Private Variables
    /// </summary>
    private bool _stopSpawning = false;

    /// <summary>
    /// Initiate spawning of Enemies and PowerUps
    /// </summary>
    /// 
    /// <remarks>
    /// Invoked by the Asteroid class when the asteroid is destroyed by laser fire.
    /// </remarks>
    /// 

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }

    /// <summary>
    /// Trigger cessation of Enemy and PowerUp spawning.
    /// </summary>
    /// 
    /// <remarks>
    /// Invoked by the Plaery game object when the Player is destroyed.  Method merely sets a boolean
    /// checked by the Spawn Routines.
    /// </remarks>
    /// 

    public void PlayerDead() { _stopSpawning = true; }

    /// <summary>
    /// IEnumerator: Spawn new Enemy game objects
    /// 
    /// <example>
    ///    <code>
    ///    this.StartCoroutine(SpawnEnemyRoutine());
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <returns>WaitForSeconds(_spawnEnemyWaitTime)</returns>
    /// 
    /// <remarks>
    /// Pause for 3 seconds before spawning first enemy, then spawn a new one based on
    /// _spawnEnemyWaitTime.  For neatness in the Hierarchy, spawn them inside an Enemy
    /// folder.
    /// </remarks>
    /// 

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);     // 3 second pause before start of game
        while (!_stopSpawning)
        {
            GameObject newEnemy = GetNewEnemy();
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnEnemyWaitTime);
        }
    }

    /// <summary>
    /// IEnumerator: Spawn new PowerUp game objects every 3-7 seconds
    /// 
    /// <example>
    ///    <code>
    ///    this.StartCoroutine(SpawnPowerUp());
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <returns>WaitForSeconds(Random.Range(3, 8))</returns>
    /// 
    /// <remarks>
    /// Pause for 3 seconds before spawning first PowerUp, then spawn a new one every 
    /// 3-7 seconds.  For neatness in the Hierarchy, spawn them inside a PowerUp
    /// folder.
    /// </remarks>
    /// 

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(3);     // 3 second pause before start of game
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            GameObject newPowerUp = GetNewPowerUp();
            newPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }

    /// <summary>
    /// Instaniate random, new PowerUp
    /// </summary>
    /// 
    /// <remarks>
    /// Randomize and return a new PowerUp game object from the PowerUp Prefab.
    /// </remarks>
    /// 
    /// <returns>GameObject</returns>
    /// 

    private GameObject GetNewPowerUp()
    {
        int choosePowerUp = Random.Range(0, 3);
        return Instantiate(_powerUps[choosePowerUp]);
    }

    /// <summary>
    /// Instaniate new Enemy
    /// </summary>
    /// 
    /// <remarks>
    /// Return a new Enemy game object from the Enemy Prefab.
    /// </remarks>
    /// 
    /// <returns>GameObject</returns>
    /// 

    private GameObject GetNewEnemy()
    {
        return Instantiate(_enemyPrefab);
    }

}
