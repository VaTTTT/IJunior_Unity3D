using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private bool _isOrderRandom;
    [SerializeField] private int _quantityOfEnemies;
    [SerializeField] EnemySpawnPoint[] _spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnAllEnemies(_delay));
    }

    private IEnumerator SpawnAllEnemies(float delay)
    {
        int enemyCounter = 0;
        int spawnPointIndex = 0;

        while (enemyCounter < _quantityOfEnemies)
        {
            if (_isOrderRandom)
            {
                spawnPointIndex = Random.Range(0, _spawnPoints.Length);
            }

            EnemySpawnPoint spawnPoint = _spawnPoints[spawnPointIndex];

            spawnPoint.SpawnEnemy();

            enemyCounter++;
            spawnPointIndex++;

            if (spawnPointIndex >= _spawnPoints.Length)
            {
                spawnPointIndex = 0;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
