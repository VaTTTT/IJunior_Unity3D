using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Target _initialTarget;
    [SerializeField] private Target[] _patrolPoints;

    public void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(_enemy, transform.position, Quaternion.identity);

        if (newEnemy.TryGetComponent<CharacterMover>(out CharacterMover mover))
        {
            if (_initialTarget != null)
            {
                mover.SetTarget(_initialTarget);
            }
            
            if (_patrolPoints != null) 
            {
                mover.SetPatrolPoints(_patrolPoints);
            }
        }
    }
}