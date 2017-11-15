using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int EnemiesPerWave;
    public GameObject Enemy;
}

public class SpawnManager : MonoBehaviour
{
    public Wave[] Waves; // class to hold information per wave
    public Transform[] SpawnPoints;
    public float TimeBetweenEnemies = 2f;

    private GameManager _gameManager;

    private int _totalEnemiesInCurrentWave;
    private int _enemiesInWaveLeft;
    private int _spawnedEnemies;

    private int _currentWave;
    private int _totalWaves;

    private GameObject _enemyContainer;
    private bool _isSpawning;

	void Start ()
	{
	    _gameManager = GetComponentInParent<GameManager>();

	    _currentWave = -1; // avoid off by 1
	    _totalWaves = Waves.Length - 1; // adjust, because we're using 0 index

        _enemyContainer = new GameObject("Enemy Container");
	    _isSpawning = true;
	    StartNextWave();
	}

    void StartNextWave()
    {
        _currentWave++;
        // win
        if (_currentWave > _totalWaves)
        {
            _gameManager.Victory();
            return;
        }

        _totalEnemiesInCurrentWave = Waves[_currentWave].EnemiesPerWave;
        _enemiesInWaveLeft = 0;
        _spawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        GameObject enemy = Waves[_currentWave].Enemy;
        while (_spawnedEnemies < _totalEnemiesInCurrentWave)
        {
            _spawnedEnemies++;
            _enemiesInWaveLeft++;

            int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

            if (_isSpawning)
            {
                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                GameObject newEnemy = Instantiate(enemy, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
                newEnemy.transform.SetParent(_enemyContainer.transform);
            }
            yield return new WaitForSeconds(TimeBetweenEnemies);
        }
        yield return null;
    }
    
    // called by an enemy when they're defeated
    public void EnemyDefeated()
    {
        _enemiesInWaveLeft--;
        // We start the next wave once we have spawned and defeated them all
        if (_enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
        {
            StartNextWave();
        }
    }

    public void DisableAllEnemies()
    {
        _isSpawning = false;
        // cycle through all of our enemies
        for (int i = 0; i < _enemyContainer.transform.childCount; i++)
        {
            Transform enemy = _enemyContainer.transform.GetChild(i);
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

            // if the enemy is still alive, we want to disable it
            if (health != null && health.Health > 0 && movement != null)
            {
                movement.PlayVictory();
            }
        }
    }
}
