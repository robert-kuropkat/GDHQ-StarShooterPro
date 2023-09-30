using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerUpContainer;
    [SerializeField] private int _spawnEnemyWaitTime = 5;
    [SerializeField] private GameObject[] _powerUps;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);     // 3 second pause before start of game
        while (!_stopSpawning)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnEnemyWaitTime);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(3);     // 3 second pause before start of game
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            int choosePowerUp = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(_powerUps[choosePowerUp]);
            newPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }
    public void PlayerDead () { _stopSpawning = true; }
}
