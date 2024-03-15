using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private PatrolPoint[] _patrolPoints;

    public Enemy Enemy => _enemy;
    public PatrolPoint[] PatrolPoints => _patrolPoints;
}