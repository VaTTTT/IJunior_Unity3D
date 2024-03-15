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

    private void SpawnEnemy(EnemySpawnPoint spawnPoint)
    {
        if (spawnPoint.Enemy)
        {
            Enemy enemy = Instantiate(spawnPoint.Enemy, spawnPoint.transform.position, Quaternion.identity);
        }
    }

    private IEnumerator SpawnAllEnemies(float delay)
    {
        int enemyCounter = 0;
        int spawnPointIndex = 0;

        WaitForSeconds pauseTime = new WaitForSeconds(delay);

        while (enemyCounter < _quantityOfEnemies)
        {
            if (_isOrderRandom)
            {
                spawnPointIndex = Random.Range(0, _spawnPoints.Length);
            }

            SpawnEnemy(_spawnPoints[spawnPointIndex]);

            enemyCounter++;
            spawnPointIndex++;

            if (spawnPointIndex >= _spawnPoints.Length)
            {
                spawnPointIndex = 0;
            }

            yield return pauseTime;
        }
    }
}
